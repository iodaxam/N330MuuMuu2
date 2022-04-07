using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (0, 0, 70) * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.SendMessage("ScoreUp");
            Destroy(gameObject);
        }
    }
}
