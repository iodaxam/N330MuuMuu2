using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotationScript : MonoBehaviour
{
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (0, RotationSpeed, 0) * Time.deltaTime);
    }
}
