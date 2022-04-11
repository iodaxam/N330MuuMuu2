using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogParentScript : MonoBehaviour
{
    public Transform PlayerTransform;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(0f, 0f, PlayerTransform.position.z-100);
    }
}
