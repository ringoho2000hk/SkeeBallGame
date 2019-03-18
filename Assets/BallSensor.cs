using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class BallSensor : MonoBehaviour
{
    public int sensorValue = 0;

    public GameObject alertReference;
    public Text ScoreText;
    public Image Popup;

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
       
        GameControl.thePower = 0;

        GameControl.ballOnStage = false;
        GameControl.currentScore += sensorValue;
        ScoreText.text = "Score : " + GameControl.currentScore.ToString();



        if (GameControl.ballsLeft == 0)
        {

            GameControl.gameFinished = true;
            //Reload();
        }

    }

    //void Reload()
    //{
    //    //Application.LoadLevel(Application.loadedLevel);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}



}
