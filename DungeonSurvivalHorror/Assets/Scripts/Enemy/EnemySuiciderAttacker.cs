using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;

namespace SHDC.Enemy 
{
    public class EnemySuiciderAttacker : EnemyAttacker
    {

        EnemyController enemyController;

        public EnemySuiciderAttacker(EnemyController enemyControl)
        {
            enemyController = enemyControl;
        }
        public override IEnumerator Attack(PlayerController playerController)
        {
            
            enemyController.EnemyAnimator.Attack();
            yield return null;
            if (playerController.KnifeCount <= 0) 
            {
                GiveDamage(enemyController.EnemyData.Damage, playerController);              
            }
            else 
            {
                playerController.KnifeCount--;
            }
            enemyController.Health = 0;
        }
    }
}

