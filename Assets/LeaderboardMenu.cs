using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardMenu : MonoBehaviour
{


    public Text HighestScoreText;
    //public Leaderboard.ScoreEntry scoreEntry;
    public const int maxLeaderCount = 5;

    // Start is called before the first frame update
    void Start()
    {

        //scoreEntry = Leaderboard.GetEntry(1);
        //HighestScoreText.text = scoreEntry.score.ToString();
        //HighestScoreText.text = "1." + Leaderboard.GetEntry(1).score.ToString() + "\n" + ;

        LeaderboardMenuUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
            //Application.Quit();
        }

      

    }
    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void LeaderboardClear()
    {
        Leaderboard.Clear();
        LeaderboardMenuUpdate();
    }
    void LeaderboardMenuUpdate()
    {
        HighestScoreText.text = "";
        for (int i = 0; i < maxLeaderCount; i++)
        {
            HighestScoreText.text += (i + 1).ToString() + ". " + Leaderboard.GetEntry(i).score.ToString() + "\n";
        }
    }


}
