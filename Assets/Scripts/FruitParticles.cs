using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitParticles : MonoBehaviour
{
    public GameObject ParticleSystem;

    void OnDestroy() {
        GameObject SpawnedThing = Instantiate(ParticleSystem, gameObject.transform.position, Quaternion.identity);
        Destroy(SpawnedThing, 1f);
    }
}
