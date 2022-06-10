using SHDC.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Interactable
{
    public class InteractableObject : MonoBehaviour,IInteractable
    {
        [SerializeField] InteractableData interactableData;
        [SerializeField] bool isOneTimeInteracted;
        void Start() 
        {
            transform.name = interactableData.InteractableName;
            isOneTimeInteracted = interactableData.IsOneTimeInteracted;
        }
        public void Interact(PlayerController player)
        {
            interactableData.Interact(player);
            if (isOneTimeInteracted) 
            {
                Destroy(gameObject);
            }
        }
    }
}
