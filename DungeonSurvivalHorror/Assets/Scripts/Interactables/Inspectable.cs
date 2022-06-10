using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;
using SHDC.Dialogue;

namespace SHDC.Interactable
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "Interactables/Talkables/Create New Inspectable")]
    public class Inspectable : Talkable
    {
        [SerializeField] List<Quotes> quoteList;

        List<Quotes> QuoteList => quoteList;
        public override void Interact(PlayerController player)
        {
            DialogueSystem.Instance.TakeQuote(quoteList);
            player.PlayerState.ChangeState(player.PlayerState.InteractState);
        }
    }
}
