using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	private GameObject ObstaclePrefab;
	private ThreeLanes OccupiedSpot1;
	private ThreeLanes OccupiedSpot2;

	private GameObject Rock1;
	private GameObject Rock2;

	public GameObject[] obstaclesFolder;
	public List<GameObject> obstacles;


	void Start()
	{
		obstacles = Resources.LoadAll<GameObject>("Prefabs/Obstacles").ToList();

		ColliderComponent = GetComponent<BoxCollider>();
		GameManager = GameObject.Find("GameManager");

		int RandObstaclePlacement = Random.Range(0, 3);
		ObstaclePrefab = obstacles[Random.Range(0, obstacles.Count)];

		if(!StarterTile) 
		{
			switch(RandObstaclePlacement)
			{
				case 0:
					Rock1 = Instantiate(ObstaclePrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Left;
					break;
				case 1:
					Rock1 = Instantiate(ObstaclePrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Middle;
					break;
				case 2:
					Rock1 = Instantiate(ObstaclePrefab, RightLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Right;
					break;
			}

			RandObstaclePlacement = Random.Range(0, 3);
			ObstaclePrefab = obstacles[Random.Range(0, obstacles.Count)];
			
			switch(RandObstaclePlacement)
			{
				case 0:
					Rock2 = Instantiate(ObstaclePrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Left;
					break;
				case 1:
					Rock2 = Instantiate(ObstaclePrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Middle;
					break;
				case 2:
					Rock2 = Instantiate(ObstaclePrefab, RightLane.position, Quaternion.identity);
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
