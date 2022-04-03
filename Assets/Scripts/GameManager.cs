using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> PrefabsToSelect;
    public List<GameObject> GeneratedTerrain;

    private float TempTimer;

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
       if(TempTimer > 0) {
           TempTimer -= Time.deltaTime;
       } else {
           TempTimer = 1f;
           GenerateNextSection(new Vector3(0,0,0));
       }
    }

    public void GenerateNextSection(Vector3 StartTransform)
    {
        //Currently only selects the straight tile. To change, remove the "- 1"
        GameObject NextSection = PrefabsToSelect[Random.Range(0, PrefabsToSelect.Count)];
        Vector3 EndTransform = GeneratedTerrain[0].transform.Find("Connectors").gameObject.transform.Find("End").transform.position;
        bool RightTurn;
        float TurnRotation = 0f;

        if(NextSection == PrefabsToSelect[1]) {
            Debug.Log("Test");
            int Rando = Random.Range(0, 2);
            if(Rando == 0) {
                RightTurn = true;
                TurnRotation = 0f;
            } else {
                RightTurn = false;
                TurnRotation = 90f;
            }
        }
        
        //This is where the tile is offset
        //More work on this part needed in order to have it offset properly depending on orientation
        Vector3 PrefabLocation = new Vector3(EndTransform.x, EndTransform.y, (EndTransform.z)); 
        
        //Destroy(GeneratedTerrain[0]);
        GeneratedTerrain[0] = Instantiate(NextSection, PrefabLocation, new Quaternion(0f, TurnRotation, 0f, 1f)); 
        Debug.Log(GeneratedTerrain[0].transform.rotation);
        
    }
}
