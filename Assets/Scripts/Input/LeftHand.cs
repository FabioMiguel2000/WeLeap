using Leap;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public float x_value;
    public float y_value;
    public float z_value;
    public float x_valueOffset = 0.35f;
    public float y_valueOffset;
    public float z_valueOffset;
    public float valueMax;
    public float valueMin;
    public float deadzoneThreshold = 0.01f;

    GameObject playerObject;

    private void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if (playerObjects.Length > 1)
        {
            Debug.LogWarning("There is more than on object with the tag Player");
        }
        playerObject = playerObjects[0];
    }

    private void Update()
    {
        Hand leftHand = Hands.Provider.GetHand(Chirality.Left);
        //List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;

        if (leftHand != null)
        {
            // Hand position for Z and Y movement
            // Undo Camera rotation and Player translation
            Vector3 leftHandVector = leftHand.Basis.translation.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation)) - playerObject.transform.position;

            y_value = leftHandVector.y + y_valueOffset;
            if (Mathf.Abs(y_value) < deadzoneThreshold) y_value = 0f;
            else if (y_value > valueMax) y_value = valueMax;
            else if (y_value < valueMin) y_value = valueMin;

            z_value = leftHandVector.z + z_valueOffset;
            if (Mathf.Abs(x_value) < deadzoneThreshold) z_value = 0f;
            else if (z_value > valueMax) z_value = valueMax;
            else if (z_value < valueMin) z_value = valueMin;


            // Hand Tilt for X movement
            // Undo Camera rotation
            Vector3 leftHandPalmNormal = leftHand.PalmNormal.Pivot(Vector3.zero, Quaternion.Inverse(Camera.main.transform.rotation));
            // Invert value
            x_value = (-1) * leftHandPalmNormal.x + x_valueOffset;

            if (Mathf.Abs(x_value) < deadzoneThreshold) x_value = 0f;
            else if (x_value > valueMax) x_value = valueMax;
            else if (x_value < valueMin) x_value = valueMin;
            //Debug.Log(x_value);
        }
        else
        {
            x_value = 0f;
            y_value = 0f;
            z_value = 0f;
        }
    }
}
