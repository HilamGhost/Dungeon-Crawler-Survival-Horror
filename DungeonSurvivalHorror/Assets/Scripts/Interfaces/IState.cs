using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Player.State
{
    public interface IState
    {
        public void OnStateEnabled();
        public void OnStateUpdate();
        public void OnStateFixedUpdate();
        public void OnStateExit();
    }
}
