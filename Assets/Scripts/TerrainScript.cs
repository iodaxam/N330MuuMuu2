using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

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

	public Biome WorldBiome;

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

	public GameObject CoinPrefab;

	public GameObject MissionGameObject;



	void Start()
	{
		MissionGameObject = GameObject.Find("MissionObject");

		switch(WorldBiome)
		{
			case Biome.Ocean:
				obstacles = Resources.LoadAll<GameObject>("Prefabs/ObstaclesOcean").ToList();
				break;
			case Biome.Beach:
				obstacles = Resources.LoadAll<GameObject>("Prefabs/ObstaclesBeach").ToList();
				break;
			case Biome.PirateTown:
				obstacles = Resources.LoadAll<GameObject>("Prefabs/ObstaclesPirateTown").ToList();
				break;
			case Biome.GhostYard:
				obstacles = Resources.LoadAll<GameObject>("Prefabs/GhostYardObstacles").ToList();
				break;
		}

		powerUps = Resources.LoadAll<GameObject>("Prefabs/PowerUps").ToList();

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

		
		if(WorldBiome == Biome.PirateTown)
		{
			List<Transform> AvailableSpots = new List<Transform>();
			switch(RandObstaclePlacement)
			{
				case 0:
					Obstacle1 = Instantiate(ObstaclePrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Left;
					AvailableSpots.Add(MiddleLane);
					AvailableSpots.Add(RightLane);
					break;
				case 1:
					if(ObstaclePrefab == obstacles[3]) {
						obstacles.RemoveAt(3);
						ObstaclePrefab = obstacles[Random.Range(0, obstacles.Count)];
					}
					Obstacle1 = Instantiate(ObstaclePrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Middle;
					AvailableSpots.Add(LeftLane);
					AvailableSpots.Add(RightLane);
					break;
				case 2:
					Obstacle1 = Instantiate(ObstaclePrefab, RightLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Right;
					AvailableSpots.Add(LeftLane);
					AvailableSpots.Add(MiddleLane);

					if(ObstaclePrefab == obstacles[3]) {
						Obstacle1.transform.rotation = Quaternion.Euler(0, 180, 0);
					}
					break;
			}

			Obstacle2 = Instantiate(CoinPrefab, AvailableSpots[Random.Range(0, AvailableSpots.Count)].position, Quaternion.identity);

		} else if(WorldBiome == Biome.GhostYard) {
			
			List<Transform> AvailableSpots = new List<Transform>();
			switch(RandObstaclePlacement)
			{
				case 0:
					Obstacle1 = Instantiate(ObstaclePrefab, LeftLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Left;
					AvailableSpots.Add(MiddleLane);
					AvailableSpots.Add(RightLane);
					break;
				case 1:
					Obstacle1 = Instantiate(ObstaclePrefab, MiddleLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Middle;
					AvailableSpots.Add(LeftLane);
					AvailableSpots.Add(RightLane);
					break;
				case 2:
					Obstacle1 = Instantiate(ObstaclePrefab, RightLane.position, Quaternion.identity);
					OccupiedSpot1 = ThreeLanes.Right;
					AvailableSpots.Add(LeftLane);
					AvailableSpots.Add(MiddleLane);
					break;
			}

			Obstacle2 = Instantiate(CoinPrefab, AvailableSpots[Random.Range(0, AvailableSpots.Count)].position, Quaternion.identity);
		} else {
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

			switch(WorldBiome)
			{
				case Biome.Ocean:
					MissionGameObject.GetComponent<TextMeshProUGUI>().text = "Current Mission: Reach the Beach";
					break;
				case Biome.Beach:
					MissionGameObject.GetComponent<TextMeshProUGUI>().text = "Current Mission: Reach the PirateTown";
					break;
				case Biome.PirateTown:
					MissionGameObject.GetComponent<TextMeshProUGUI>().text = "Current Mission: Reach the ship graveyard";
					break;
				case Biome.GhostYard:
					MissionGameObject.GetComponent<TextMeshProUGUI>().text = "Current Mission: Reach the Volcano";
					break;
			}
			
		}
	}
}
