using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Abstract;
using SHDC.Player.State;
using SHDC.Interactable;

namespace SHDC.Player 
{
    public class PlayerController : MonoBehaviour, IDamageTaker
    {
        PlayerSpriteRotator playerSpriteRotator;
        PlayerAnimation playerAnimation;
        PlayerMover playerMover;
        PlayerShoot playerShoot;
        PlayerStateManager playerState;

        [Header("Input"), SerializeField] PlayerInputController playerInput;
        [SerializeField] Camera playerCamera;

        [Header("Stats")]
        [SerializeField] string currentState;
        [SerializeField] int health;
        [SerializeField] int knifeCount;
        [SerializeField] int medPackCount;
        [SerializeField] float healTime;
        [SerializeField] bool isHealing;
        Collider2D interactCollider;

        [Header("Movement")]
        [SerializeField] float playerSpeed;
        [SerializeField] Vector2 moveDirection;
        Vector2 mousePosition;

        [Header("Gun Movement")]
        [SerializeField] Vector2 gunDirection;
        [SerializeField] Transform gunTransform;
        [SerializeField] float gunAngle;

        [Header("Shoot Mechanic")]
        [SerializeField] ProjectileDatas currentProjectileData;
        [SerializeField] Transform shotPoint;
        [SerializeField] float shootTime;
        float shootTimeReset;

        #region Properties
        //Movement Properties
        public Vector2 MoveDirection { get => new Vector2(playerInput.Horizontal, playerInput.Vertical); }
        public bool CanMove 
        {
            get 
            {
                if (playerState.CurrentState == playerState.IdleState) return true;
                else if (playerState.CurrentState == playerState.ShootState) return true;
                else if (playerState.CurrentState == playerState.HealState) return true;
                else return false;
            } 
        }
        public Rigidbody2D PlayerRB => GetComponent<Rigidbody2D>();
        //Gun Movement Properties
        public PlayerInputController PlayerInput { get => playerInput; }
        public float GunAngle { get => gunAngle; set => gunAngle = value; }
        //Shoot Properties
        public Transform ShotPoint { get => shotPoint; }
        public ProjectileDatas CurrentProjectileData { get => currentProjectileData; }
        public float ShootTime { get => shootTime; set => shootTime = value; }

        //Stats Properties
        public PlayerStateManager PlayerState => playerState;
        public  IState State { set => currentState = value.GetType().Name; }
        public int Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0) Death();
                if (health >= 6) health = 6;
            }
        }
        public int KnifeCount { get => knifeCount; set => knifeCount = value; }
        public int MedPackCount { get => medPackCount; set => medPackCount = value; }

        //Components
        public PlayerAnimation PlayerAnimation { get => playerAnimation; }
        #endregion

        void Start()
        {
            DoAssignment();
        }

        
        void Update()
        {
            moveDirection = MoveDirection;
            mousePosition = playerInput.MouseInput(playerCamera);

            playerSpriteRotator.GunRotate(gunAngle,gunTransform);
            playerSpriteRotator.PlayerAngle = gunAngle;
            gunDirection = playerSpriteRotator.PlayerDirection;

            PlayerAnimationController();
            playerState.OnStateUpdate();
        }
        private void FixedUpdate()
        {               
             playerState.OnStateFixedUpdate();
        }
        public void MovePlayer(bool canPlayerMove) 
        {
            if (canPlayerMove) playerMover.Move(moveDirection, playerSpeed);
            else playerMover.DontMove();
        }
        public void Aim() 
        {
            playerMover.RotatePlayer(mousePosition, gunTransform);          
        }
        public void Shoot(bool cancelShoot = false) 
        {
            playerShoot.PrepareShoot(playerInput.InteractDown,playerInput.InteractUp);
            if (cancelShoot) playerShoot.PrepareShoot(false, true);
        }
        public void StartHeal() 
        {
            if(medPackCount > 0 && health < 6) 
            {
                StartCoroutine(Heal());
            }
            else 
            {
                playerState.ChangeState(playerState.IdleState);
            }
        }
        public IEnumerator Heal()
        {
            isHealing = true;
            yield return new WaitForSeconds(healTime);
            Health++;
            MedPackCount--;
            isHealing = false;
            playerState.ChangeState(playerState.IdleState);
        }
        public void PlayerGrabbed(bool isGrabbed) 
        {
            if(isGrabbed)
                PlayerState.ChangeState(PlayerState.GrabbedState);
            else
                PlayerState.ChangeState(PlayerState.IdleState);
        }     
        public void TakeDamage(int takenGamage, bool isGrabbed)
        {
            StartCoroutine(GrabDamage(takenGamage));
        }
        IEnumerator GrabDamage(int takenGamage) 
        {
            playerState.ChangeState(playerState.HurtState);
            
            Health -= takenGamage;
            
            SlamPlayer();
            playerAnimation.PlayHurtAnimation();
            
            yield return new WaitForSeconds(0.1f);                    
            
         
            playerMover.IsSlammed = false;
            playerState.ChangeState(playerState.IdleState);
        }
        public void SlamPlayer() 
        {
            playerMover.IsSlammed = true;
            Vector2 slamTarget = new Vector2(transform.localPosition.x * 15, transform.localPosition.y * 15);
            playerMover.Slam(slamTarget.x, slamTarget.y);
            
        }
        void Death() 
        {
            
        }
        void DoAssignment()
        {         

            playerMover = new PlayerMover(this);
            playerAnimation = new PlayerAnimation(this);
            playerSpriteRotator = new PlayerSpriteRotator(this);
            playerShoot = new PlayerShoot(this);
            playerState = new PlayerStateManager(this);

            shootTimeReset = shootTime;
        }
        void PlayerAnimationController()
        {
            playerAnimation.SetDirection(gunDirection);

            if (CanMove)
            {
                if (Mathf.Approximately(moveDirection.x, 0) && Mathf.Approximately(moveDirection.y, 0)) playerAnimation.IsMoving = false;
                else playerAnimation.IsMoving = true;
            }
            else playerAnimation.IsMoving = false;
        }

        public void TakeDamage(int takenGamage)
        {
            Health -= takenGamage;
            playerAnimation.PlayHurtAnimation();
        }
        public void Interact() 
        {
           interactCollider?.GetComponent<IInteractable>().Interact(this);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Interactable")) 
            {
                interactCollider = other;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            interactCollider = null;
        }

    }
}

