using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostYardBoundaryScript : MonoBehaviour
{
    public List<GameObject> AddedObjects;

    public List<Transform> ObjectSpawns;
    public List<GameObject> ObjectPrefabs;
    public List<int> ObjectScales;

    public bool isLeft;
    public GameObject LandParent;

    void Start()
    {
        if(!isLeft) {
            LandParent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        foreach(Transform Spawn in ObjectSpawns)
        {
            int Randnumber = Random.Range(0, ObjectPrefabs.Count);

            GameObject SpawnedObject;

            if((Randnumber == 6) || (Randnumber == 7)) {
                SpawnedObject = Instantiate(ObjectPrefabs[Randnumber], Spawn.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            } else {
                SpawnedObject = Instantiate(ObjectPrefabs[Randnumber], Spawn.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            }

            AddedObjects.Add(SpawnedObject);

            SpawnedObject.transform.localScale = new Vector3(ObjectScales[Randnumber], ObjectScales[Randnumber], ObjectScales[Randnumber]);
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
