using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingScript : MonoBehaviour
{
    public GameObject Ship;

    //one for right and negative 1 for left
    public float MoveDirection;
    
    void Start() {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
           
        }
    }
}
