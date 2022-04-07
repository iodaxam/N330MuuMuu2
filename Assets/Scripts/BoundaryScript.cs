using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    public List<GameObject> RockObjects;
    public Mesh LandMesh;

    public bool isLand;

    void Start()
    {
        //MeshFilter ObjectsFilter = CubeObject.GetComponent<MeshFilter>();
        //ObjectsFilter.mesh = NewMeshToSet;

        Debug.Log(isLand);
    }
}
