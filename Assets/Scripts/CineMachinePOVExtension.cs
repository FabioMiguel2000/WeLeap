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
    private float verticalSpeed = 100; // Look around speed in the vertical axis
    [SerializeField]
    private float horizontalSpeed = 100; // Look around speed in the horizontal axis


    private InputManager inputManager;
    private Vector3 startingRotation;
    private Vector3 lastTipPosition;
    private Vector3 deltaTipPosition;
    GameObject playerObject;

    protected override void Awake()
    {
        //GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player");
        inputManager = InputManager.Instance;
        lastTipPosition = new Vector3(0, 0, 0);
        base.Awake();
    }

    private void Update()
    {
        Hand rightHand = Hands.Provider.GetHand(Chirality.Right);
        //List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;
        if (rightHand == null)
        {
            deltaTipPosition = new Vector3(0, 0, 0);
            return;
        }

        //Vector3 rightHandVector = rightHand.Basis.translation.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation)) - playerObject.transform.position;


        Finger _middle = rightHand.GetMiddle();


        //if (lastTipPosition == null)
        //{
        //    lastTipPosition = _middle.TipPosition;
        //    deltaTipPosition = new Vector3(0, 0, 0);
        //    return;
        //}

        deltaTipPosition =  _middle.TipPosition- lastTipPosition;

        lastTipPosition = _middle.TipPosition;



        //Vector3 positionChange = lastTipPosition - rightHandVector;

        //lastTipPosition = rightHandVector;

        //deltaTipPosition = new Vector2(positionChange.x, positionChange.y);

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
                if (deltaTipPosition.x != 0)
                {
                    print(deltaTipPosition.x * 35000);

                }
                if (deltaTipPosition.y != 0)
                {
                    print(deltaTipPosition.y * 25000);

                }

                startingRotation.x += deltaTime * 50000 * deltaTipPosition.x;
                startingRotation.y += deltaTime * 25000 * deltaTipPosition.y;

                //print(100 * deltaInput.x);
                //startingRotation.x += deltaTime * 100 * deltaInput.x;
                //startingRotation.y += deltaTime * 100 * deltaInput.y;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);

            }
        }
    }
}
