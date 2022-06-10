using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Dialogue;
using SHDC.Player;

namespace SHDC.Interactable
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "Interactables/Talkables/Create New NPC")]
    public class NPC : Talkable
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
