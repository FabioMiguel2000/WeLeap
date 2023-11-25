using Leap;
using Leap.Unity;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public LeapProvider leapProvider;
    public float x_value;
    public float y_value;
    public float z_value;
    public float x_valueOffset;
    public float y_valueOffset;
    public float z_valueOffset;
    public float valueMax;
    public float valueMin;
    public float deadzoneThreshold = 0.1f;
    protected bool inFist;

    protected GameObject playerObject;

    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if (playerObjects.Length > 1)
        {
            Debug.LogWarning("There is more than on object with the tag Player");
        }
        playerObject = playerObjects[0];
    }

    public void OnFistEnter(){
        inFist = true;
    }
    public void OnFistExit(){
        inFist = false;
    }

    protected float ProcessValue(float value){
        float newValue;

        if (Mathf.Abs(value) < deadzoneThreshold) newValue = 0f;
        else if (value > valueMax) newValue = valueMax;
        else if (value < valueMin) newValue = valueMin;
        else if(value > 0) newValue = value - deadzoneThreshold;
        else if(value < 0) newValue = value + deadzoneThreshold;
        else newValue = value;

        return newValue;
    }

    protected void OnEnable()
    {
        leapProvider.OnUpdateFrame += OnUpdateFrame;
    }
    protected void OnDisable()
    {
        leapProvider.OnUpdateFrame -= OnUpdateFrame;
    }
    protected virtual void OnUpdateFrame (Frame frame){}
}
