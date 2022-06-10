using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player.State
{
    public class HurtState : IState
    {
        PlayerController player;
        PlayerStateManager state;
        public HurtState(PlayerController playerController, PlayerStateManager stateManager)
        {
            player = playerController;
            state = stateManager;
        }

        public void OnStateEnabled()
        {
            
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
