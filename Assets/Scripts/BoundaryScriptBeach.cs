using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScriptBeach : MonoBehaviour
{
    public List<GameObject> SpawnedBeachObjects;

    public List<Mesh> BeachMesh;
    public List<GameObject> BeachObjectLocations;
    public List<GameObject> AdditionalTerrainObjects;

    public GameObject LandParent;

    public bool isLeft;

    public LayerMask terrainLayer;

    private Vector3 HitPosition;

    void Start()
    {     
        foreach(GameObject SpawnLocation in BeachObjectLocations)
        {
            int RandNumber = Random.Range(0, BeachMesh.Count);

            MeshFilter Filter = SpawnLocation.GetComponent<MeshFilter>();

            Filter.mesh = BeachMesh[RandNumber];

            GameObject ColliderChild = SpawnLocation.transform.Find("MeshColliderEmpty").gameObject;

            ColliderChild.GetComponent<MeshCollider>().sharedMesh = BeachMesh[RandNumber];

            //SpawnLocation.GetComponent<MeshCollider>().sharedMesh = BeachMesh[RandNumber];

            if(RandNumber == 0) {
                //int Offset = (isLeft) ? 100 : -100;

                SpawnLocation.transform.localPosition += new Vector3(100, 0, 0);
            } else if(RandNumber == 2) {
                SpawnLocation.transform.localPosition += new Vector3(-20, 0, 0);
            }

            SpawnLocation.transform.localScale += new Vector3(0, 0, Random.Range(-12, 10));

            //ColliderChild.transform.localScale = SpawnLocation.transform.localScale;
        }

        if(!isLeft)
        {   
            //Vector3 OriginalPosition = gameObject.transform.position;
            //gameObject.transform.position = new Vector3(OriginalPosition.x, OriginalPosition.y, (OriginalPosition.z + 240f));

            LandParent.transform.localRotation = Quaternion.Euler(0, 180, 0);
            LandParent.transform.localPosition = new Vector3(0, 0, 150);
        }

        StartCoroutine(LateStart(.1f));

        // SpawnedBeachObjects.Add(Instantiate(AdditionalTerrainObjects[0], gameObject.transform.position, Quaternion.identity));

        // SpawnedBeachObjects[0].transform.position += new Vector3(0, 150, 75);

        // Ray ray = new Ray((SpawnedBeachObjects[0].transform.position - new Vector3(0, 0, 0)), Vector3.down);
        // if(Physics.Raycast(ray, out RaycastHit info, 400, terrainLayer.value))
        // {
        //     Vector3 CurrentPosition = SpawnedBeachObjects[0].transform.position;
        //     SpawnedBeachObjects[0].transform.position = new Vector3(CurrentPosition.x, info.point.y, CurrentPosition.z);
        //     //Debug.Log(info.point);
        //     HitPosition = info.point;

        //     //info.collider.gameObject.tag = "Respawn";
        // }
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SpawnedBeachObjects.Add(Instantiate(AdditionalTerrainObjects[0], gameObject.transform.position, Quaternion.identity));

        SpawnedBeachObjects[0].transform.position += new Vector3(0, 150, 75);

        Ray ray = new Ray((SpawnedBeachObjects[0].transform.position - new Vector3(0, 0, 0)), Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit info, 400, terrainLayer.value))
        {
            Vector3 CurrentPosition = SpawnedBeachObjects[0].transform.position;
            SpawnedBeachObjects[0].transform.position = new Vector3(CurrentPosition.x, (info.point.y - 10), CurrentPosition.z);
            //Debug.Log(info.point);
            HitPosition = info.point;

            //info.collider.gameObject.tag = "Respawn";
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(HitPosition, 2f);
    }

    void OnDestroy()
    {
        foreach(GameObject AdditionalObject in SpawnedBeachObjects)
        {
            Destroy(AdditionalObject);
        }
    }
}  

