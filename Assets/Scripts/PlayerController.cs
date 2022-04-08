using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    private Vector3 startPosition;   //First touch position
    private Vector3 endPosition;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public float speed = 5;
    public float rotationSpeed = 150f;
    private Rigidbody rigidbody;
    private float timeRemaining;
    private float xPos = 0;
    public float startSpeed;
    private int skinIndex;

    [Header("References")]
    private GameObject GameManager;
    private int score;
    private int coins;
    public Text money;
    private bool gameStarted;
    public GameObject TextCanvas;

    private ThreeLanes CurrentLane = ThreeLanes.Middle;

    void Start()
    {
        coins = PlayerPrefs.GetInt("Money");
        gameStarted = false;
        score = 0;
        GameManager = GameObject.Find("GameManager");
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
            if (gameStarted)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, xPos, 0.5f), 0, transform.position.z);
            rigidbody.AddRelativeForce(0, 0, 10);
        }
    }

    void Update()
    {
        // if (gameStarted)
        // {
        //     transform.position = new Vector3(Mathf.Lerp(transform.position.x, xPos, 0.5f), 0, transform.position.z);
        //     rigidbody.AddRelativeForce(0, 0, 1);
        // }

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
                    {   //If the horizontal movement is greater than the vertical movement
                        if((endPosition.x > startPosition.x) && (CurrentLane != ThreeLanes.Right))
                        {
                            xPos += 57;
                            ChangeLane(57);
                        } else if((endPosition.x < startPosition.x) && (CurrentLane != ThreeLanes.Left))
                        {
                            xPos -= 57;
                            ChangeLane(-57);
                        } else if(CurrentLane == ThreeLanes.Middle)
                        {
                            int ChangeAmount = (endPosition.x > startPosition.x) ? 57 : -57;

                            xPos += ChangeAmount;

                            ChangeLane(ChangeAmount);
                        }
                    }
                    else
                    {
                        //the vertical movement is greater than the horizontal movement
                        Debug.Log(endPosition.y > startPosition.y ? "Up Swipe" : "Down Swipe");
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 15% of the screen height
                    Debug.Log("Tap");
                    if (!gameStarted)
                    {
                        StartGame();
                    }
                }

                break;
            }
        }
    }

    private void Lose()
    {
        rigidbody.velocity = Vector3.zero;
        gameObject.transform.position = new Vector3(0, 0, 65.3f);
        gameStarted = false;
        GameManager.SendMessage("Restart");
        SaveGame();
        TextCanvas.SetActive(true);
    }

    private void MoneyUp()
    {
        coins += 1;
        money.text = coins.ToString();
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Money", coins); 
    }
    
    private void StartGame()
    {
        rigidbody.velocity = new Vector3(0, 0, startSpeed);
        gameStarted = true;
        TextCanvas.SetActive(false);

    }

    private void ChangeLane(int PositionChange) 
    {
        switch(CurrentLane)
        {
            case ThreeLanes.None:
                Debug.Log("Lane set to none");
                break;

            case ThreeLanes.Left:
                CurrentLane = ThreeLanes.Middle;
                break;
            case ThreeLanes.Middle:
                if(PositionChange == 57)
                {
                    CurrentLane = ThreeLanes.Right;
                } else {
                    CurrentLane = ThreeLanes.Left;
                }
                break;
            case ThreeLanes.Right:
                CurrentLane = ThreeLanes.Middle;
                break;
        }
    }
}
