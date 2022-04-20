using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScriptBeach : MonoBehaviour
{
    public List<GameObject> SpawnedBeachObjects;

    public List<Mesh> BeachMesh;
    public List<GameObject> BeachObjectLocations;

    public GameObject LandParent;

    public bool isLeft;

    void Start()
    {     
        foreach(GameObject SpawnLocation in BeachObjectLocations)
        {
            int RandNumber = Random.Range(0, BeachMesh.Count);

            MeshFilter Filter = SpawnLocation.GetComponent<MeshFilter>();

            Filter.mesh = BeachMesh[RandNumber];

            if(RandNumber == 0) {
                //int Offset = (isLeft) ? 100 : -100;

                SpawnLocation.transform.localPosition += new Vector3(100, 0, 0);
            } else if(RandNumber == 2) {
                SpawnLocation.transform.localPosition += new Vector3(-20, 0, 0);
            }

            SpawnLocation.transform.localScale += new Vector3(0, 0, Random.Range(-12, 10));
        }

        if(!isLeft)
        {   
            //Vector3 OriginalPosition = gameObject.transform.position;
            //gameObject.transform.position = new Vector3(OriginalPosition.x, OriginalPosition.y, (OriginalPosition.z + 240f));

            LandParent.transform.localRotation = Quaternion.Euler(0, 180, 0);
            LandParent.transform.localPosition = new Vector3(0, 0, 150);
        }
    }

    void OnDestroy()
    {

    }
}  

