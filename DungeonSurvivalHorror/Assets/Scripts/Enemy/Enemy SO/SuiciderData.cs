using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Enemy;


namespace SHDC.Abstract
{
    [CreateAssetMenu(fileName = "New Suicider", menuName = "Enemy/Suicider")]
    public class SuiciderData : EnemyDatas
    {
        public override LayerMask EnemyLayerMask()
        {
            return LayerMask.NameToLayer("Enemy/Suicider");
        }

        public override void Move(float moveSpeed, Rigidbody2D enemyRB, Vector2 direction)
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
        public override EnemyAttacker CreateEnemyAttacker(EnemyController enemyControl)
        {
            return new EnemySuiciderAttacker(enemyControl);
        }
    }
}
