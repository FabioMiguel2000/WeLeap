using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class PlayerController : MonoBehaviour
{
    public LeftHand leftHand;
    public LeapProvider leapProvider;
    public float leftHandScaleFactorX = 6.0f;
    public float leftHandScaleFactorY = 12.0f;
    public float leftHandScaleFactorZ = 6.0f;
    public float minimumOrbitDistance = 2f;

    private CharacterController controller;
    private CameraController cam;

    [SerializeField]
    private float playerSpeed = 2.0f;

    private Transform cameraTransform;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        cam = gameObject.GetComponent<CameraController>();
    }
    
    void Update ()
    {
        Vector3 movement = new Vector3(leftHand.x_value * leftHandScaleFactorX, leftHand.y_value * leftHandScaleFactorY, leftHand.z_value * leftHandScaleFactorZ);
        //Vector3 movement = inputManager.GetPlayerMovement();

        Vector3 move;
        if (cam.inOrbit){
            move = cameraTransform.forward * movement.z;
            Vector3 newPosition = transform.position + move * Time.deltaTime * playerSpeed;
            if (Vector3.Distance(newPosition, cam.orbitCentre.transform.position) < Vector3.Distance(transform.position, cam.orbitCentre.transform.position)
                && Vector3.Distance(newPosition, cam.orbitCentre.transform.position) < minimumOrbitDistance){
                // Orbit distance to centre decreased too much
                move = new Vector3();
            }
        }
        else move = cameraTransform.forward * movement.z + cameraTransform.right * movement.x + cameraTransform.up * movement.y;
        if (!move.Equals(new Vector3())) transform.position = transform.position + move * Time.deltaTime * playerSpeed ;
    }
}
