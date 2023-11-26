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
    ModeUI modeUI;
    private bool triggerOn;

    private void Start(){
        inputManager = InputManager.Instance;
        cam = player.GetComponent<CameraController>();
        triggerOn = false;
        modeUI = GameObject.FindGameObjectWithTag("UI Manager").transform.Find("Mode").GetComponent<ModeUI>();
    }

    public void OnTriggerEnter()
    {
        triggerOn = true;
    }

    public void OnTriggerRelease()
    {
        if (cam.inOrbit)
        {
            modeUI.ChangeMode(0);
            cam.inOrbit = false;
            triggerOn = false;
            Destroy(cam.orbitCentre);
        }
        else
        {
            modeUI.ChangeMode(1);
            cam.inOrbit = true;
            GameObject orbitCentre = Instantiate(orbitCentreObject);
            orbitCentre.transform.SetPositionAndRotation(transform.position, transform.rotation);
            //player.transform.SetParent(orbitCentre.transform);                
            cam.orbitCentre = orbitCentre;
        }

        incrementsSum = 0;
        transform.position = player.transform.position;
    }

    private void Update(){
        if ((inputManager.GetTriggerIsPressed() || triggerOn) && !cam.inOrbit){
            float increment = incrementRate * Time.deltaTime;
            incrementsSum += increment;
            Debug.Log(incrementsSum);

            transform.position = player.transform.position + Camera.main.transform.forward * incrementsSum;
        }
        else{
            if (inputManager.GetTriggerWasReleasedThisFrame() && cam.inOrbit){
                OnTriggerRelease();
            }
        }
    }
}
