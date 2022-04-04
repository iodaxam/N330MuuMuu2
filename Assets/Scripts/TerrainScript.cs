using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	void Start()
	{
		ColliderComponent = GetComponent<BoxCollider>();
		GameManager = GameObject.Find("GameManager");

		int RandRockNumber = Random.Range(0,3);
	
		if(!StarterTile) 
		{
			switch(RandRockNumber)
			{
				case 0:
					Instantiate(RockPrefab, LeftLane.position, Quaternion.identity);
					break;
				case 1:
					Instantiate(RockPrefab, MiddleLane.position, Quaternion.identity);
					break;
				case 2:
					Instantiate(RockPrefab, RightLane.position, Quaternion.identity);
					break;
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
