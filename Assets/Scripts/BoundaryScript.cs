using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    public List<GameObject> SpawnedRockObjects;
    
    public Mesh LandMesh;
    public bool isLand;
    public GameObject LandObject;

    public List<GameObject> RockPrefabs;
    public List<Transform> RockLocations;

    public bool isLeft;

    void Start()
    {
        if(!isLand) {
            
            foreach(Transform SpawnLocation in RockLocations)
            {
                float Orientation = Random.Range(0,360);

                GameObject SpawnedRock = Instantiate(RockPrefabs[Random.Range(0, RockPrefabs.Count)], SpawnLocation.position, Quaternion.Euler(0, Orientation, 0));

                //(Random.Range(0f, 360f)

                SpawnedRockObjects.Add(SpawnedRock);

                SpawnedRock.transform.localScale = new Vector3(5f, 5f, 5f);
            }

        } else {

            MeshFilter LandMeshFilter = LandObject.GetComponent<MeshFilter>();
            LandMeshFilter.mesh = LandMesh;

            if(!isLeft)
            {   
                Vector3 OriginalPosition = LandObject.transform.position;
                LandObject.transform.position = new Vector3((OriginalPosition.x + 120), OriginalPosition.y, OriginalPosition.z);

                LandObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void OnDestroy()
    {
        if(!isLand)
        {
            foreach(GameObject Rock in SpawnedRockObjects)
            {
                Destroy(Rock.gameObject);
            }
        } else {
            Destroy(LandObject);
        }
    }
}
