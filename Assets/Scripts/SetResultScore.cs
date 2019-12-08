using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetResultScore : MonoBehaviour
{
    public GameObject resultScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ScoreManager.score.ToString());
        resultScoreText.GetComponent<Text>().text = "RESULT SCORE  " + ScoreManager.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
