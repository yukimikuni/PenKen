﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSelect : MonoBehaviour
{
    public string sceneName;
    public float waitTime = 0.0f;
    public GameObject BGM;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Select");
        }
    }

    public void SceneChange()
    {
        if (BGM != null)
        {
            BGM.GetComponent<AudioSource>().Stop();
        }
        Invoke("changeToNextScene", waitTime);

    }

    public void changeToNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
