using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player 
{
    public class PlayerAnimation
    {
        Animator playerAnimator;
        public PlayerAnimation(PlayerController playerContoller) 
        {
            playerAnimator = playerContoller.GetComponent<Animator>();
        }


        public float DirectionX { set => playerAnimator.SetFloat("DirectionX", value); }

        public float DirectionY { set => playerAnimator.SetFloat("DirectionY", value); }

        public bool IsMoving {set => playerAnimator.SetBool("isMoving", value); }
        public void PlayShootAnimation() 
        {
            playerAnimator.SetTrigger("Shoot");
        }
        public void PlayHurtAnimation() 
        {
            playerAnimator.SetTrigger("TakeDamage");
        }
        public void SetDirection(Vector2 direction) 
        {
            DirectionX = direction.x;
            DirectionY = direction.y;
        }
    }
}

