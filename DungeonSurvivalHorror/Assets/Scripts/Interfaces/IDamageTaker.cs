using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageTaker
{
    public int Health { get; set; }
    public void TakeDamage(int takenGamage);

    public void TakeDamage(int takenGamage,bool isGrabbed);
}
    
