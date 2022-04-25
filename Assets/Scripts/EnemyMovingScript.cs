using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingScript : MonoBehaviour
{
    public GameObject Ship;

    private bool startMoving;

    private int MoveDirection;
    
    void Start() {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        if(startMoving)
        {
            Ship.transform.localPosition += ((Vector3.right * 30 * Time.deltaTime) * MoveDirection);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
           startMoving = true;

            MoveDirection = (Ship.transform.rotation.y == 0) ? 1 : -1;
        }
    }
}
