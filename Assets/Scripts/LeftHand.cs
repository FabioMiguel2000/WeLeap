using Leap;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public float x_value;
    public float y_value;
    public float z_value;
    public float x_valueOffset;
    public float y_valueOffset;
    public float z_valueOffset;
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
        List<Hand> _allHands = Hands.Provider.CurrentFrame.Hands;

        if (leftHand != null)
        {
            // Undo Camera rotation and Player translation
            Vector3 leftHandVector = leftHand.Basis.translation.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation)) - playerObject.transform.position;
            x_value = leftHandVector.x + x_valueOffset;
            y_value = leftHandVector.y + y_valueOffset;
            z_value = leftHandVector.z + z_valueOffset;
            //Debug.Log(leftHandVector);

            Vector3 leftHandPalmNormal = leftHand.PalmNormal.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation));

            Debug.Log(leftHand.PalmNormal);
            //Debug.Log((leftHand.Basis.rotation.eulerAngles.z - Camera.main.transform.rotation.x));

        }
        else
        {
            x_value = 0f;
            y_value = 0f;
            z_value = 0f;
        }
    }
}
