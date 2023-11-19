using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Leap.Unity;
using Leap;

public class CineMachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private float clampAngle = 80f;
    [SerializeField]
    private float verticalSpeed = 2.5f; // Look around speed in the vertical axis
    [SerializeField]
    private float horizontalSpeed = 2.5f; // Look around speed in the horizontal axis

    private InputManager inputManager;
    private Vector3 startingRotation;
    public GameObject playerObject;


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

                RightHand rightHand = GameObject.FindGameObjectWithTag("Player").GetComponent<RightHand>();

                Vector2 deltaInput = inputManager.GetMouseDelta();

                startingRotation.x += deltaTime  *10000* horizontalSpeed * rightHand.GetDeltaPosition().x;
                startingRotation.y += deltaTime  * 10000*verticalSpeed * rightHand.GetDeltaPosition().y;

                //print(100 * deltaInput.x);
                startingRotation.x += deltaTime * 100 * deltaInput.x;
                startingRotation.y += deltaTime * 100 * deltaInput.y;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);

            }
        }
    }
}
