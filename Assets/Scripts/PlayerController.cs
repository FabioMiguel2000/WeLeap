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

    // Camera
    public float cameraSpeed = 12.0f;
    public float moveSpeed = 2.0f;
    public float minYAngle = -90.0f;
    public float maxYAngle = 90.0f;
    private float rotX;
    private float rotY;
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
        UpdateCamera();
        UpdateMovement();
    }
    
    void UpdateCamera(){
        Vector2 mouseInfo = inputManager.GetMouseDelta();

        rotX += -mouseInfo.y * cameraSpeed * Time.deltaTime;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minYAngle, maxYAngle);

        rotY += mouseInfo.x * cameraSpeed * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
    }

    void UpdateMovement(){
        groundedPlayer = controller.isGrounded;

        //Vector3 move = new Vector3(leftHand.x_value * leftHandScaleFactorX, leftHand.y_value * leftHandScaleFactorY, leftHand.z_value * leftHandScaleFactorZ);
        Vector3 movement = inputManager.GetPlayerMovement();

        Vector3 move = cameraTransform.forward * movement.z + cameraTransform.right * movement.x + cameraTransform.up * movement.y;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }
}
