using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public float incrementRate = 2f;
    float incrementsSum;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject orbitCentreObject;
    InputManager inputManager;
    CameraController cam;

    private void Start(){
        inputManager = InputManager.Instance;
        cam = player.GetComponent<CameraController>();
    }

    // private void Update(){
    //     if (inputManager.GetTriggerIsPressed()){
    //         float scaleIncrement = incrementRate * Time.deltaTime;
    //         transform.localScale += new Vector3 (0, scaleIncrement, 0);
    //     }else{
    //         if (inputManager.GetTriggerWasReleasedThisFrame()){
    //             transform.localScale = new Vector3 (transform.localScale.x, 0, transform.localScale.z);
    //         }
    //     }
    // }

    private void Update(){
        if (inputManager.GetTriggerIsPressed() && !cam.inOrbit){
            float increment = incrementRate * Time.deltaTime;
            incrementsSum += increment;
            Debug.Log(incrementsSum);

            transform.position = player.transform.position + Camera.main.transform.forward * incrementsSum;
        }else{
            if (inputManager.GetTriggerWasReleasedThisFrame()){
                //transform.localScale = new Vector3 (transform.localScale.x, 0, transform.localScale.z);\

                if (cam.inOrbit) {
                    cam.inOrbit = false;
                    Destroy(cam.orbitCentre);
                }
                else{
                    cam.inOrbit = true;
                    GameObject orbitCentre = Instantiate(orbitCentreObject);
                    orbitCentre.transform.SetPositionAndRotation(transform.position, transform.rotation);
                    //player.transform.SetParent(orbitCentre.transform);                
                    cam.orbitCentre = orbitCentre;
                }

                incrementsSum = 0;
                transform.position = player.transform.position;
            }
        }
    }
}
