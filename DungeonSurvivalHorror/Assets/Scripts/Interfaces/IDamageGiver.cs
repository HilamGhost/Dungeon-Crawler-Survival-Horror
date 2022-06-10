using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageGiver
{
    public void GiveDamage(int givenDamage,IDamageTaker damageTaker);
}
