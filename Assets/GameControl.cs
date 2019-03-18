using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{


    public enum GameStatus
    {
        StartingCountDown, Playing, GameEnd
    }
    public GameStatus gameStatus;
    public Image Popup;

    public Text TimeLimitText;
    public Text StartCountDown3sText;
    public static GameControl instace;
    public static float thePower; //current power
    public bool powerIncreasing = false;
    public float barSpeed = 100; //how fast bar will fill in.
    public int maxPowerValue = 100;
    public int minPowerValue = 0;
    public Slider PowerBarslider;
    public Text PowerBarText;
    public Image PowerBar;
    public GameObject ballReference;
    private Vector2 endPos;

    public static bool ballOnStage = false;
    public static int ballsLeft = 5;
    public static bool gameFinished = false;
    public Text BallsLeftText;
    public static int currentScore = 0;
    public int timerValueS = 30;

    public Button GoBackButton;
    public Button TryAgainButton;


    public Camera cameraObj;
    private Vector3 ballPos = new Vector3(0, 0.6f, -7.58f);
    public float throwSpeedZ;
    public float throwSpeedY;
    private GameObject currentBallObj;
    public GameObject alertReference;
    public Text GameOverText;




    // Use this for initialization
    void Start()
    {
   
        thePower = 0;
        PowerBarslider.value = minPowerValue;
        BallsLeftText.text = "Balls Left : 5";
        ballsLeft = 5;
        currentScore = 0;
        GameOverText.enabled = false;
        //GoBackButton.enabled = false;
        GoBackButton.gameObject.SetActive(false);
        TryAgainButton.gameObject.SetActive(false);
     
        Popup.enabled = true;



        gameStatus = GameStatus.StartingCountDown;
        Popup.enabled = false;

        gameFinished = false;
        ballOnStage = false;
        TimeLimitText.text = "Time Limit : " + timerValueS + " s";
        StartCoroutine(CountDown());


    }


    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && ballsLeft > 0 && !ballOnStage && gameStatus == GameStatus.Playing)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //startPos = touch.position;
                powerIncreasing = true;

            }
            if (touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position;
                powerIncreasing = false;


                if (!ballOnStage)
                {
                    Vector3 realPos = cameraObj.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y, 2.5f));
                    float forceZ = throwSpeedZ + (thePower/2);
                    float forceY = throwSpeedY + (thePower/2);

                    GameObject ball = Instantiate(ballReference, ballPos, Quaternion.identity) as GameObject;

                    Vector3 dir = (realPos - ballPos).normalized * 3 + new Vector3(0, forceY, forceZ);
                    ball.GetComponent<Rigidbody>().velocity = dir;

                    currentBallObj = ball;
                    ballOnStage = true;
                    endPos = new Vector2(0, 0);
        
                        ballsLeft--;
                        BallsLeftText.text = "Balls Left : " + ballsLeft.ToString();
            


                   
                }

            

            }


            if (powerIncreasing) //if bar is increasing, calculate thepower
            {

               if (thePower < 100)
               {
                    thePower += Time.deltaTime * barSpeed;
                        //thePower = Mathf.Clamp(thePower, 0, maxPowerValue);
               }
               else
               {
                    thePower = 0;
               }
            }
            else //else set thepower back to 0.
            {
                thePower = 0;
                //resetPowerBar();
            }
            PowerBarslider.value = thePower;
            PowerBarText.text = "Power " + Mathf.RoundToInt(thePower).ToString() + " %";
            //powerText.text = "abc";


        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
            //Application.Quit();
        }
        else if (gameFinished && gameStatus != GameStatus.GameEnd)
        {


            GameOver();
        }
    }

    private IEnumerator TimerCount()
    {
        yield return new WaitForSecondsRealtime(1.0f);

        timerValueS--;

        if (timerValueS <= 0)
        {
            timerValueS = 0;

            GameOver();

        }
        else
            StartCoroutine(TimerCount());

        TimeLimitText.text = "Time Limit : " + timerValueS + " s";


    }
    private IEnumerator CountDown()
    {
        StartCountDown3sText.enabled = true;
        Popup.enabled = true;

        StartCountDown3sText.text = "3";
        yield return new WaitForSecondsRealtime(1.0f);
        StartCountDown3sText.text = "2";
        yield return new WaitForSecondsRealtime(1.0f);
        StartCountDown3sText.text = "1";
        yield return new WaitForSecondsRealtime(1.0f);

        Popup.enabled = false;
        StartCountDown3sText.enabled = false;

        //Start the game
        gameStatus = GameStatus.Playing;
        StartCoroutine(TimerCount());


    }

    private void GameOver()
    {

        gameStatus = GameStatus.GameEnd;




        Popup.enabled = true;

        GameOverText.enabled = true;
        Popup.enabled = true;
      
            

        if (Leaderboard.HighestScore(currentScore))
        {
            GameOverText.text = "Game Over : Amazing! You got the highest score!";
            GoBackButton.gameObject.SetActive(true);
            TryAgainButton.gameObject.SetActive(false);
            GoBackButton.GetComponent<Button>().onClick.AddListener(() => Back());
        }
        else
        if (Leaderboard.LowestScore(currentScore))
        {
            GameOverText.text = "Game Over : Your score is low. Try again";
            TryAgainButton.gameObject.SetActive(true);
            GoBackButton.gameObject.SetActive(false);
            TryAgainButton.GetComponent<Button>().onClick.AddListener(() => Reload());
        }
        else
        {
            GameOverText.text = "Game Over : Try again.";
            TryAgainButton.gameObject.SetActive(true);
            GoBackButton.gameObject.SetActive(false);
            TryAgainButton.GetComponent<Button>().onClick.AddListener(() => Reload());
        }
        Leaderboard.Record(currentScore);




    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void Reload()
    {
        GameOverText.enabled = false;
        GoBackButton.gameObject.SetActive(false);
        TryAgainButton.gameObject.SetActive(false);
        Popup.enabled = false;
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    


}
