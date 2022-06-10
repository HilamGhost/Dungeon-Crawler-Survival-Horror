using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Enemy;



namespace SHDC.Abstract
{
    [CreateAssetMenu(fileName = "New Catcher", menuName = "Enemy/Catcher")]
    public class CatcherData : EnemyDatas
    {

        [Header("Catcher Stats")]
        [SerializeField] float catchTime;

        public float CatchTime => catchTime;

        public override LayerMask EnemyLayerMask()
        {
            return  LayerMask.NameToLayer("Enemy/Catcher");
        }

        public override EnemyAttacker CreateEnemyAttacker(EnemyController enemyControl)
        {
            return new EnemyCatcherAttacker(enemyControl);
        }

        public override void Move(float moveSpeed, Rigidbody2D enemyRB, Vector2 direction)
        {
            if(moveSpeed > 0) 
            {
                Vector2 moveDirection = direction * moveSpeed;
                enemyRB.velocity = moveDirection;
            }
            else
            {
                enemyRB.velocity = new Vector2(0, 0);
            }      
        }
    }
}

