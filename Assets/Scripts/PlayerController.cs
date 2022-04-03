using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector3 startPosition;   //First touch position
    private Vector3 endPosition;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public float speed = 5;
    public float rotationSpeed = 150f;
    private Rigidbody rigidbody;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        rigidbody = GetComponent<Rigidbody>();
    }
 
    void Update()
    {
        //Vector3 localTransform = gameObject.transform.localPosition;
        //transform.Translate(transform.forward * speed * Time.deltaTime);
        rigidbody.AddRelativeForce( 0, 0, 1);
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                startPosition = touch.position;
                endPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                endPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                endPosition = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(endPosition.x - startPosition.x) > dragDistance || Mathf.Abs(endPosition.y - startPosition.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(endPosition.x - startPosition.x) > Mathf.Abs(endPosition.y - startPosition.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        
                        transform.RotateAround(transform.position, transform.up, (endPosition.x > startPosition.x) ? 90 : -90);
                        rigidbody.velocity = Vector3.zero;
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (endPosition.y > startPosition.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }
}
