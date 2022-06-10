using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player.State
{
    public class DeadState : IState
    {
        PlayerController player;
        PlayerStateManager state;
        public DeadState(PlayerController playerController, PlayerStateManager stateManager)
        {
            player = playerController;
            state = stateManager;
        }

        public void OnStateEnabled()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
