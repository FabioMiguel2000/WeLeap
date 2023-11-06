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
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private Transform cameraTransform;

    private InputManager inputManager;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        //Vector3 movement = inputManager.GetPlayerMovement();
        Vector3 movement = new Vector3(leftHand.x_value * leftHandScaleFactorX, leftHand.y_value * leftHandScaleFactorY, leftHand.z_value * leftHandScaleFactorZ);

        Vector3 move = new Vector3(movement.x, movement.y, movement.z);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x + cameraTransform.up * move.y;

        controller.Move(move * Time.deltaTime * playerSpeed);
    }
}
