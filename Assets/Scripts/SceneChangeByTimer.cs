using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeByTimer : MonoBehaviour
{
    public string startScene;
    public float changeSeconds = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SceneChange");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // コルーチン  
    private IEnumerator SceneChange()
    {
        // コルーチンの処理  
        yield return new WaitForSeconds(changeSeconds);

        SceneManager.LoadScene(startScene);
    }
}
