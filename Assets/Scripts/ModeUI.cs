using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeUI : MonoBehaviour
{
    private enum mode { NAVIGATION_MODE, ORBIT_MODE};


    public void ChangeMode(int mode_num)
    {
        if (mode_num == ((int)mode.NAVIGATION_MODE))
        {
            transform.Find("Orbit").gameObject.SetActive(false);
            transform.Find("Navigation").gameObject.SetActive(true);
            return;

        }
        if (mode_num == ((int)mode.ORBIT_MODE))
        {
            transform.Find("Navigation").gameObject.SetActive(false);
            transform.Find("Orbit").gameObject.SetActive(true);
            return;
        }
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Orbit").gameObject.SetActive(false);
        transform.Find("Navigation").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
