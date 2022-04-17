using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject CollectPE;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (0, 0, 70) * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.SendMessage("MoneyUp");

            GameObject ParticleEffect = Instantiate(CollectPE, gameObject.transform.position, Quaternion.identity);

            Destroy(ParticleEffect, 1);

            Destroy(gameObject);
        }
    }
}
