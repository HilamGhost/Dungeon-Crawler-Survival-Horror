using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Abstract;
using SHDC.Player;

namespace SHDC.Enemy 
{
    [RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D),typeof(SpriteRenderer))]
    public class EnemyController : MonoBehaviour, IDamageTaker
    {
        public EnemyMover EnemyMover { get; private set; }
        EnemyAttacker enemyAttacker;
        EnemyVisual enemyVisual;
        public EnemyAnimator EnemyAnimator;

        [SerializeField] EnemyDatas enemyData;
        [Header("Stats")]
        [SerializeField] Vector2 enemyDirection;
        [SerializeField] EnemyStates enemyState;
        [SerializeField] int health;
        [SerializeField] int damage;
        [SerializeField] bool isAttacking;
        
        #region Properties
        public int Health 
        {   
            get => health; 
            set 
            {
                health = value;
                if (health <= 0 && !IsDead) StartCoroutine(Death());             
            } 
        }
        public bool IsDead { get; private set; }
        public EnemyStates EnemyState
        {
            get => enemyState;
            set => enemyState = value;
        }
        /// <summary>
        /// Turns Enemy data base class to child classes.
        /// </summary>
        public EnemyDatas EnemyData
        {
            get
            {
                if (enemyData.GetType() == typeof(CatcherData)) return enemyData as CatcherData;
                else if (enemyData.GetType() == typeof(ThrowerData)) return enemyData as ThrowerData;
                else if (enemyData.GetType() == typeof(SuiciderData)) return enemyData as SuiciderData;
                else return null;
            }
        }
        /// <summary>
        /// Is EnemyData equal the wanted kind of data?
        /// </summary>
        public bool IsEnemyTypeEqual(EnemyDatas kind)
        {
            if (EnemyData.GetType() == kind.GetType()) return true; else return false;
        }

        public bool IsAttacking 
        {
            get => isAttacking; 
            set => isAttacking = value; 
        }
        /// <summary>
        /// Enemy's direction but with flat values.
        /// </summary>
        public Vector2 EnemyDirection
        {
            get => enemyDirection;             
            private set 
            {
                float newX = Mathf.Round(value.x);
                float newY = Mathf.Round(value.y);
                Vector2 newDirection = new Vector2(newX,newY);
                enemyDirection = newDirection; 
            }
        }

        #endregion

        void Awake() 
        {
            MakeAssignment();
        }
        void Update() 
        {
            //Check distance with player.
            EnemyMover.CheckDistance(enemyData.DedectRadius,enemyData.PlayerLayer);

            //Change States of Enemy
            ChangeEnemyState();
            SetEnemyAnimations();

            enemyVisual.RotatePlayer(EnemyDirection.x);

            EnemyDirection = EnemyMover.Direction;
        }
        void FixedUpdate() 
        {
            switch (EnemyState) 
            {
                case EnemyStates.Chase:

                    //If distance with player is smaller than thrower's shoot distance,SHOOT!
                    if (IsEnemyTypeEqual(ScriptableObject.CreateInstance<ThrowerData>()))
                    {
                        var throwerDataVar = EnemyData as ThrowerData;
                        if (EnemyMover.Distance < throwerDataVar.ShootDistance)
                        {
                            StartCoroutine(enemyAttacker.Attack());
                        }
                    }
                    //Movement code 
                    EnemyMover.Move(enemyData.MoveSpeed);
                    break;
                case EnemyStates.Attacking:
                    EnemyMover.Move(0);
                    break;
                case EnemyStates.Death:
                    EnemyMover.Move(0);
                    break;
            }
                 
            
        }

        ///Stats Functions
        public void TakeDamage(int takenGamage)
        {
            Health-= takenGamage;
        }
        #region Null Interface 
        public void TakeDamage(int takenGamage, bool isGrabbed) { } 
        #endregion

        IEnumerator Death() 
        {
            EnemyState = EnemyStates.Death;
            IsDead = true;
            EnemyAnimator.Death();
            yield return new WaitForSeconds(EnemyAnimator.AnimationTime);
            Destroy(gameObject);
        }
        ///Make Assignment
        void MakeAssignment()
        {
            Health = enemyData.MaxHealth;
            damage = enemyData.Damage;
            gameObject.name = enemyData.EnemyName;
            gameObject.layer = enemyData.EnemyLayerMask();


            EnemyMover = new EnemyMover(this);
            enemyAttacker = EnemyData.CreateEnemyAttacker(this);
            enemyVisual = new EnemyVisual(this);
            EnemyAnimator = new EnemyAnimator(this);

            enemyVisual.SetEnemiesVisuals();
        }
        /// <summary>
        /// Changes Enemy's State
        /// </summary>
        void ChangeEnemyState() 
        {
            switch (EnemyState) 
            {
                case EnemyStates.Idle:
                    if (EnemyMover.HasPlayerSeen) EnemyState = EnemyStates.Chase;
                    if (IsAttacking) EnemyState = EnemyState = EnemyStates.Attacking;
                    if (IsDead) EnemyState = EnemyStates.Death;
                    break;
                case EnemyStates.Chase:
                    if (IsAttacking) EnemyState = EnemyState = EnemyStates.Attacking;
                    if (IsDead) EnemyState = EnemyStates.Death;
                    break;
                case EnemyStates.Attacking:
                    if (!IsAttacking) 
                    {
                        if (EnemyMover.HasPlayerSeen) EnemyState = EnemyStates.Chase; 
                        else EnemyState = EnemyStates.Idle;         
                    }
                    if (IsDead) EnemyState = EnemyStates.Death;
                    break;
                case EnemyStates.Death:                  
                    break;
                case EnemyStates.Hit: 
                    break;
            }
        }
        /// <summary>
        /// Set Enemy's Animation
        /// </summary>
        void SetEnemyAnimations() 
        {
            switch (EnemyState)
            {
                case EnemyStates.Idle:
                    EnemyAnimator.IsMoving = false;
                    break;
                case EnemyStates.Chase:
                    EnemyAnimator.IsMoving = true;
                    break;             
                case EnemyStates.Death:        
                    break;
                case EnemyStates.Hit: break;
            }
        }
        ///Physics Functions
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (EnemyState != EnemyStates.Death) 
            {
                if (EnemyState != EnemyStates.Attacking)
                {
                    PlayerController playerController = collision.transform.GetComponent<PlayerController>();
                    if (playerController != null) 
                    {
                        if (!playerController.PlayerState.IsPlayerStateEqual(playerController.PlayerState.GrabbedState)) 
                        {
                            StartCoroutine(enemyAttacker.Attack(playerController));
                        }
                        else 
                        {
                            enemyState = EnemyStates.Idle;
                            playerController = null;
                        }
                        
                        
                    }
                }
                if (collision.transform.CompareTag("Wall"))
                {
                    if (IsEnemyTypeEqual(ScriptableObject.CreateInstance<SuiciderData>())) Health = 0;
                }
            }
            


        }

    }
}

