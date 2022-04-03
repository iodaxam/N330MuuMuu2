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
        GameObject NextSection = PrefabsToSelect[Random.Range(0, PrefabsToSelect.Count)];

        TerrainScript NextTscript = NextSection.GetComponent<TerrainScript>();
        TerrainScript CurrentTscript = GeneratedTerrain[0].GetComponent<TerrainScript>();
        
        Instantiate(NextSection, CurrentTscript.EndTransform.position, Quaternion.identity);
        
    }
}
