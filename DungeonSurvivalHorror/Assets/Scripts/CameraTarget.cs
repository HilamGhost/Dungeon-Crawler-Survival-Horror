using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SHDC.Player;

namespace SHDC.CameraSettings 
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] PlayerController playerController;
        [SerializeField] float threshold;
        
        void Update()
        {
            Vector3 mousePos = playerController.PlayerInput.MouseInput(cam);
            Vector3 targetPos = (playerController.transform.position + mousePos)/2;

            targetPos.x = Mathf.Clamp(targetPos.x,-threshold + playerController.transform.position.x, threshold + playerController.transform.position.x);
            targetPos.y = Mathf.Clamp(targetPos.y, -threshold + playerController.transform.position.y, threshold + playerController.transform.position.y);

            transform.position = targetPos;
        }
    }
}

