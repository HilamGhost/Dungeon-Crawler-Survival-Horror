using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player.State
{
    public class PlayerStateManager
    {
        static PlayerController player;
        IState currentState;

        IdleState idleState;
        ShootState shootState;
        HealState healState;
        HurtState hurtState;
        GrabbedState grabbedState;
        DeadState deadState;
        InteractState interactState;

        #region Properties
        public IState CurrentState => currentState;
        public IdleState IdleState => idleState;
        public ShootState ShootState => shootState;
        public HealState HealState => healState;
        public HurtState HurtState => hurtState;
        public GrabbedState GrabbedState => grabbedState;
        public DeadState DeadState => deadState;
        public InteractState InteractState => interactState;
        #endregion
        #region StateManager Const.
        public PlayerStateManager(PlayerController playerController)
        {
            player = playerController;
            
            idleState = new IdleState(player,this);
            shootState = new ShootState(player,this);
            healState = new HealState(player,this);
            hurtState = new HurtState(player,this);
            grabbedState = new GrabbedState(player,this);
            deadState = new DeadState(player,this);
            interactState = new InteractState(player,this);

            currentState = idleState;
        }
        #endregion

        public void OnStateUpdate() 
        {
            currentState.OnStateUpdate();
        }
        public void OnStateFixedUpdate()
        {
            currentState.OnStateFixedUpdate();
        }

        /// <summary>
        /// Changes the player's state
        /// </summary>
        /// <param name="state">You must use this format: player.PlayerState.IState</param>
        public void ChangeState(IState state) 
        {
            if (state != currentState) 
            {      
                currentState.OnStateExit();
                currentState = state;
                currentState.OnStateEnabled();
                player.State = currentState;
            }
            
        }

        /// <summary>
        /// Is the wanted state equal player's state
        /// </summary>
        /// <param name="state">You must use this format: player.PlayerState.IState</param>
        public bool IsPlayerStateEqual(IState state) 
        {
            if (state == currentState) return true;
            else return false;
        }
    }
}
