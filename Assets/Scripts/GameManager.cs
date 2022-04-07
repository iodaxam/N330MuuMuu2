using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject SectionPrefab;
    public List<GameObject> GeneratedTerrain;
    private int MaxDistanceAhead;

    public List<Transform> StartingPositions;
    
    //public GameObject Test;

    void Start()
    {
        MaxDistanceAhead = GeneratedTerrain.Count;


        // GameObject ThisTest = Instantiate(Test, this.gameObject.transform.position, Quaternion.identity);
        // ThisTest.GetComponent<BoundaryScript>().isLand = true;
        // ThisTest.SetActive(true);
    }

    public void GenerateNextSection(Vector3 StartTransform)
    {
        //Finds the end Gameobject within the last index object
        Vector3 EndTransform = GeneratedTerrain[MaxDistanceAhead - 1].transform.Find("Connectors").gameObject.transform.Find("End").transform.position;
        
        GameObject NextSection = Instantiate(SectionPrefab, EndTransform, Quaternion.identity);
        
        NextSection.SetActive(true);

        //Destroys the section behind the ship
        Destroy(GeneratedTerrain[0]);

        //Shifts all elements in list down one index
        for(int i=0; i < MaxDistanceAhead; i++)
        {
            //So that it doesn't try to access an index that doesn't exist
            int AddedOffset = (i == (MaxDistanceAhead - 1)) ? 0 : 1;

            GeneratedTerrain[i] = GeneratedTerrain[i + AddedOffset];
        }  

        //Sets the newest generated section as the last index
        GeneratedTerrain[MaxDistanceAhead - 1] = NextSection;
    }

    public void Restart()
    {
        foreach(GameObject Section in GeneratedTerrain)
        {
            Destroy(Section);
        }

        int i = 0;

        foreach(Transform StartLocation in StartingPositions)
        {
            GeneratedTerrain[i] = Instantiate(SectionPrefab, StartLocation.position, Quaternion.identity);
            
            if(i <= 1) {
                GeneratedTerrain[i].transform.Find("Connectors").gameObject.transform.Find("Start").gameObject.GetComponent<TerrainScript>().StarterTile =  true;
            }
            
            GeneratedTerrain[i].SetActive(true);
            i++;
        }
    }
}
