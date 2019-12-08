using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public bool clearHighScore;
    public GameObject highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (clearHighScore)
        {
            PlayerPrefs.SetInt("HIGH SCORE", 0);
        }
        int hightScore = PlayerPrefs.GetInt("HIGH SCORE");
        if ( ScoreManager.score > hightScore)
        {
            PlayerPrefs.SetInt("HIGH SCORE", ScoreManager.score);
        }
        highScoreText.GetComponent<Text>().text = "HIGH SCORE  " + hightScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
