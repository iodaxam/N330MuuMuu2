using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThreeLanes
{
	None,
	Left,
	Middle,
	Right
}

public class TerrainScript : MonoBehaviour
{
	public bool StarterTile;

	private BoxCollider ColliderComponent;
	private GameObject GameManager;

	public Transform StartTransform;
	public Transform EndTransform;

	public Transform LeftLane;
	public Transform MiddleLane;
	public Transform RightLane;

	public GameObject RockPrefab;
	private ThreeLanes OccupiedSpot1;
	private ThreeLanes OccupiedSpot2;

	private GameObject Rock1;
	private GameObject Rock2;


	void Start()
	{
		ColliderComponent = GetComponent<BoxCollider>();
		GameManager = GameObject.Find("GameManager");

		int RandRockNumber1 = Random.Range(0,3);
		int RandRockNumber2 = Random.Range(0,3);
	
		if(!StarterTile) 
		{
			switch(RandRockNumber1)
			{
				case 0:
					Rock1 = Instantiate(RockPrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Left;
					break;
				case 1:
					Rock1 = Instantiate(RockPrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Middle;
					break;
				case 2:
					Rock1 = Instantiate(RockPrefab, RightLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Right;
					break;
			}

			switch(RandRockNumber2)
			{
				case 0:
					Rock2 = Instantiate(RockPrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Left;
					break;
				case 1:
					Rock2 = Instantiate(RockPrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Middle;
					break;
				case 2:
					Rock2 = Instantiate(RockPrefab, RightLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Right;
					break;
			}

			if(OccupiedSpot1 == OccupiedSpot2) 
			{
				Destroy(Rock2.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			GameManager.SendMessage("GenerateNextSection", StartTransform.position);
			
		}
	}
}
