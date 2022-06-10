using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;

namespace SHDC.Enemy
{
    public abstract class EnemyAttacker: IDamageGiver
    {
        public virtual IEnumerator Attack()        
        {
            yield return null;
        }
        public virtual IEnumerator Attack(PlayerController playerController) 
        {
            yield return null;
        }
        
        public void GiveDamage(int givenDamage, IDamageTaker damageTaker)
        {
            damageTaker.TakeDamage(givenDamage);
        }
        public void GiveDamage(int givenDamage, IDamageTaker damageTaker,bool isGrabbed)
        {
            damageTaker.TakeDamage(givenDamage,true);
        }
    }
}
   
