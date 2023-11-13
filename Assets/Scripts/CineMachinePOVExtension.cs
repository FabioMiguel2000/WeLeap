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
    private float verticalSpeed = 1f; // Look around speed in the vertical axis
    [SerializeField]
    private float horizontalSpeed = 1f; // Look around speed in the horizontal axis


    private InputManager inputManager;
    private Vector3 startingRotation;
    private Vector3 lastTipPosition;
    private Vector2 deltaTipPosition;
    GameObject playerObject;

    protected override void Awake()
    {
        GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player");
        inputManager = InputManager.Instance;
        base.Awake();
    }

    private void Update()
    {
        Hand rightHand = Hands.Provider.GetHand(Chirality.Right);
        //List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;
        if (rightHand == null)
        {
            deltaTipPosition = new Vector2(0, 0);
            return;
        }

        Vector3 rightHandVector = rightHand.Basis.translation.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation)) - playerObject.transform.position;

        if (lastTipPosition == null)
        {
            lastTipPosition = rightHandVector;
        }

        //Finger _middle = rightHand.GetMiddle();

        Vector3 positionChange = lastTipPosition - rightHandVector;

        lastTipPosition = rightHandVector;

        deltaTipPosition = new Vector2(positionChange.x, positionChange.y);

        //print(deltaTipPosition);


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
                //Hand rightHand = Hands.Provider.GetHand(Chirality.Right);
                //List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;

                //Finger _middle = rightHand.GetMiddle();

                //print(_middle.TipPosition);

                //Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaTime * verticalSpeed * deltaTipPosition.x;
                startingRotation.y += deltaTime * horizontalSpeed * deltaTipPosition.y;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);

            }
        }
    }
}
