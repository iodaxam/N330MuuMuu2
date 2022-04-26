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

    public Material VolcanoMat;

    void Start()
    {
        if(!isLeft)
       {
           LandParent.transform.rotation = Quaternion.Euler(0, 180, 0);
       }

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

                Location.transform.localScale = (RandNumber == 4) ? new Vector3(30, 30, 30) : new Vector3(3, 3, 3);

                if(RandNumber == 4)
                {
                    Renderer MatRend = Location.GetComponent<Renderer>();

                    MatRend.material = VolcanoMat;
                }
            }

            Location.transform.localScale += new Vector3(0, 0, Random.Range(-1, 2));
        }

        foreach(Transform Location in BackLocations)
        {
            int RandNumber = Random.Range(0, BackPrefabs.Count);

            Vector3 NewRotation = new Vector3(0, Random.Range(0, 360), 0);
            
            //(RandNumber <= 3) ? new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)) : new Vector3(0, Random.Range(0, 360), 0);

            Vector3 NewScale = (RandNumber <= 3) ? new Vector3(4, 2, Random.Range(4, 6)) : new Vector3(44, 44, 44);

            GameObject SpawnedObject = Instantiate(BackPrefabs[RandNumber], Location.position, Quaternion.Euler(NewRotation));

            SpawnedObject.transform.localScale = NewScale;

            AddedObjects.Add(SpawnedObject);
        }

        StartCoroutine(LateStart(.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

    }

    void OnDestroy()
    {
        foreach(GameObject AddedThing in AddedObjects)
        {
            Destroy(AddedThing.gameObject);
        }
    }
}  
