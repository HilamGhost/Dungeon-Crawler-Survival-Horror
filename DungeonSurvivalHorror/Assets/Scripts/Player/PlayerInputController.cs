using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHDC.Abstract 
{
    [CreateAssetMenu(fileName = "New Player Input", menuName = "Systems/Input")]
    public class PlayerInputController : ScriptableObject
    {
        [SerializeField] string horizontal = "Horizontal";
        [SerializeField] string vertical = "Verical";
        [SerializeField] string interact = "Interact";
        [SerializeField] string aim = "Aim";
        [SerializeField] string heal = "Heal";
        

        public float Horizontal => Input.GetAxis(horizontal); 
        public float Vertical => Input.GetAxis(vertical); 
        public bool InteractDown => Input.GetButtonDown(interact);

        public bool InteractUp => Input.GetButtonUp(interact);
        public bool Aim => Input.GetButton(aim);

        public bool Heal { get => Input.GetButtonDown(heal); }
        public Vector2 MouseInput(Camera cam)
        {
            return cam.ScreenToWorldPoint(Input.mousePosition); 
        }
    }
}

