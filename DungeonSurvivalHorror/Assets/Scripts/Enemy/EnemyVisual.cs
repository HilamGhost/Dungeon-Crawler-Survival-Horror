using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Abstract;

namespace SHDC.Enemy
{
    public class EnemyVisual
    {
        EnemyController enemy;
        EnemyDatas enemyData;

        BoxCollider2D enemyCollider;
        SpriteRenderer enemySpriteRenderer;
        public EnemyVisual(EnemyController enemyController) 
        {
            enemy = enemyController;
            enemyData = enemyController.EnemyData;

            enemyCollider = enemyController.GetComponent<BoxCollider2D>();
            enemySpriteRenderer = enemyController.GetComponent<SpriteRenderer>();
        }
        /// <summary>
        /// Sets Enemy's Visual
        /// </summary>
        public void SetEnemiesVisuals() 
        {
            enemyCollider.size = enemyData.ColliderSize;
            enemyCollider.offset = enemyData.ColliderOffset;

            enemySpriteRenderer.sprite = enemyData.EnemySprite;
        }

        public void RotatePlayer(float enemyDirection)
        {        
                if (enemyDirection < 0) enemySpriteRenderer.flipX = true;
                if (enemyDirection > 0) enemySpriteRenderer.flipX = false;                  
        }
    }
}

