using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //MainCanvas.SetActive(false);
        //LeaderboardCanvas.SetActive(false);

        SceneManager.LoadScene("Game");
    }
    public void OpenLeaderboard()
    {
        //MainCanvas.SetActive(false);
        //LeaderboardCanvas.SetActive(false);

        SceneManager.LoadScene("Leaderboard");
    }

}
