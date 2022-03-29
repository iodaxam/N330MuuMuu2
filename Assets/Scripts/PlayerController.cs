using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // swipe begins
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            startPosition = Camera.main.ScreenToWorldPoint(mousePos);
        }
        if (Input.GetMouseButtonUp(0))    // swipe ends
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            endPosition = Camera.main.ScreenToWorldPoint(mousePos);
        }
 
        if (startPosition != endPosition && startPosition != Vector3.zero && endPosition != Vector3.zero)
        {
            float deltaX = endPosition.x - startPosition.x;
            float deltaY = endPosition.y - startPosition.y;
            if ((deltaX > 5.0f || deltaX < -5.0f) && (deltaY >= -1.0f || deltaY <= 1.0f))
            {
                if (startPosition.x < endPosition.x) // swipe LTR
                {
                    Debug.Log("LTR");
                } else // swipe RTL
                {
                    Debug.Log("RTL");
                }
            }
            startPosition = endPosition = Vector3.zero;
        }
    }

    private void Turn()
    {
        
    }
}
