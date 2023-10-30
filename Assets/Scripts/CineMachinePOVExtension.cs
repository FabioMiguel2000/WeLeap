using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private float clampAngle = 80f;
    [SerializeField]
    private float verticalSpeed = 40f; // Look around speed in the vertical axis
    [SerializeField]
    private float horizontalSpeed = 40f; // Look around speed in the horizontal axis


    private InputManager inputManager;
    private Vector3 startingRotation;
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }

                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaTime * verticalSpeed *deltaInput.x;
                startingRotation.y += deltaTime * horizontalSpeed * deltaInput.y;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);

            }
        }
    }
}
