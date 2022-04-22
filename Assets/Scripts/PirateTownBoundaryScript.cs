using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateTownBoundaryScript : MonoBehaviour
{
    public bool isLeft;
    public GameObject LandParent;

    void Start()
    {
       if(!isLeft)
        {   
            LandParent.transform.localRotation = Quaternion.Euler(0, 180, 0);
            LandParent.transform.localPosition = new Vector3(0, 0, 150);
        }
    }
}  

