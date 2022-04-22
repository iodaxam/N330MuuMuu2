using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateTownBoundaryScript : MonoBehaviour
{
    public bool isLeft;
    public GameObject LandParent;

    public List<Mesh> Platforms;
    public List<GameObject> PlatformObjects;

    void Start()
    {
        foreach(GameObject Plank in PlatformObjects)
        {
            int RandNumber = Random.Range(0, Platforms.Count);

            MeshFilter Filter = Plank.GetComponent<MeshFilter>();

            Filter.mesh = Platforms[RandNumber];
        }

        if(!isLeft)
        {   
            LandParent.transform.localRotation = Quaternion.Euler(0, 180, 0);
            LandParent.transform.localPosition = new Vector3(0, 0, 150);
        }
    }
}  

