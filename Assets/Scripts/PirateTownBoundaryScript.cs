using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateTownBoundaryScript : MonoBehaviour
{
    public List<GameObject> AddedObjects;

    public bool isLeft;
    public GameObject LandParent;

    public List<Mesh> Platforms;
    public List<GameObject> PlatformObjects;
    public List<GameObject> BuildingObjects;
    public List<Transform> BuildingLocations;
    public List<GameObject> BarrelObjects;
    public Transform BarrelSpawnLocation;


    public LayerMask terrainLayer;

    void Start()
    {
        foreach(GameObject Plank in PlatformObjects)
        {
            int RandNumber = Random.Range(0, Platforms.Count);

            MeshFilter Filter = Plank.GetComponent<MeshFilter>();

            Filter.mesh = Platforms[RandNumber];

            GameObject ColliderChild = Plank.transform.Find("MeshColliderEmpty").gameObject;

            ColliderChild.GetComponent<MeshCollider>().sharedMesh = Platforms[RandNumber];
        }

        if(!isLeft)
        {   
            LandParent.transform.localRotation = Quaternion.Euler(0, 180, 0);
            LandParent.transform.localPosition = new Vector3(0, 0, 150);
        }

        StartCoroutine(LateStart(.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        foreach(Transform BuildingLocation in BuildingLocations)
        {
            int RandNumber = Random.Range(0, BuildingObjects.Count);

            GameObject SpawnedObject = Instantiate(BuildingObjects[RandNumber], (BuildingLocation.position + new Vector3((Random.Range(-41,41)), 100, 0)), Quaternion.Euler(0, 90, 0));

            AddedObjects.Add(SpawnedObject);

            SpawnedObject.transform.localScale = new Vector3(10, 10, 10);

            Ray ray = new Ray((SpawnedObject.transform.position), Vector3.down);

            if(Physics.Raycast(ray, out RaycastHit info, 400, terrainLayer.value))
            {
                SpawnedObject.transform.position = info.point;
            } else {
                //This is wehre it should destroy if it doesn't hit terrain
            }
        }

        if(Random.Range(0,3) == 1)
        {
            int RandNumber = Random.Range(0, BarrelObjects.Count);

            GameObject SpawnedBarrel = Instantiate(BarrelObjects[RandNumber], BarrelSpawnLocation.position, Quaternion.identity);

            AddedObjects.Add(SpawnedBarrel);

            SpawnedBarrel.transform.localScale = new Vector3(13, 13, 13);

            Ray ray = new Ray((SpawnedBarrel.transform.position), Vector3.down);

            if(Physics.Raycast(ray, out RaycastHit info, 400, terrainLayer.value))
            {
                SpawnedBarrel.transform.position = info.point;
            } else {
                //This is wehre it should destroy if it doesn't hit terrain
            }
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
