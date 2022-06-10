using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Enemy;

namespace SHDC.Abstract
{
    public abstract class EnemyDatas : ScriptableObject
    {
        [Header("Identify")]
        [SerializeField] string enemyName;

        [Header("Stats")]
        [SerializeField] int maxHealth;
        [SerializeField] int damage;
        [SerializeField] float moveSpeed;
        [SerializeField] float dedectRadius;

        [Header("Visual")]
        [SerializeField] Sprite enemySprite;
        [SerializeField] RuntimeAnimatorController enemyAnimatorController;

        [Header("Collider Datas")]
        [SerializeField] Vector2 colliderOffset;
        [SerializeField] Vector2 colliderSize;


        public string EnemyName => enemyName;

       
        public Sprite EnemySprite => enemySprite;
        public RuntimeAnimatorController EnemyAnimatorController => enemyAnimatorController;

        public int Damage  => damage; 
        public int MaxHealth  => maxHealth; 
        public float MoveSpeed  => moveSpeed; 
        public float DedectRadius  => dedectRadius; 
        public LayerMask PlayerLayer => 0x01 << LayerMask.NameToLayer("Player"); 
        public Vector2 ColliderSize => colliderSize;
        public Vector2 ColliderOffset => colliderOffset;

        /// <summary>
        /// Creates new Enemy Attacker class.
        /// </summary>
        /// <param name="enemyControl"></param>
        /// <returns></returns>
        public abstract EnemyAttacker CreateEnemyAttacker(EnemyController enemyControl);

        public abstract void Move(float moveSpeed,Rigidbody2D enemyRB,Vector2 direction);

        public abstract LayerMask EnemyLayerMask();
        
        
    }
}
