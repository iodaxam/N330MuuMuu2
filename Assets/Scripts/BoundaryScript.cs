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

                int RandNumber = Random.Range(0, RockPrefabs.Count);

                GameObject SpawnedRock = Instantiate(RockPrefabs[RandNumber], SpawnLocation.position, Quaternion.Euler(0, Orientation, 0));

                if(RandNumber == 2) {
                    
                    int Offset = (isLeft) ? -15 : 15;

                    SpawnedRock.transform.position = new Vector3((SpawnedRock.transform.position.x + Offset), SpawnedRock.transform.position.y, SpawnedRock.transform.position.z);
                }

                SpawnedRockObjects.Add(SpawnedRock);

                SpawnedRock.transform.localScale = new Vector3(5f, 5f, 5f);
            }

            if(!isLeft)
            {   
                Vector3 OriginalPosition = LandObject.transform.position;
                LandObject.transform.position = new Vector3((OriginalPosition.x + 120), OriginalPosition.y, OriginalPosition.z);

                LandObject.transform.rotation = Quaternion.Euler(0, 180, 0);
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
