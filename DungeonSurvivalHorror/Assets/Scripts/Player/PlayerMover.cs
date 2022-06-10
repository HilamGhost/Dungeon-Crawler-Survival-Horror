using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player 
{
    public class PlayerMover 
    {
        Rigidbody2D playerRB;
        PlayerController playerController;

        public bool IsSlammed { get; set; }

        public PlayerMover(PlayerController playerControl) 
        {
            playerRB = playerControl.GetComponent<Rigidbody2D>();
            playerController = playerControl;
        }

        public void Move(Vector2 direction,float playerSpeed) 
        {
            Vector2 moveDirection = direction * playerSpeed;
            playerRB.velocity = moveDirection;
        }
        public void DontMove() 
        {
            if (!IsSlammed) playerRB.velocity = Vector2.zero; else return;
        }

        public void RotatePlayer(Vector3 mousePos, Transform gunTransform) 
        {
            Vector2 lookDirection = (mousePos - gunTransform.position);
            float playerAngle = Mathf.Atan2(lookDirection.y,lookDirection.x)*Mathf.Rad2Deg;
            gunTransform.localRotation =Quaternion.Euler(0,0,playerAngle);
            playerController.GunAngle = playerAngle;
        }
        public void Slam(float x, float y)
        {
            if (!Mathf.Approximately(x, 0) || !Mathf.Approximately(y, 0))
            {
                IsSlammed = true;
                playerRB.AddForce(new Vector2(x,y),ForceMode2D.Impulse);
            }
        }
    }
}

