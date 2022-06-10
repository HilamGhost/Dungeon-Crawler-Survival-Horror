using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Enemy
{
    public class EnemyAnimator
    {
        Animator enemyAnimator;
        public EnemyAnimator(EnemyController enemy) 
        {
            enemyAnimator = enemy.GetComponent<Animator>();
            enemyAnimator.runtimeAnimatorController = enemy.EnemyData.EnemyAnimatorController;
        }
        public bool IsMoving { set => enemyAnimator.SetBool("IsMoving",value); }
        public void Attack() { enemyAnimator.SetTrigger("Attack"); }
        public void Death() { enemyAnimator.SetTrigger("Death"); }
        public float AnimationTime => enemyAnimator.GetCurrentAnimatorClipInfo(0).Length;
    }
}

