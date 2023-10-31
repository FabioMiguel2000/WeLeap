using Leap;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public float y_value;
    public float y_valueOffset;
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
            // Undo Camera Rotation
            Vector3 leftHandVector = leftHand.Basis.translation.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation)) - playerObject.transform.position;
            y_value = leftHandVector.y + y_valueOffset;
            Debug.Log(y_value);
        }
        else
        {
            y_value = 0f;
        }
    }
}
