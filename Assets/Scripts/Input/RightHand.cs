using UnityEngine;
using Leap.Unity;
using Leap;

public class RightHand : MonoBehaviour
{
    public float x_value;
    public float y_value;
    public float x_value_offset;
    public float y_value_offset;
    public float valueMax;
    public float valueMin;
    public float deadzoneThreshold = 0.01f;
    private bool inFist;
    GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if (playerObjects.Length > 1)
        {
            Debug.LogWarning("There is more than on object with the tag Player");
        }
        playerObject = playerObjects[0];    
    }

    // Update is called once per frame
    void Update()
    {
        Hand rightHand = Hands.Provider.GetHand(Chirality.Right);

        if (rightHand != null && !inFist)
        {
            // Undo Camera rotation
            Vector3 rightHandVector = rightHand.Direction.Pivot(Vector3.zero, Quaternion.Inverse(Camera.main.transform.rotation));
            x_value = rightHandVector.x + x_value_offset;
            if (Mathf.Abs(x_value) < deadzoneThreshold) x_value = 0f;
            else if (x_value > valueMax) x_value = valueMax;
            else if (x_value < valueMin) x_value = valueMin;

            y_value = rightHandVector.y + y_value_offset;
            if (Mathf.Abs(y_value) < deadzoneThreshold) y_value = 0f;
            else if (y_value > valueMax) y_value = valueMax;
            else if (y_value < valueMin) y_value = valueMin;
            
            Debug.Log(new Vector2 (x_value, y_value));
        }
        else
        {
            x_value = 0f;
            y_value = 0f;
        }

    }

    public void OnFistEnter(){
        inFist = true;
    }
    public void OnFistExit(){
        inFist = false;
    }
}