using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    private Vector3 startPosition;   //First touch position
    private Vector3 endPosition;   //Last touch position
    private Vector3 camPos;
    private float dragDistance;  //minimum distance for a swipe to be registered

    public float speed = 5;
    public bool isDead = false;
    private float defSpeed;
    private Rigidbody rb;
    private Animator anim;
    public GameObject kraken;
    private float timeRemaining;
    private float xPos = 0;
    public float startSpeed;
    private int skindex;
    public int laneDistance = 57;
    private int skinIndex;
    private int recoilForce;
    private int shields;
    private int currentCoins;

    [Header("References")]
    private Kraken kr;
    private GameObject GameManager;
    private int score;
    private int highScore;
    private int coins;
    public Text money;
    private bool gameStarted;
    public GameObject TitleScreen;
    public GameObject ShopScreen;
    public Text highScoreText;
    public Text scoreText;
    public Text shipCost;
    public Text currentCoinsText;
    public GameObject inGameMenu;
    // public GameObject ScoreUI;
    // public Button PurchaseButton;
    // public Button StartButton;
    public GameObject[] skins;

    private int[] ownedSkins;

    private ThreeLanes CurrentLane = ThreeLanes.Middle;

    private AudioManager AudioManagerScript;
    private float cooldown = .5f;

    public GameObject CannonPrefab;
    public Transform LaunchPosition;
    public GameObject CannonPE;

    private bool disableInput = false;
    private int i;
    public int MaxBalls = 3;
    private int CurrentBalls = 3;

    public void Start()
    {
        ownedSkins = new int[5] {1,0,0,0,0};
        for (int i = 0; i < ownedSkins.Length; i++)
        {
            ownedSkins[i] = PlayerPrefs.GetInt("Skin" + i);
        }
        
        coins = PlayerPrefs.GetInt("Money");
        // coins = 9999; // uncomment for free money
        highScore = PlayerPrefs.GetInt("HighScore");
        gameStarted = false;
        score = 0;
        GameManager = GameObject.Find("GameManager");
        dragDistance = Screen.height * 2/ 100; //dragDistance is 10% height of the screen
        rb = GetComponent<Rigidbody>();
        money.text = coins.ToString();
        highScoreText.text = "High Score: " + highScore;
        AudioManagerScript = GameManager.GetComponent<AudioManager>();
        ShopScreen.SetActive(false);
        inGameMenu.SetActive(false);

    }

    private void SpeedUp()
    {
            if (rb.velocity.magnitude < 400) {
                rb.AddRelativeForce(0, 0, speed*(Mathf.Log(speed)*2));
            } else if (rb.velocity.magnitude < 600) {
                // StartCoroutine(speedTimer());
                rb.AddRelativeForce(0, 0, speed/(Mathf.Log(speed, 2)*2));
            } else if (rb.velocity.magnitude < 1000) {
                rb.AddRelativeForce(0, 0, speed/Mathf.Exp(speed));
            }
    }

    // IEnumerator speedTimer()
    // {
    //     float i = Random.Range(5.0f, 20.0f);
    //     yield return new WaitForSeconds(i);
    // }

    private void FixedUpdate()
    {
        if (!gameStarted) return;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xPos, 0.5f), 0, transform.position.z);
        if (!isDead){
            SpeedUp();
        }
    }

    private void Update()
    {

        anim = GetComponentInChildren<Animator>();
        if(cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        if (gameStarted) {
            anim.SetFloat("Blend", 1, 0.1f, Time.deltaTime);
        } else {
            anim.SetFloat("Blend", 0.3f, 0.1f, Time.deltaTime);
        }
        
        score = Mathf.RoundToInt(transform.position.z - 100) / 10 * (skindex + 1);
        scoreText.text = score.ToString();

        if (!isDead)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("KeyPressed");
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                {
                    if (Input.GetKey(KeyCode.RightArrow) && CurrentLane != ThreeLanes.Right)
                    {
                        if (!gameStarted)
                        {
                            SelectSkin(1);
                        }
                        else
                        {
                            ChangeLane(laneDistance);
                            anim.Play("Right Turn");
                        }
                    }
                    else if (Input.GetKey(KeyCode.LeftArrow) && CurrentLane != ThreeLanes.Left)
                    {
                        if (!gameStarted)
                        {
                            SelectSkin(-1);
                        }
                        else
                        {
                            ChangeLane(-laneDistance);
                            anim.Play("Left Turn");
                        }
                    }
                    else if (CurrentLane == ThreeLanes.Middle)
                    {
                        if (Input.GetKey(KeyCode.RightArrow))
                        {
                            xPos += laneDistance;
                            ChangeLane(laneDistance);
                        }
                        else if (Input.GetKey(KeyCode.LeftArrow))
                        {
                            xPos -= laneDistance;
                            ChangeLane(-laneDistance);
                        }
                    }
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        if (gameStarted && CurrentBalls > 0)
                        {
                            GameObject cannonBall =
                                Instantiate(CannonPrefab, LaunchPosition.position, Quaternion.identity);

                            cannonBall.GetComponent<Rigidbody>().AddForce(LaunchPosition.forward * 60000f);

                            GameObject PE = Instantiate(CannonPE, LaunchPosition.position, Quaternion.identity);

                            Destroy(PE, 2);

                            Destroy(cannonBall, 4);

                            CurrentBalls--;
                        }
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        if (!gameStarted)
                        {
                            if (ownedSkins[skindex] == 1)
                            {
                                StartGame();
                            }
                            else
                            {
                                PurchaseSkin(skindex);
                            }
                        }
                    }
                }
        }
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
                    //Check if drag distance is greater than 10% of the screen height
                    if (Mathf.Abs(endPosition.x - startPosition.x) > dragDistance || Mathf.Abs(endPosition.y - startPosition.y) > dragDistance)
                    {//It's a drag
                        //check if the drag is vertical or horizontal
                        if (Mathf.Abs(endPosition.x - startPosition.x) > Mathf.Abs(endPosition.y - startPosition.y))
                        {   //If the horizontal movement is greater than the vertical movement
                            if((endPosition.x > startPosition.x) && (CurrentLane != ThreeLanes.Right))
                            {
                                if (!gameStarted)
                                {
                                    SelectSkin(1);
                                }
                                else
                                {
                                    ChangeLane(laneDistance);
                                    anim.Play("Right Turn");
                                }
                            } else if((endPosition.x < startPosition.x) && (CurrentLane != ThreeLanes.Left))
                            {
                                if (!gameStarted)
                                {
                                    SelectSkin(-1);
                                }
                                else
                                {
                                    ChangeLane(-laneDistance);
                                    anim.Play("Left Turn");
                                }
                            } else if(CurrentLane == ThreeLanes.Middle)
                            {
                                int ChangeAmount = (endPosition.x > startPosition.x) ? 57 : -57;                     
                                xPos += ChangeAmount;

                                ChangeLane(ChangeAmount);
                            }
                        }
                        else
                        { //the vertical movement is greater than the horizontal movement
                            if(CurrentBalls > 0)
                            {
                                if(endPosition.y > startPosition.y)
                                {
                                    GameObject cannonBall = Instantiate(CannonPrefab, LaunchPosition.position, Quaternion.identity);

                                    cannonBall.GetComponent<Rigidbody>().AddForce(LaunchPosition.forward * 60000f);

                                    GameObject PE = Instantiate(CannonPE, LaunchPosition.position, Quaternion.identity);

                                    Destroy(PE, 2);

                                    Destroy(cannonBall, 4);

                                    CurrentBalls--;
                                }
                            } 
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 15% of the screen height
                        Debug.Log("Tap");
                        if (!gameStarted)
                        {
                            if (ownedSkins[skindex] == 1)
                            {
                                StartGame();
                            }
                            else
                            {
                                PurchaseSkin(skindex);
                            }
                        }
                    }

                    break;
                }
            }
        }
    }

    private void Lose()
    {
        if (shields > 0)
        {
            shields -= 1;
            // animation / particles here?
        }
        else
        {
            StartCoroutine(deathTimer());
            rb.velocity = Vector3.zero;
            cooldown = .5f;
            GameManager.SendMessage("PlayPitched", "CrashSound");
            GameManager.SendMessage("StopFades");
            GameManager.SendMessage("FadeOut", "SailingSound");
            GameManager.SendMessage("FadeOut", "BackgroundMusic");
            isDead = true;
            startPosition = endPosition = Vector3.zero; // This is to fix the odd issue where the play can no longer swipe one direction until the game is started again.
        }
    }

    public void Kraken()
    {
        Lose();
    }

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(1.25f);
        gameStarted = false;
        gameObject.transform.position = new Vector3(0, 0, 100);
        GameManager.SendMessage("Restart");
        SaveGame();
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HighScore: " + highScore;
        }
        AudioManagerScript.FadeIn("MenuSound", .05f, 1f);
        TitleScreen.SetActive(true);
        inGameMenu.SetActive(false);
        CurrentBalls = 3;
        isDead = false;
        anim.SetFloat("Blend", 1, 0.1f, Time.deltaTime);
        
        currentCoins = 0;
        currentCoinsText.text = currentCoins.ToString();
    }

    private void MoneyUp(int amount)
    {
        coins += amount;
        currentCoins += amount;
        currentCoinsText.text = currentCoins.ToString();
        money.text = coins.ToString();

        GameManager.SendMessage("PlayPitched", "CoinSound");
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Money", coins); 
        PlayerPrefs.SetInt("HighScore", highScore);
        
        for (int i = 0; i < ownedSkins.Length; i++)
        {
            PlayerPrefs.SetInt("Skin"+i, ownedSkins[i]);
        }
    }
    
    public void StartGame()
    {
        if(cooldown <= 0) {
            rb.velocity = new Vector3(0, 0, startSpeed);
            gameStarted = true;
            TitleScreen.SetActive(false);
            inGameMenu.SetActive(true);
            score = 0;
            GameManager.SendMessage("StopFades");
            GameManager.SendMessage("Play", "BellSound");
            GameManager.SendMessage("FadeOut", "MenuSound");
            AudioManagerScript.FadeIn("BackgroundMusic", .5f, 1f);
            AudioManagerScript.FadeIn("SailingSound", .5f, 8f);
            skins[skindex].transform.localRotation = Quaternion.identity;
        }
    }

    private void ChangeLane(int PositionChange)
    {
        xPos += PositionChange;
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

     private void SelectSkin(int direction)
     {
         skins[skindex].transform.localRotation = Quaternion.identity;
         skins[skindex].SetActive(false);
         skindex -= direction;
         if (skindex < 0)
         {
             skindex = 4;
         }
         else if(skindex > 4)
         {
             skindex = 0;
         }
        skins[skindex].SetActive(true);
        skins[skindex].transform.localRotation = Quaternion.identity;
        //Debug.Log(skins[skindex] + "" + skindex);
        if (ownedSkins[skindex] == 1 || skindex == 0)
        {
            ShopScreen.SetActive(false);
        }
        else
        {
            ShopScreen.SetActive(true);
            shipCost.text = (50 * skindex * skindex).ToString();
            Debug.Log("Skindex: " + skindex);
        }
        
     }

     public void PurchaseSkin(int index)
     {
         int cost = 50 * index * index;
         if (coins < cost) return;
         coins -= cost; 
         ownedSkins[index] = 1;
         money.text = coins.ToString();
         ShopScreen.SetActive(false);
         SaveGame();

     }

     private void AddShield(int amount)
     {
         shields += amount;
     }

     private void AddCannonBall(int amount)
     {
         CurrentBalls += amount;
     }
}

