using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoBoundaryScript : MonoBehaviour
{
    public List<GameObject> AddedObjects;

    public List<Mesh> SandMeshs;
    public List<Mesh> RockMeshs;
    public List<GameObject> BackPrefabs;

    public List<GameObject> FrontLocations;
    public List<Transform> BackLocations;

    public GameObject TreePrefab;

    public bool isLeft;

    public GameObject LandParent;

    void Start()
    {
        foreach(GameObject Location in FrontLocations)
        {
            if(Random.Range(0,2) == 0)
            {
                int RandNumber = Random.Range(0, SandMeshs.Count);

                MeshFilter Filter = Location.GetComponent<MeshFilter>();

                Filter.mesh = SandMeshs[RandNumber];

                GameObject ColliderChild = Location.transform.Find("MeshColliderThing").gameObject;

                ColliderChild.GetComponent<MeshCollider>().sharedMesh = SandMeshs[RandNumber];
            } else if(Random.Range(0,2) == 0) {
                int RandNumber = Random.Range(0, RockMeshs.Count);

                MeshFilter Filter = Location.GetComponent<MeshFilter>();

                Filter.mesh = RockMeshs[RandNumber];

                Location.transform.localScale = new Vector3(3, 3, 3);
            }
        }

       if(!isLeft)
       {
           LandParent.transform.rotation = Quaternion.Euler(0, 180, 0);
       }
    }

    void OnDestroy()
    {
        foreach(GameObject AddedThing in AddedObjects)
        {
            Destroy(AddedThing.gameObject);
        }
    }
}  
