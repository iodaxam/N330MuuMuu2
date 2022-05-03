using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biome
{
    Ocean,
    Beach,
    PirateTown,
    GhostYard,
    VolcanoZone
}

public class GameManager : MonoBehaviour
{
    public GameObject SectionPrefab;
    public GameObject BoundaryPrefab;
    public GameObject BeachBoundaryPrefab;
    public GameObject PirateTownBoundaryPrefab;
    public GameObject GhostYardBoundaryPrefab;
    public GameObject VolcanoBoundaryPrefab;

    public List<GameObject> GeneratedTerrain;
    public List<GameObject> GeneratedLeftBoundary;
    public List<GameObject> GeneratedRightBoundary;

    private int MaxDistanceAhead;

    public List<Transform> StartingPositions;

    private int Distance = 1;
    public int ChangeDistance;
    public bool LandSection;
    private int BeachDistance;
    private int PirateTownDistance;
    private int GhostYardDistance;
    private int VolcanoDistance;

    private Biome CurrentBiome;

    public GameObject NotificationObject;

    private GameObject Player;
    private AudioManager AudioScript;

    public float SpeedIncrease = 50f;
    
    void Start()
    {
        MaxDistanceAhead = GeneratedTerrain.Count;

        Player = GameObject.Find("Player");

        AudioScript = gameObject.GetComponent<AudioManager>();

        BeachDistance = ChangeDistance;
        PirateTownDistance = BeachDistance + ChangeDistance;
        GhostYardDistance = PirateTownDistance + ChangeDistance;
        VolcanoDistance = GhostYardDistance + ChangeDistance;
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

            if(CurrentBiome != Biome.Beach)
            {
                StartCoroutine(NotificationObject.GetComponent<NotifScript>().NotifFade(Biome.Beach));

                AudioScript.FadeOutFunc(Player.GetComponent<PlayerController>().CurrentMusic); 

                String Music = "Music3";
                
                PlayerController PlayerScript = Player.GetComponent<PlayerController>();

                PlayerScript.CurrentMusic = Music;

                PlayerScript.SpeedGoal += SpeedIncrease;

                AudioScript.FadeIn(Music, .3f, 1f);

                //AudioScript.FadeIn("Waves", .5f, 1f);
            }

            SelectedBoundary = BeachBoundaryPrefab;

            CurrentBiome = Biome.Beach;

        } else if(Distance < GhostYardDistance) {
            if(CurrentBiome != Biome.PirateTown)
            {
                StartCoroutine(NotificationObject.GetComponent<NotifScript>().NotifFade(Biome.PirateTown));

                AudioScript.FadeOutFunc(Player.GetComponent<PlayerController>().CurrentMusic); 

                String Music = "Music5";
                
                PlayerController PlayerScript = Player.GetComponent<PlayerController>();

                PlayerScript.CurrentMusic = Music;

                PlayerScript.SpeedGoal += SpeedIncrease;

                AudioScript.FadeIn(Music, .3f, 1f);
            }

            SelectedBoundary = PirateTownBoundaryPrefab;

            CurrentBiome = Biome.PirateTown;

        } else if(Distance < VolcanoDistance) {
            if(CurrentBiome != Biome.GhostYard)
            {
                StartCoroutine(NotificationObject.GetComponent<NotifScript>().NotifFade(Biome.GhostYard));

                AudioScript.FadeOutFunc(Player.GetComponent<PlayerController>().CurrentMusic); 

                String Music = "Music4";
                
                PlayerController PlayerScript = Player.GetComponent<PlayerController>();

                PlayerScript.CurrentMusic = Music;

                PlayerScript.SpeedGoal += SpeedIncrease;

                AudioScript.FadeIn(Music, .3f, 1f);
            }

            SelectedBoundary = GhostYardBoundaryPrefab;

            CurrentBiome = Biome.GhostYard;
        } else {
            if(CurrentBiome != Biome.VolcanoZone)
            {
                StartCoroutine(NotificationObject.GetComponent<NotifScript>().NotifFade(Biome.VolcanoZone));

                AudioScript.FadeOutFunc(Player.GetComponent<PlayerController>().CurrentMusic); 

                String Music = "Music2";
                
                PlayerController PlayerScript = Player.GetComponent<PlayerController>();

                PlayerScript.CurrentMusic = Music;

                PlayerScript.SpeedGoal += SpeedIncrease;

                AudioScript.FadeIn(Music, .6f, 1f);
            }

            SelectedBoundary = VolcanoBoundaryPrefab;

            CurrentBiome = Biome.VolcanoZone;
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
            case Biome.GhostYard:
                LeftSection.GetComponent<GhostYardBoundaryScript>().isLeft = true;
                break;
            case Biome.VolcanoZone:
                LeftSection.GetComponent<VolcanoBoundaryScript>().isLeft = true;
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
        NotificationObject.GetComponent<NotifScript>().RestartFadeStats();

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
