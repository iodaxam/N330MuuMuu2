using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoScript : MonoBehaviour
{
    public Transform ExpLocation;
    public GameObject ExplosionPS;

    void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Player")
        {
            Instantiate(ExplosionPS, ExpLocation.position, Quaternion.identity);

            //GameObject.Find("GameManager").SendMessage("Play", "VolcanoExplosion");
        }
    }
}
