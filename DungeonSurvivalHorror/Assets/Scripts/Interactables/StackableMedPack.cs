using SHDC.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Interactable
{
    [CreateAssetMenu(fileName = "Create New Interactable", menuName = "Interactables/Create New MedPack")]
    public class StackableMedPack : Resources
    {
        public override void Interact(PlayerController player)
        {
            player.MedPackCount++;
        }
    }
}
