using Leap;
using Leap.Unity;
using Unity.VisualScripting;
using UnityEngine;

public class LeftHand : HandScript
{
    protected override void OnUpdateFrame(Frame frame)
    {
        //Use a helpful utility function to get the first hand that matches the Chirality
        Hand leftHand = frame.GetHand(Chirality.Left);

        if (leftHand != null && !inFist)
        {
            // Hand position for Y movement
            // Undo Camera rotation and Player translation
            Vector3 leftHandVector = leftHand.Basis.translation.Pivot(Camera.main.transform.position, Quaternion.Inverse(Camera.main.transform.rotation)) - playerObject.transform.position;

            y_value = ProcessValue(leftHandVector.y + y_valueOffset);

            // Hand Tilt for X and Z movement
            // Undo Camera rotation
            Vector3 leftHandPalmNormal = leftHand.PalmNormal.Pivot(Vector3.zero, Quaternion.Inverse(Camera.main.transform.rotation));

            // Invert values
            x_value = ProcessValue((-1) * leftHandPalmNormal.x + x_valueOffset);
            z_value = ProcessValue((-1) * leftHandPalmNormal.z + z_valueOffset);
        }
        else
        {
            x_value = 0f;
            y_value = 0f;
            z_value = 0f;
        }
    }
}
