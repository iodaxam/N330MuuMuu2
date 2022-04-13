using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    public GameObject ParticleEffect;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Terrain")
        {
            Destroy(gameObject);
        } else if(other.tag == "Obstacle")
        {
            Destroy(gameObject);

            Destroy(other.gameObject);

            GameObject PE = Instantiate(ParticleEffect, other.transform.position, Quaternion.identity);
        }
    }
}
