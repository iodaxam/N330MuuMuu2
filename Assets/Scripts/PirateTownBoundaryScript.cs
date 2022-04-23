using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateTownBoundaryScript : MonoBehaviour
{
    private List<GameObject> AddedObjects;

    public bool isLeft;
    public GameObject LandParent;

    public List<Mesh> Platforms;
    public List<GameObject> PlatformObjects;
    public List<GameObject> BuildingObjects;
    public List<Transform> BuildingLocations;

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

            AddedObjects.Add(Instantiate(BuildingObjects[RandNumber], BuildingLocation.position, Quaternion.identity));
        }
    }
}  

