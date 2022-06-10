using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;
using SHDC.Abstract;

namespace SHDC.Enemy 
{
    public class EnemyThrowerAttacker : EnemyAttacker, IShooter
    {
        EnemyController enemyController;
        ThrowerData enemyData;

        public EnemyThrowerAttacker(EnemyController enemyControl)
        {
            enemyController = enemyControl;
            enemyData = enemyController.EnemyData as ThrowerData;
        }
        public override IEnumerator Attack()
        {
            enemyController.IsAttacking = true;
                       
            yield return new WaitForSeconds(enemyData.ShootRate);
            enemyController.EnemyAnimator.Attack();
            Shoot();
            
            enemyController.IsAttacking = false;
        }
        public void Shoot()
        {
            enemyData.ShootProjectile(enemyController.transform,enemyController.EnemyMover.Direction);
        }
    }
}

