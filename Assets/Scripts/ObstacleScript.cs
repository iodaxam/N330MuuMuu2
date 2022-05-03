using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
	public bool FiringObject;
	public bool MovingObject;
	public GameObject FirePrefab;
	public Transform ShootLocation;
	public GameObject PE;

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

			script.Ship = this.gameObject;

			SpawnedObject.SetActive(true);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if(other.GetComponent<PlayerController>().shields > 0)
			{
				Destroy(gameObject, 0.1f);

				Instantiate(PE, gameObject.transform.position, Quaternion.identity);
			}

		    other.SendMessage("Lose");
		}
	}
}
