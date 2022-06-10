using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SHDC.CameraSettings
{
    public class CameraController : Singleton<CameraController>
    {
        [Header("Targets")]
        [SerializeField] Transform player;
        [SerializeField] Transform aimTarget;
        [Header("Cinemachine")]
        [SerializeField] CinemachineVirtualCamera cinemachineCamera;
        [SerializeField] Camera mainCamera;

        
       
        public void ChangeCameraTargetPlayer() 
        {
            cinemachineCamera.Follow = player;
        }
        public void ChangeCameraTargetAimTarget()
        {
            cinemachineCamera.Follow = aimTarget;
        }

    }
}
