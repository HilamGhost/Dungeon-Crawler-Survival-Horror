using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;
using SHDC.Abstract;


namespace SHDC.Enemy 
{
    public class EnemyMover
    {

        EnemyController enemyController;
        Rigidbody2D enemyRB;
        EnemyDatas enemyData;

        Transform player;
        Transform enemy;

        Vector3 playerSawPosition;
        Vector3 enemyFirstPosition;
        
        #region Properties
        public float Distance 
        {
            get 
            {
                if (player != null) return Vector2.Distance(player.transform.position, enemy.transform.position);
                else return 666.66f;
            }
        }
        public Vector3 Direction 
        {
            get
            {
                if (player != null) return (player.transform.position - enemy.transform.position).normalized;
                else return Vector3.zero;
            }
        }
        public bool HasPlayerSeen => player != null;
        #endregion
        
        public EnemyMover(EnemyController enemyControl)
        {
            enemyController = enemyControl;
            enemyRB = enemyController.GetComponent<Rigidbody2D>();
            enemy = enemyController.transform;
            
            enemyFirstPosition = enemyController.transform.position;
            enemyData = enemyControl.EnemyData;
        }

        /// <summary>
        /// Checks distance between player and enemy.
        /// </summary>
        /// <param name="dedectRadius">Player dedect radius </param>
        /// <param name="playerLayer"> Player's layer </param>
        public void CheckDistance(float dedectRadius, LayerMask playerLayer) 
        {
            

            if (player == null) 
            {
                Collider2D playerCollider = Physics2D.OverlapCircle(enemy.position, dedectRadius, playerLayer);
                if (playerCollider != null) 
                {
                    player = playerCollider.transform;
                    playerSawPosition = player.position;

                } else return;
            }
            else 
            {
                if (Distance > (dedectRadius * 2)) 
                     player = null;
            }
               
            


        }


        /// <summary>
        /// Moves enemy into the Player. If you give a O value, enemy will stop.
        /// </summary>
        /// <param name="moveSpeed">This paramater must get the value from Enemy Data!</param>
        public void Move(float moveSpeed) 
        {
            if (player != null && !enemyController.EnemyState.Equals(EnemyStates.Grab)) 
            {
                Vector2 direction;
                if (enemyController.IsEnemyTypeEqual(ScriptableObject.CreateInstance<SuiciderData>()))
                    direction = (playerSawPosition - enemyFirstPosition).normalized;
                else
                    direction = (player.position - enemy.position).normalized;

                enemyData.Move(moveSpeed, enemyRB, direction);
            }
            else 
            {
                enemyData.Move(0, enemyRB, new Vector2(0,0));
            }
            
            
        }
    }
}

