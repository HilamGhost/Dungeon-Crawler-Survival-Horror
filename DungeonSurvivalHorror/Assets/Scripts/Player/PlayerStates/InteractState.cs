using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Dialogue;

namespace SHDC.Player.State
{
    public class InteractState : IState
    {
        PlayerController player;
        PlayerStateManager state;
        public InteractState(PlayerController playerController, PlayerStateManager stateManager)
        {
            player = playerController;
            state = stateManager;
        }

        public void OnStateEnabled()
        {
            
        }

        public void OnStateUpdate()
        {
            if (player.PlayerInput.InteractDown) 
            {
                DialogueSystem.Instance.InteractWithDialogue();
                
                if (!DialogueSystem.Instance.IsOpen) 
                {
                    player.PlayerState.ChangeState(player.PlayerState.IdleState);
                }
            }
        }
        public void OnStateFixedUpdate()
        {
            player.MovePlayer(false);
        }
        public void OnStateExit()
        {
        }
        
    }
}
