using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biome
{
    Ocean,
    Beach,
    PirateTown
}

public class GameManager : MonoBehaviour
{
    public GameObject SectionPrefab;
    public GameObject BoundaryPrefab;
    public GameObject BeachBoundaryPrefab;
    public GameObject PirateTownBoundaryPrefab;

    public List<GameObject> GeneratedTerrain;
    public List<GameObject> GeneratedLeftBoundary;
    public List<GameObject> GeneratedRightBoundary;

    private int MaxDistanceAhead;

    public List<Transform> StartingPositions;

    private int Distance = 1;
    public int ChangeDistance = 3;
    public bool LandSection;
    public int BeachDistance = 5;
    public int PirateTownDistance = 10;

    private Biome CurrentBiome;
    
    void Start()
    {
        MaxDistanceAhead = GeneratedTerrain.Count;
    }

    public void GenerateNextSection(Vector3 StartTransform)
    {
        
        // if((Distance % ChangeDistance) == 0) 
        // {
        //     LandSection = (!LandSection) ? true : false;
        // }
        
        GameObject SelectedBoundary  = BoundaryPrefab;

        if(Distance < BeachDistance) {

            SelectedBoundary = BoundaryPrefab;

            CurrentBiome = Biome.Ocean;

        } else if(Distance < PirateTownDistance) {

            SelectedBoundary = BeachBoundaryPrefab;

            CurrentBiome = Biome.Beach;

        } else {
            SelectedBoundary = PirateTownBoundaryPrefab;

            CurrentBiome = Biome.PirateTown;
        }
    
        Distance++;
        //Finds the end Gameobject within the last index object
        Vector3 EndTransform = GeneratedTerrain[MaxDistanceAhead - 1].transform.Find("Connectors").gameObject.transform.Find("End").transform.position;
        
        GameObject NextSection = Instantiate(SectionPrefab, EndTransform, Quaternion.identity);

        NextSection.transform.Find("Connectors").transform.Find("Start").GetComponent<TerrainScript>().WorldBiome = CurrentBiome;

        GameObject LeftSection = Instantiate(SelectedBoundary, new Vector3((EndTransform.x - 180), EndTransform.y, EndTransform.z), Quaternion.identity);
        
        switch(CurrentBiome)
        {
            case Biome.Ocean:
                LeftSection.GetComponent<BoundaryScript>().isLeft = true;
                break;
            case Biome.Beach:
                LeftSection.GetComponent<BoundaryScriptBeach>().isLeft = true;
                break;
            case Biome.PirateTown:
                LeftSection.GetComponent<PirateTownBoundaryScript>().isLeft = true;
                break;
        }

        GameObject RightSection = Instantiate(SelectedBoundary, new Vector3((EndTransform.x + 180), EndTransform.y, EndTransform.z), Quaternion.identity);

        // if(LandSection)
        // {
        //     LeftSection.GetComponent<BoundaryScript>().isLand = true;
        //     RightSection.GetComponent<BoundaryScript>().isLand = true;
        // }
        
        NextSection.SetActive(true);
        LeftSection.SetActive(true);
        RightSection.SetActive(true);

        //Destroys the section behind the ship
        Destroy(GeneratedTerrain[0]);
        Destroy(GeneratedLeftBoundary[0]);
        Destroy(GeneratedRightBoundary[0]);

        //Shifts all elements in list down one index
        for(int i=0; i < MaxDistanceAhead; i++)
        {
            //So that it doesn't try to access an index that doesn't exist
            int AddedOffset = (i == (MaxDistanceAhead - 1)) ? 0 : 1;

            GeneratedTerrain[i] = GeneratedTerrain[i + AddedOffset];
            GeneratedLeftBoundary[i] = GeneratedLeftBoundary[i + AddedOffset];
            GeneratedRightBoundary[i] = GeneratedRightBoundary[i + AddedOffset];
        }  

        //Sets the newest generated section as the last index
        GeneratedTerrain[MaxDistanceAhead - 1] = NextSection;
        GeneratedLeftBoundary[MaxDistanceAhead - 1] = LeftSection;
        GeneratedRightBoundary[MaxDistanceAhead - 1] = RightSection;
    }

    public void Restart()
    {
        Distance = 1;
        int i = 0;

        foreach(GameObject Section in GeneratedTerrain)
        {
            Destroy(Section);
            //Destroy(GeneratedLeftBoundary[i]);
            //Destroy(GeneratedRightBoundary[i]);
            i++;
            //Debug.Log(i);
        }
        foreach(GameObject section in GeneratedLeftBoundary)
        {
            Destroy(section);
        }

        foreach(GameObject section in GeneratedRightBoundary)
        {
            Destroy(section);
        }

        i = 0;

        foreach(Transform StartLocation in StartingPositions)
        {
            Vector3 LocationPosition = StartLocation.position;

            GeneratedTerrain[i] = Instantiate(SectionPrefab, StartLocation.position, Quaternion.identity);
            
            GeneratedLeftBoundary[i] = Instantiate(BoundaryPrefab, new Vector3((LocationPosition.x - 180), LocationPosition.y, LocationPosition.z), Quaternion.identity);

            GeneratedLeftBoundary[i].GetComponent<BoundaryScript>().isLeft = true;

            GeneratedRightBoundary[i] = Instantiate(BoundaryPrefab, new Vector3((LocationPosition.x + 180), LocationPosition.y, LocationPosition.z), Quaternion.identity);
            
            if(i <= 1) {
                GeneratedTerrain[i].transform.Find("Connectors").gameObject.transform.Find("Start").gameObject.GetComponent<TerrainScript>().StarterTile =  true;
            }

            GeneratedTerrain[i].SetActive(true);
            GeneratedLeftBoundary[i].SetActive(true);
            GeneratedRightBoundary[i].SetActive(true);

            i++;
            //Debug.Log(i);
        }



    }
}
