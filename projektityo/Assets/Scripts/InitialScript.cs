using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Initial script used for timing
public class InitialScript : MonoBehaviour
{
    private float gameTime;
    public static int RealTime; // global int timer, not completely correct but who cares
    
    public void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > 1)
        {
            gameTime -= 1;
            RealTime += 1;
        }
        
    }
}
