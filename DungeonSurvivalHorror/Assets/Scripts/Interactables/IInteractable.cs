using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;

namespace SHDC.Interactable
{
    public interface IInteractable
    {
        public void Interact(PlayerController player);
    }
}
