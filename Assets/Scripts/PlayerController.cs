using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector3 startPosition;   //First touch position
    private Vector3 endPosition;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public float speed = 5;
    public float rotationSpeed = 150f;
    private Rigidbody rigidbody;
    private float timeRemaining;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        rigidbody = GetComponent<Rigidbody>();
    }
 
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            startPosition = endPosition = Vector3.zero;
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, (endPosition.x > startPosition.x) ? 30 : -30, 0.5f), 0, transform.position.z);
        //Vector3 localTransform = gameObject.transform.localPosition;
        //transform.Translate(transform.forward * speed * Time.deltaTime);
        rigidbody.AddRelativeForce( 0, 0, 1);
        Debug.Log(rigidbody.velocity);
        if (Input.touchCount != 1) return;
        Touch touch = Input.GetTouch(0); // get the touch
        switch (touch.phase)
        {
            //check for the first touch
            case TouchPhase.Began:
                startPosition = touch.position;
                endPosition = touch.position;
                break;
            // update the last position based on where they moved
            case TouchPhase.Moved:
                endPosition = touch.position;
                break;
            //check if the finger is removed from the screen
            case TouchPhase.Ended:
            {
                endPosition = touch.position;  //last touch position.
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(endPosition.x - startPosition.x) > dragDistance || Mathf.Abs(endPosition.y - startPosition.y) > dragDistance)
                {//It's a drag
                    //check if the drag is vertical or horizontal
                    if (Mathf.Abs(endPosition.x - startPosition.x) > Mathf.Abs(endPosition.y - startPosition.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        timeRemaining = 0.5f;
                    }
                    else
                    {
                        //the vertical movement is greater than the horizontal movement
                        Debug.Log(endPosition.y > startPosition.y ? "Up Swipe" : "Down Swipe");
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }

                break;
            }
        }
        
    }
}
