using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    public GameObject Rock_ParticleEffect;
    public GameObject Water_ParticleEffect;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Terrain")
        {
            GameObject PE = Instantiate(Water_ParticleEffect, gameObject.transform.position, Quaternion.identity);

            Destroy(PE, 2);

            Destroy(gameObject);
        } else if(other.tag == "Obstacle")
        {
            Destroy(other.gameObject);

            GameObject PE = Instantiate(Rock_ParticleEffect, other.transform.position, Quaternion.identity);

            Destroy(PE, 2);

            Destroy(gameObject);
        }
    }
}
