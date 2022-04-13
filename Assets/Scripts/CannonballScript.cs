using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Terrain")
        {
            Destroy(gameObject);
        } else if(other.tag == "Obstacle")
        {
            Destroy(gameObject);

            Destroy(other.gameObject);
        }
    }
}
