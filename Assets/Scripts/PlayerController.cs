using System;
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
    private Vector3 camPos;
    private float dragDistance;  //minimum distance for a swipe to be registered
    public float speed = 5;
    public float rotationSpeed = 150f;
    private Rigidbody rb;
    private Animator anim;
    private float timeRemaining;
    private float xPos = 0;
    public float startSpeed;
    private int skinIndex;

    [Header("References")]
    private GameObject GameManager;
    private int score;
    private int highScore;
    private int coins;
    public Text money;
    private bool gameStarted;
    public GameObject TitleScreen;
    // public GameObject ShopScreen;
    public Text highScoreText;
    public Text scoreText;
    // public GameObject ScoreUI;
    // public GameObject[] skins;
    // public Button PurchaseButton;
    // public Button StartButton;
    private int selectedSkin;

    // public int[] ownedSkins;

    private ThreeLanes CurrentLane = ThreeLanes.Middle;

    private AudioManager AudioManagerScript;
    private float cooldown = .5f;

    public GameObject CannonPrefab;
    public Transform LaunchPosition;

    void Start()
    {
        // ownedSkins = new int[4] {0,0,0,0};
        // for (int i = 0; i < ownedSkins.Length; i++)
        // {
        //     ownedSkins[i] = PlayerPrefs.GetInt("Skin" + i);
        // }
        
        // ShopScreen.SetActive(false);
        coins = PlayerPrefs.GetInt("Money");
        highScore = PlayerPrefs.GetInt("HighScore");
        gameStarted = false;
        score = 0;
        GameManager = GameObject.Find("GameManager");
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
        rb = GetComponent<Rigidbody>();
        money.text = coins.ToString();
        highScoreText.text = "High Score: " + highScore;
        AudioManagerScript = GameManager.GetComponent<AudioManager>();
        anim = GetComponentInChildren<Animator>();
    }
    
    void FixedUpdate()
    {
        if (!gameStarted) return;

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xPos, 0.5f), 0, transform.position.z);

        if(rb.velocity.z < 250f)
        {
            rb.AddRelativeForce(0, 0, 30);
        }
    }

    void Update()
    {
        if(cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        if(gameStarted) {
            anim.SetFloat("Blend", 1, 0.1f, Time.deltaTime);
        } else {
            anim.SetFloat("Blend", .3f, 0.1f, Time.deltaTime);
        }
        // if (gameStarted)
        // {
            // transform.position = new Vector3(Mathf.Lerp(transform.position.x, xPos, 0.5f), 0, transform.position.z);
            // rb.AddRelativeForce(0, 0, 1);
        // }
        score = Mathf.RoundToInt(transform.position.z - 100);
        scoreText.text = score.ToString();
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
                            if(gameStarted){anim.Play("Right Turn");}
                        } else if((endPosition.x < startPosition.x) && (CurrentLane != ThreeLanes.Left))
                        {
                            xPos -= 57;
                            ChangeLane(-57);
                            if(gameStarted){anim.Play("Left Turn");}
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
                        if(endPosition.y > startPosition.y)
                        {
                            GameObject CannonBall = Instantiate(CannonPrefab, LaunchPosition.position, Quaternion.identity);

                            CannonBall.GetComponent<Rigidbody>().AddForce(LaunchPosition.forward * 60000f);

                            Destroy(CannonBall, 4);
                        }
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
        cooldown = .5f;
        rb.velocity = Vector3.zero;
        gameObject.transform.position = new Vector3(0, 0, 100);
        gameStarted = false;
        GameManager.SendMessage("Restart");
        SaveGame();
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HighScore: " + highScore;
        }
        TitleScreen.SetActive(true);
        GameManager.SendMessage("StopFades");
        GameManager.SendMessage("PlayPitched", "CrashSound");
        GameManager.SendMessage("FadeOut", "SailingSound");
        GameManager.SendMessage("FadeOut", "BackgroundMusic");
        AudioManagerScript.FadeIn("MenuSound", .05f, 1f);
    }

    private void MoneyUp()
    {
        coins += 1;
        money.text = coins.ToString();

        GameManager.SendMessage("PlayPitched", "CoinSound");
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Money", coins); 
        PlayerPrefs.SetInt("HighScore", highScore);
        
    //     for (int i = 0; i < ownedSkins.Length; i++)
    //     {
    //         PlayerPrefs.SetInt("Skin"+i, ownedSkins[i]);
    //     }
    }
    
    public void StartGame()
    {
        if(cooldown <= 0) {
            rb.velocity = new Vector3(0, 0, startSpeed);
            gameStarted = true;
            TitleScreen.SetActive(false);
            score = 0;
            GameManager.SendMessage("StopFades");
            GameManager.SendMessage("Play", "BellSound");
            GameManager.SendMessage("FadeOut", "MenuSound");
            AudioManagerScript.FadeIn("BackgroundMusic", .5f, 1f);
            AudioManagerScript.FadeIn("SailingSound", .5f, 8f);
        }
    }

    private void ChangeLane(int PositionChange) 
    {
        GameManager.SendMessage("PlayPitched", "SloshSound");
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

//     public void OpenShop()
//     {  
//         TitleScreen.SetActive(false);
//         ScoreUI.SetActive(false);
//         ShopScreen.SetActive(true);
//     }

//     public void CloseShop()
//     {
//         TitleScreen.SetActive(true);
//         ScoreUI.SetActive(true);
//         ShopScreen.SetActive(false);
//     }

//     public void SelectSkin(int skindex)
//     {
//         if (ownedSkins[skindex] == 1)
//         {
//             PurchaseButton.enabled = false;
//             foreach (GameObject skin in skins)
//             {
//                 skin.SetActive(false);
//             }
//             skins[skindex].SetActive(true);
//         }
//         else
//         {
//             PurchaseButton.enabled = true;
//         }
//         selectedSkin = skindex;
//     }

//     public void PurchaseSkin()
//     {
//         int cost = 25 * (selectedSkin + 1) * (selectedSkin + 1);
//         if (coins >= cost)
//         {
//             coins -= cost;
//         }

//         ownedSkins[selectedSkin] = 1;
//         PurchaseButton.enabled = false;
//         SaveGame();
//     }
}
