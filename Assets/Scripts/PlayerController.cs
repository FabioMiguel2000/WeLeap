using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public LeftHand leftHand;
    public float leftHandScaleFactorX = 6.0f;
    public float leftHandScaleFactorY = 12.0f;
    public float leftHandScaleFactorZ = 12.0f;

    private CharacterController controller;
    private CameraController cam;

    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;

    private Transform cameraTransform;

    private InputManager inputManager;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        cam = gameObject.GetComponent<CameraController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        //Vector3 move = new Vector3(leftHand.x_value * leftHandScaleFactorX, leftHand.y_value * leftHandScaleFactorY, leftHand.z_value * leftHandScaleFactorZ);
        Vector3 movement = inputManager.GetPlayerMovement();

        Vector3 move;
        if (cam.inOrbit) move = cameraTransform.forward * movement.z;
        else move = cameraTransform.forward * movement.z + cameraTransform.right * movement.x + cameraTransform.up * movement.y;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }
}
