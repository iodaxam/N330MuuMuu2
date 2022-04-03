using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> PrefabsToSelect;
    public List<GameObject> GeneratedTerrain;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GenerateNextSection()
    {
        GameObject NextSection = PrefabsToSelect[Random.Range(0, PrefabsToSelect.Count)];
        
    }
}
