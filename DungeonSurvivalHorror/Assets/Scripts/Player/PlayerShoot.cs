using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SHDC.Player 
{
    public class PlayerShoot : IShooter
    {
        Transform shotPointTransform;
        GameObject projectilePrefap;

        int projectileDamage;
        float projectileSpeed;
       
        float shootTime;
        float shootTimeReset;


        bool prepareShoot;
        PlayerController playerControl;
        PlayerAnimation playerAnimation;

        public float ShootTime => shootTime;

        public PlayerShoot(PlayerController playerController) 
        {

            playerControl = playerController;
            playerAnimation = playerControl.PlayerAnimation;
            shootTime = playerController.ShootTime;
            shootTimeReset = shootTime;
            shotPointTransform = playerController.ShotPoint;

            projectilePrefap = playerController.CurrentProjectileData.ProjectielPrefap;
            projectileDamage = playerController.CurrentProjectileData.ProjectileDamage;
            projectileSpeed = playerController.CurrentProjectileData.ProjectileSpeed;
           
        }
        public void PrepareShoot(bool isHolding,bool isReleased) 
        {
            bool endShoot = false;
            if (isHolding) 
            {
                prepareShoot = true;
            }
            if (isReleased)
            {
                endShoot = true;
            }
            if (prepareShoot) 
            {
                playerControl.ShootTime -= Time.deltaTime;
                if (playerControl.ShootTime <= 0)
                {
                    endShoot = true;
                    Shoot();

                    //Visual
                    playerAnimation.PlayShootAnimation();
                }
            }
            if(endShoot)
            {
                prepareShoot = false;
                playerControl.ShootTime = shootTimeReset;
            }
        }
        public void Shoot()
        {
            playerControl.Health--;
            GameObject projectile = GameObject.Instantiate(projectilePrefap, shotPointTransform.position,shotPointTransform.rotation);
            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            projectileRB.AddForce(shotPointTransform.right*projectileSpeed, ForceMode2D.Impulse);
        }
       
    }
}

