using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public bool isStartScene;

    // Start is called before the first frame update
    void Start()
    {
        if (isStartScene)
        {
            score = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
