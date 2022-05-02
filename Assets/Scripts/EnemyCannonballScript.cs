using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonballScript : MonoBehaviour
{
    public GameObject Rock_ParticleEffect;

    void Start()
    {
        Destroy(gameObject, 5);
        GameObject.Find("GameManager").SendMessage("Play", "EnemyCannon");
    }

    void Update()
    {
        gameObject.transform.position += new Vector3(0, 0, -(300 * Time.deltaTime));
    }
    
    void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Player") || (other.tag == "Obstacle"))
        {
            GameObject PE = Instantiate(Rock_ParticleEffect, other.transform.position, Quaternion.identity);

            Destroy(PE, 2);

            other.SendMessage("Lose", SendMessageOptions.DontRequireReceiver);

            Destroy(gameObject);
        }
    }
}
