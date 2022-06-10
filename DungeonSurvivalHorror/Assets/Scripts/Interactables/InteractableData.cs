using SHDC.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Interactable
{
    
    public abstract class InteractableData : ScriptableObject,IInteractable
    {
        [SerializeField] string interactableName;
        [SerializeField] bool isOneTimeInteracted;

        public string InteractableName => interactableName;
        public bool IsOneTimeInteracted => isOneTimeInteracted;

        public abstract void Interact(PlayerController player);
        
    }
}
