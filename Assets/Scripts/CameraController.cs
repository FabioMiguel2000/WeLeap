using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraController : MonoBehaviour
{
    // Camera
    public float cameraSpeed = 12.0f;
    public float moveSpeed = 2.0f;
    public float minYAngle = -90.0f;
    public float maxYAngle = 90.0f;
    private float rotX;
    private float rotY;
    public bool inOrbit;
    public GameObject orbitCentre;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        Vector2 mouseInfo = inputManager.GetMouseDelta();

        rotX += -mouseInfo.y * cameraSpeed * Time.deltaTime;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minYAngle, maxYAngle);

        rotY += mouseInfo.x * cameraSpeed * Time.deltaTime;

        if (inOrbit) {
            transform.RotateAround(orbitCentre.transform.position, Vector3.up, mouseInfo.x * cameraSpeed * Time.deltaTime);
            orbitCentre.transform.Rotate(new Vector3(0, mouseInfo.x * cameraSpeed * Time.deltaTime, 0));
            transform.RotateAround(orbitCentre.transform.position, orbitCentre.transform.right, -mouseInfo.y * cameraSpeed * Time.deltaTime);
        }else transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
    }
}
