using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoScript : MonoBehaviour
{
    public Transform ExpLocation;
    public GameObject ExplosionPS;

    void OnTriggerEnter(Collider other)
	{
       Instantiate(ExplosionPS, ExpLocation.position, Quaternion.identity);
    }
}
