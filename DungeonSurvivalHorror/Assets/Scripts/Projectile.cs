using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SHDC 
{
    [RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
    public class Projectile : MonoBehaviour, IDamageGiver
    {
        [SerializeField] int damage;
        public int Damage { get => damage; set => damage = value; }

        public void GiveDamage(int givenDamage, IDamageTaker damageTaker)
        {
            damageTaker.TakeDamage(Damage);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            IDamageTaker damageTaker = collision.gameObject.GetComponent<IDamageTaker>();
            if(damageTaker != null) 
            {
                GiveDamage(Damage,damageTaker);
            }
             Destroy(gameObject);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            
        }

    }
}
