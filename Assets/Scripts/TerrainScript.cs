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

	public int percentChanceOfPowerUp;
	public GameObject Coin;
	private GameObject Obstacle1;
	private GameObject Obstacle2;

	//private GameObject[] obstaclesFolder;
	//private GameObject[] powerUpsFolder;
	private List<GameObject> obstacles;
	private List<GameObject> powerUps;



	void Start()
	{
		powerUps = Resources.LoadAll<GameObject>("Prefabs/PowerUps").ToList();
		obstacles = Resources.LoadAll<GameObject>("Prefabs/Obstacles").ToList();

		ColliderComponent = GetComponent<BoxCollider>();
		GameManager = GameObject.Find("GameManager");


		int RandObstaclePlacement = Random.Range(0, 3);
		if (Random.Range(0, 100) <= percentChanceOfPowerUp)
		{
			ObstaclePrefab = powerUps[Random.Range(0, powerUps.Count)];
		}
		else
		{
			ObstaclePrefab = obstacles[Random.Range(0, obstacles.Count)];	
		}

		if(!StarterTile) 
		{
			switch(RandObstaclePlacement)
			{
				case 0:
					Obstacle1 = Instantiate(ObstaclePrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Left;
					break;
				case 1:
					Obstacle1 = Instantiate(ObstaclePrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Middle;
					break;
				case 2:
					Obstacle1 = Instantiate(ObstaclePrefab, RightLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Right;
					break;
			}

			RandObstaclePlacement = Random.Range(0, 3);
			ObstaclePrefab = obstacles[Random.Range(0, obstacles.Count)];
			
			switch(RandObstaclePlacement)
			{
				case 0:
					Obstacle2 = Instantiate(ObstaclePrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Left;
					break;
				case 1:
					Obstacle2 = Instantiate(ObstaclePrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Middle;
					break;
				case 2:
					Obstacle2 = Instantiate(ObstaclePrefab, RightLane.position, Quaternion.identity);
					OccupiedSpot2 = ThreeLanes.Right;
					break;
			}

			if(OccupiedSpot1 == OccupiedSpot2) 
			{
				Destroy(Obstacle2.gameObject);
			}
		}
		// Animator krakenAnim = Instantiate(obstacles[1].GetComponentInChildren<Animator>());
	}

	void OnDestroy() 
	{
		Destroy(Obstacle1);

		if (Obstacle2 != null)
		{
			Destroy(Obstacle2);
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
