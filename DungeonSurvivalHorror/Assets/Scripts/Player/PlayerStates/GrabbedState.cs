using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player.State
{
    public class GrabbedState : IState
    {
        PlayerController player;
        PlayerStateManager state;
        public GrabbedState(PlayerController playerController, PlayerStateManager stateManager)
        {
            player = playerController;
            state = stateManager;
        }

        public void OnStateEnabled()
        {
            player.MovePlayer(false);
        }

        public void OnStateExit()
        {
           
        }

        public void OnStateFixedUpdate()
        {
            
        }

        public void OnStateUpdate()
        {
           
        }
    }
}
