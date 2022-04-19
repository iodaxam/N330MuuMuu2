using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
		    other.SendMessage("Lose");
		}
	}
}
