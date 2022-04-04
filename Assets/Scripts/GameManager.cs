using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackDirection
{
    None,
    Straight,
    Left,
    Right
}

public class GameManager : MonoBehaviour
{
    public List<GameObject> PrefabsToSelect;
    public List<GameObject> GeneratedTerrain;

    private float TempTimer;
    public TrackDirection CurrentDirection;

    //public List<int> testList;

    void Start()
    {
        // testList.RemoveAt(1);

        // foreach (int item in testList)
        // {
        //     Debug.Log(item);
        // }

        CurrentDirection = TrackDirection.Straight;
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
        float TurnRotation = 0f;
        bool RightTurn = false;

        if(NextSection == PrefabsToSelect[1]) {
            int Rando = Random.Range(0, 2);
            if(Rando == 0) {
                RightTurn = true;
            } else {
                RightTurn = false;
            }
        }
        
        Debug.Log(CurrentDirection);
        
        switch(CurrentDirection) {
            case TrackDirection.None:
                Debug.Log("--------DIRECTION VALUE IS NONE-----------");
                break;
            case TrackDirection.Straight:
                if(NextSection == PrefabsToSelect[0]) {
                    break;
                } else {
                    if(!RightTurn) {
                        TurnRotation = 90f;
                        CurrentDirection = TrackDirection.Left;
                    } else {
                        CurrentDirection = TrackDirection.Right;
                    }
                }
                break;
            case TrackDirection.Left:
                if(NextSection == PrefabsToSelect[0]) {
                    TurnRotation = 270f;
                    break;
                } else {
                    if(RightTurn) {
                        CurrentDirection = TrackDirection.Straight;
                        TurnRotation = 270f;
                    } else {
                        CurrentDirection = TrackDirection.Straight;
                        break;
                    }
                }
                break;
            case TrackDirection.Right:
                if(NextSection == PrefabsToSelect[0]) {
                    TurnRotation = 90f;
                    break;
                } else {
                    if(RightTurn) {
                        TurnRotation = 90f;
                        CurrentDirection = TrackDirection.Straight;
                    } else {
                        TurnRotation = 180f;
                        CurrentDirection = TrackDirection.Straight;
                        break;
                    }
                }
            break;
        }
        //This is where the tile is offset
        //More work on this part needed in order to have it offset properly depending on orientation
        Vector3 PrefabLocation = new Vector3(EndTransform.x, EndTransform.y, (EndTransform.z)); 
        
        //Destroy(GeneratedTerrain[0]);
        GeneratedTerrain[0] = Instantiate(NextSection, PrefabLocation, Quaternion.Euler(0f, TurnRotation, 0f)); 
        Debug.Log(GeneratedTerrain[0].transform.rotation);
        
    }
}
