using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Script for the main enemy behaviour
public class MainEnemy : MonoBehaviour
{
    public GameObject bullet;

    public int health = 100;

    private int yRange = -6;

    public float speed = 0.5f;

    public int scoreGive;
    
    private float gameTime;

    private Collider2D enemyCollision;

    public int bulletAngle = 180;

    public float shootInterval = 0.3f;
    

    // Update is called once per frame
    protected virtual void Update()
    {
        gameTime += Time.deltaTime;
        transform.Translate(Vector2.down * (speed * Time.deltaTime));

        if (health <= 0)
        {
            Destroy(gameObject);
            EnemyManagement.UpdateScore(scoreGive);
        }

        if (gameTime > shootInterval)
        {
            BulletScript();
            gameTime -= shootInterval;
        }

        if(transform.position.y < yRange)
        {
            Destroy(gameObject);
        }
    }

    private void BulletScript()
    {
        Instantiate(bullet, new Vector2(transform.position.x , transform.position.y) , Quaternion.Euler(0,0, bulletAngle));
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("PlayerBullet")) return;
        health--;
        Destroy(other.gameObject);
    }
}
