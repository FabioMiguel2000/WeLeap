using UnityEngine;
using Leap.Unity;
using Leap;

public class RightHand : HandScript
{
    void Update()
    {
        Hand rightHand = Hands.Provider.GetHand(Chirality.Right);

        if (rightHand != null && !inFist)
        {
            // Undo Camera rotation
            Vector3 rightHandVector = rightHand.Direction.Pivot(Vector3.zero, Quaternion.Inverse(Camera.main.transform.rotation));
            x_value = ProcessValue(rightHandVector.x + x_valueOffset);
            y_value = ProcessValue(rightHandVector.y + y_valueOffset);
        }
        else
        {
            x_value = 0f;
            y_value = 0f;
        }

    }
}