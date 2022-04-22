using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShipRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 ObjectRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        gameObject.transform.rotation = Quaternion.Euler(ObjectRotation);
    }
}
