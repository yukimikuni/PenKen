using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destoryTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Suicide", destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Suicide()
    {
        Destroy(gameObject);
    }
}
