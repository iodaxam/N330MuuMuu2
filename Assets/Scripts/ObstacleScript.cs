using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
	public bool FiringObject;
	public bool MovingObject;
	public GameObject FirePrefab;
	public Transform ShootLocation;

	void Start()
	{
		if(FiringObject)
		{
			GameObject SpawnedObject = Instantiate(FirePrefab, gameObject.transform.position, Quaternion.identity);
			
			SpawnedObject.GetComponent<EnemyFireScript>().FireLocation = ShootLocation;

			SpawnedObject.SetActive(true);
		} else if(MovingObject) {
			GameObject SpawnedObject = Instantiate(FirePrefab, gameObject.transform.position, Quaternion.identity);

			EnemyMovingScript script = SpawnedObject.GetComponent<EnemyMovingScript>();

			script.Ship = gameObject;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
		    other.SendMessage("Lose");
		}
	}
}
