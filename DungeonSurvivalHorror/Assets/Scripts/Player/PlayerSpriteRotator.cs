using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SHDC.Player 
{
    public class PlayerSpriteRotator
    {
        PlayerController playerController;
        SpriteRenderer playerSpriteRenderer;
        Vector2 playerDirection;
        
        public PlayerSpriteRotator(PlayerController player) 
        {
            playerController = player;
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        }

        public float PlayerAngle
        {
            set 
            {
                     if (value >  157.5f  && value <  179.9f) playerDirection = new Vector2(-1,  0); //West//
                else if (value >  112.5f  && value <  157.5f) playerDirection = new Vector2(-1,  1); //North West
                else if (value >  67.5f   && value <  112.5f) playerDirection = new Vector2( 0,  1); //North//
                else if (value >  22.5f   && value <   67.5f) playerDirection = new Vector2( 1,  1); //North East
                else if (value > -22.5f   && value <   22.5f) playerDirection = new Vector2( 1,  0); //East//
                else if (value > -57.5f   && value <  -22.5f) playerDirection = new Vector2( 1, -1); //South East
                else if (value > -112.5f  && value <  -57.5f) playerDirection = new Vector2( 0, -1); //South //
                else if (value > -157.5f  && value < -112.5f) playerDirection = new Vector2(-1, -1); //South West
                else if (value > -179.9f  && value < -157.5f) playerDirection = new Vector2(-1,  0); //West//
            }
        }
        public Vector2 PlayerDirection 
        {
            get 
            {
                if(playerDirection.y> 0) playerSpriteRenderer.sortingOrder = 2;
                else playerSpriteRenderer.sortingOrder = 0;
                return  playerDirection; 
            } 
        }
        public void GunRotate(float gunAngle,Transform gunTransform)
        { 
            if (gunAngle >= 90 || gunAngle <= -90) gunTransform.localScale = new Vector3(1, -1, 1);
            else gunTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}

