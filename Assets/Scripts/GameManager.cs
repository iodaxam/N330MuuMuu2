using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> PrefabsToSelect;
    public List<GameObject> GeneratedTerrain;

    //public List<int> testList;

    void Start()
    {
        // testList.RemoveAt(1);

        // foreach (int item in testList)
        // {
        //     Debug.Log(item);
        // }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GenerateNextSection(Vector3 StartTransform)
    {
        Debug.Log("Generate");

        //Currently only selects the straight tile. To change, remove the "- 1"
        GameObject NextSection = PrefabsToSelect[Random.Range(0, PrefabsToSelect.Count - 1)];
        Vector3 EndTransform = GeneratedTerrain[0].transform.Find("Connectors").gameObject.transform.Find("End").transform.position;
        
        //This is where the tile is offset
        //More work on this part needed in order to have it offset properly depending on orientation
        Vector3 PrefabLocation = new Vector3(EndTransform.x, EndTransform.y, (EndTransform.z + 45)); 
        
        Destroy(GeneratedTerrain[0]);
        GeneratedTerrain[0] = Instantiate(NextSection, PrefabLocation, Quaternion.identity); //CurrentTscript.EndTransform.position
        
    }
}
