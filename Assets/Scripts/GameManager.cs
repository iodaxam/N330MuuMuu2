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

    public void GenerateNextSection()
    {
        Debug.Log("Generate");
        GameObject NextSection = PrefabsToSelect[Random.Range(0, PrefabsToSelect.Count - 1)];
        Transform EndTransform = GeneratedTerrain[0].transform.Find("Connectors").gameObject.transform.Find("End").transform;
        Debug.Log(EndTransform);
        
        Destroy(GeneratedTerrain[0]);
        GeneratedTerrain[0] = Instantiate(NextSection, EndTransform.position, Quaternion.identity); //CurrentTscript.EndTransform.position
        
    }
}
