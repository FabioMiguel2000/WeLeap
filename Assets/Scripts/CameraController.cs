using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraController : MonoBehaviour
{
    public RightHand rightHand;

    // Camera
    public float cameraSpeed = 12.0f;
    public float minYAngle = -90.0f;
    public float maxYAngle = 90.0f;
    public bool inOrbit;
    public GameObject orbitCentre;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        //Vector2 mouseInfo = inputManager.GetMouseDelta();
        Vector2 mouseInfo = new Vector2(rightHand.x_value, rightHand.y_value) * 15f;

        float newYAngle = transform.eulerAngles.x - mouseInfo.y * cameraSpeed * Time.deltaTime;

        if (inOrbit) {
            orbitCentre.transform.RotateAround(orbitCentre.transform.position, Vector3.up, mouseInfo.x * cameraSpeed * Time.deltaTime);
            transform.RotateAround(orbitCentre.transform.position, Vector3.up, mouseInfo.x * cameraSpeed * Time.deltaTime);
            
            if (newYAngle < maxYAngle || newYAngle > 360f + minYAngle)
                transform.RotateAround(orbitCentre.transform.position, orbitCentre.transform.right, -mouseInfo.y * cameraSpeed * Time.deltaTime);
            
        }else {
            transform.RotateAround(transform.position, Vector3.up, mouseInfo.x * cameraSpeed * Time.deltaTime);

            if (newYAngle < maxYAngle || newYAngle > 360f + minYAngle)
            transform.RotateAround(transform.position, transform.right, -mouseInfo.y * cameraSpeed * Time.deltaTime);
        }
    }
}
