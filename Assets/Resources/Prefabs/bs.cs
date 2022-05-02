using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bs : MonoBehaviour
{
    public GameObject VolcanoPrefab;

    private GameObject spawnedThing;
    // Start is called before the first frame update
    void Start()
    {
        spawnedThing = Instantiate(VolcanoPrefab, gameObject.transform.position, Quaternion.identity);

        spawnedThing.GetComponent<VolcanoScript>().Obstacle = true;
    }

    void OnDestroy()
    {
        Destroy(spawnedThing);
    }
}
