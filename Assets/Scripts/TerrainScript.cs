using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
	private BoxCollider ColliderComponent;
	private GameObject GameManager;

	void Start()
	{
		ColliderComponent = GetComponent<BoxCollider>();
		GameManager = GameObject.Find("GameManager");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//GameManager.SendMessage("GenerateNextSection");
		}
	}
}
