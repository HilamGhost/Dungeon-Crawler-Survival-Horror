using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.CameraSettings;

namespace SHDC.Player.State
{
    public class IdleState : IState
    {
        PlayerController player;
        PlayerStateManager state;
        public IdleState(PlayerController playerController,PlayerStateManager stateManager)
        {
            player = playerController;
            state = stateManager;
        }

        public void OnStateEnabled()
        {
            CameraController.Instance.ChangeCameraTargetPlayer();
        }

        public void OnStateUpdate()
        {
            if (player.PlayerInput.Heal) 
            {
                state.ChangeState(state.HealState);
            }
            if (player.PlayerInput.Aim)
            {
                state.ChangeState(state.ShootState);
            }
            if (player.PlayerInput.InteractDown)
            {
                player.Interact();
            }
            player.Aim();

        }
        public void OnStateFixedUpdate()
        {
            player.MovePlayer(true);
        }
        public void OnStateExit()
        {
           
        }

        
    }
}
