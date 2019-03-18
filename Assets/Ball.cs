using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyMe());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator DestroyMe()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        Destroy(gameObject);
        if (GameControl.ballOnStage)
        {
            GameControl.ballOnStage = false;
        }
        if (GameControl.ballsLeft == 0)
        {
            GameControl.gameFinished = true;

        }
    }
}
