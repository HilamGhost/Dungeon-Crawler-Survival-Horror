using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Abstract 
{
    [CreateAssetMenu(fileName = "New Projectile Data",menuName ="Projectiles")]
    public class ProjectileDatas : ScriptableObject
    {
        [SerializeField] int projectileDamage;
        [SerializeField] float projectileSpeed;
        [SerializeField] GameObject projectilePrefap;

        public int ProjectileDamage { get => projectileDamage; }
        public float ProjectileSpeed { get => projectileSpeed; }
        public GameObject ProjectielPrefap { get => projectilePrefap; }
    }
}

