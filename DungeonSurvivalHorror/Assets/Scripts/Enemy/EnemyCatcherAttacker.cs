using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;
using SHDC.Abstract;

namespace SHDC.Enemy 
{
    public class EnemyCatcherAttacker : EnemyAttacker 
    {
        EnemyController enemyController;
        CatcherData catcherData;
        public EnemyCatcherAttacker(EnemyController enemyControl)
        {
            enemyController = enemyControl;

            catcherData = enemyController.EnemyData as CatcherData;
            
        }
        public override IEnumerator Attack(PlayerController playerController)
        {
            enemyController.IsAttacking = true;
            playerController.PlayerGrabbed(true);

            CatchPlayer(playerController);
            enemyController.EnemyAnimator.Attack();
            yield return new WaitForSeconds(catcherData.CatchTime);

            if(playerController.KnifeCount > 0) 
            {
                LetPlayer(playerController);
                playerController.KnifeCount--;
                enemyController.TakeDamage(1);
                playerController.PlayerGrabbed(false);
                yield return new WaitForSeconds(catcherData.CatchTime);
            }
            else 
            {
                GiveDamage(catcherData.Damage, playerController, true);
                LetPlayer(playerController);
                yield return new WaitForSeconds(catcherData.CatchTime*2);
            }                    
            enemyController.IsAttacking = false;
        }

        void CatchPlayer(PlayerController player) 
        {
            player.transform.parent = enemyController.transform;
            player.transform.localPosition = new Vector3(enemyController.EnemyDirection.x,0,0);
        }
        void LetPlayer(PlayerController player)
        {
            player.transform.parent = null;
        }
    }
}

