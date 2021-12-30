using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManagement : MonoBehaviour
{
    public GameObject basicEnemy;
    public GameObject basicEnemy2;

    private int maxAmm = 1;
    
    static int score = 0;
    
    static public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        // spawns enemies depending on the time
        if (InitialScript.RealTime == 5 && maxAmm == 1)
        {
            Instantiate(basicEnemy,new Vector2(0, 5) , basicEnemy.transform.rotation);
            Instantiate(basicEnemy,new Vector2(2, 7) , basicEnemy.transform.rotation);
            Instantiate(basicEnemy,new Vector2(-1, 5) , basicEnemy.transform.rotation);
            Instantiate(basicEnemy,new Vector2(0.5f, 9) , basicEnemy.transform.rotation);

            maxAmm++;
        }

        if (InitialScript.RealTime == 20 && maxAmm == 2)
        {
            Instantiate(basicEnemy,new Vector2(-3, 5) , basicEnemy.transform.rotation);
            Instantiate(basicEnemy,new Vector2(-1, 6) , basicEnemy.transform.rotation);
            
            maxAmm++;
        }

        if (InitialScript.RealTime == 30 && maxAmm == 3)
        {
            Instantiate(basicEnemy2,new Vector2(2, 5.3f) , basicEnemy2.transform.rotation);
            Instantiate(basicEnemy2,new Vector2(-4, 5) , basicEnemy2.transform.rotation);
            Instantiate(basicEnemy2,new Vector2(0.5f, 5.8f) , basicEnemy2.transform.rotation);

            maxAmm++;
        }
    }

    // Gets called when an enemy dies, adds their scoreGive to the score
    public static void UpdateScore(int give)
    {
        score += give;

        scoreText.text = "Score: " + score;
    }
}
