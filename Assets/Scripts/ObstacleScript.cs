using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
		    Destroy(other.gameObject);
			
		}
	}
}
