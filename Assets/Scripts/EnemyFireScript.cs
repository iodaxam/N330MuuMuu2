using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireScript : MonoBehaviour
{
    public GameObject CannonballPrefab;
    public Transform FireLocation;
    public GameObject ExplosionParticle;

    void Start() {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            GameObject CB = Instantiate(CannonballPrefab, FireLocation.position, Quaternion.identity);
            Destroy(CB, 6);
            GameObject PE = Instantiate(ExplosionParticle, FireLocation.position, Quaternion.identity);
            Destroy(PE, 2);
        }
    }
}
