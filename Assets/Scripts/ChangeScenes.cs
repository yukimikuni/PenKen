using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public float timeToChange = 78.0f ;
    public float finishDispTime = 2.0f ;
    public string[] nextSceneNames;
    public GameObject finishObject;

    // Use this for initialization
    void Start()
    {
        //Invoke("ChangeScene", timeToChange);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// スコアを見て次に使うシーン名を返す
    /// </summary>
    /// <returns></returns>
    string getNextSceneName()
    {
        var score = ScoreManager.score;

        // 30以下なら0番目
        if (score <= 40)
            return nextSceneNames[0];

        if (score <= 65)
            return nextSceneNames[1];

        if (score <= 80)
            return nextSceneNames[2];

        return nextSceneNames[3];
    }

    void ChangeScene()
    {
        var sceneName = getNextSceneName();
         SceneManager.LoadScene(sceneName);
    }

    public void startChangeScene()
    {
        Invoke("ChangeScene", timeToChange);

        float finishTime = timeToChange - finishDispTime;

        Invoke("dispFinish", finishTime);

    }

    void dispFinish()
    {
        finishObject.SetActive(true);
    }
}