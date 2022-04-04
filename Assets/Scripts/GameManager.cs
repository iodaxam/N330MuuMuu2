using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject SectionPrefab;
    public List<GameObject> GeneratedTerrain;
    public int MaxDistanceAhead = 5;

    private float TempTimer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(TempTimer > 0) {
           TempTimer -= Time.deltaTime;
       } else {
           TempTimer = 1f;
           GenerateNextSection(new Vector3(0,0,0));
       }
    }

    public void GenerateNextSection(Vector3 StartTransform)
    {
        Vector3 EndTransform = GeneratedTerrain[MaxDistanceAhead - 1].transform.Find("Connectors").gameObject.transform.Find("End").transform.position;
        
        GameObject NextSection = Instantiate(SectionPrefab, EndTransform, Quaternion.identity); 

        Destroy(GeneratedTerrain[0]);

        for(int i=0; i < MaxDistanceAhead; i++)
        {
            int AddedOffset = (i == (MaxDistanceAhead - 1)) ? 0 : 1;

            GeneratedTerrain[i] = GeneratedTerrain[i + AddedOffset];
        }  

        GeneratedTerrain[MaxDistanceAhead - 1] = NextSection;
    }
}
