using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player.State
{
    public class HealState : IState
    {
        PlayerController player;
        PlayerStateManager state;
        public HealState(PlayerController playerController, PlayerStateManager stateManager)
        {
            player = playerController;
            state = stateManager;
        }

        public void OnStateEnabled()
        {
            player.StartHeal();
        }

        public void OnStateFixedUpdate()
        {
            player.MovePlayer(true);
        }

        public void OnStateUpdate()
        {
            
        }
        public void OnStateExit()
        {
            
        }
    }
}
