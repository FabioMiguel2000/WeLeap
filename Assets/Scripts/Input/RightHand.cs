using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class RightHand : MonoBehaviour
{
    private Vector3 deltaPosition;
    private GameObject playerObject;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        previousPosition = new Vector3(0, 0, 0);
    }

    public Vector3 GetDeltaPosition()
    {
        return deltaPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Hand rightHand = Hands.Provider.GetHand(Chirality.Right);

        if (rightHand == null)
        {
            deltaPosition = new Vector3(0, 0, 0);
            return;
        }

        Vector3 rightHandVector = rightHand.Basis.translation.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation)) - playerObject.transform.position;

        deltaPosition = rightHandVector - previousPosition;

        previousPosition = rightHandVector;
    }
}