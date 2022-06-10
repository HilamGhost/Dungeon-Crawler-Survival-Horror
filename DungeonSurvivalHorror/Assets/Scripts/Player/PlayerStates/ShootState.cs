using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.CameraSettings;

namespace SHDC.Player.State
{
    public class ShootState : IState
    {
        PlayerController player;
        PlayerStateManager state;
        public ShootState(PlayerController playerController, PlayerStateManager stateManager)
        {
            player = playerController;
            state = stateManager;
        }

        public void OnStateEnabled()
        {
            CameraController.Instance.ChangeCameraTargetAimTarget();
        }

        public void OnStateUpdate()
        {
            player.Aim();
            player.Shoot();
            if (!player.PlayerInput.Aim) 
            {
                state.ChangeState(state.IdleState);
            }
        }
        public void OnStateFixedUpdate()
        {
            player.MovePlayer(true);
        }
        public void OnStateExit()
        {
            player.Shoot(true);
        }

       
    }
}
