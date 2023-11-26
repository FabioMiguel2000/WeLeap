using UnityEngine;
using Leap.Unity;
using Leap;

public class RightHand : HandScript
{
    protected override void OnUpdateFrame(Frame frame)
    {
        //Use a helpful utility function to get the first hand that matches the Chirality
        Hand rightHand = frame.GetHand(Chirality.Right);

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