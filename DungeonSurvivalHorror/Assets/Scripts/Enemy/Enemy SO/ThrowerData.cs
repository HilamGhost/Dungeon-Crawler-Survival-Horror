using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Enemy;

namespace SHDC.Abstract
{
    [CreateAssetMenu(fileName = "New Thrower", menuName = "Enemy/Thrower")]
    public class ThrowerData : EnemyDatas
    {

        [Header("Thrower Stats")]
        [SerializeField] GameObject enemyProjectile;
        [SerializeField] float shootDistance;
        [SerializeField] float shootRate;
        [SerializeField] float projectileSpeed;

        public GameObject EnemyProjectile => enemyProjectile; 
        public float ShootDistance => shootDistance;
        public float ShootRate => shootRate;
        public float ProjectileSpeed => projectileSpeed;

        public override EnemyAttacker CreateEnemyAttacker(EnemyController enemyControl)
        {
            return new EnemyThrowerAttacker(enemyControl);
        }

        public override LayerMask EnemyLayerMask()
        {
            return  LayerMask.NameToLayer("Enemy/Thrower");
        }

        public override void Move(float moveSpeed, Rigidbody2D enemyRB,Vector2 direction)
        {
            if (moveSpeed > 0)
            {
                Vector2 moveDirection = direction * moveSpeed;
                enemyRB.velocity = moveDirection;
            }
            else
            {
                enemyRB.velocity = new Vector2(0, 0);
            }
        }
        
        /// <summary>
        /// Shoots a projectile into the Player
        /// </summary>
        public void ShootProjectile(Transform enemy,Vector2 direction) 
        {
            GameObject spawnedProjectile = Instantiate(EnemyProjectile,enemy.position,enemy.rotation);
            Rigidbody2D projectileRb = spawnedProjectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(direction*projectileSpeed,ForceMode2D.Impulse);
        }
    }
}

