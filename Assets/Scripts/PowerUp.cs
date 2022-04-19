using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [Header("Stats")] public string functionName;
    public int amount;

    private BoxCollider collider;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (0, 0, 70) * Time.deltaTime);

        Vector3 currentPos = transform.position;
        float yPos = Mathf.Sin(Time.time);
        transform.position = new Vector3(currentPos.x, currentPos.y + yPos, currentPos.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.SendMessage(functionName, amount);
        Destroy(gameObject);
    }
    
    
}
