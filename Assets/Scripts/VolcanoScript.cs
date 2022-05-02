using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoScript : MonoBehaviour
{
    public Transform ExpLocation;
    public GameObject ExplosionPS;
    public bool Obstacle;

    void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Player")
        {
            Instantiate(ExplosionPS, ExpLocation.position, Quaternion.identity);

            if(Obstacle)
            {
                GameObject.Find("GameManager").SendMessage("Play", "VolcanoExplosion2");
            }
        }
    }
}
