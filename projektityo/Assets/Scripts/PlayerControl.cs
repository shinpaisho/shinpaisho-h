using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    private float speed;
    
    private float xBoundry = 5.2f;

    private float yBoundry = 3.8f;

    private float frameTime;
    private float flashTime;
    private float zRotation;

    private int InvincibilityTimer;

    public float playerLevel = 1;

    private int lives = 3;

    private bool invincibility;

    public GameObject projectile;
    public GameObject levelProjectile;
    public GameObject hitbox;
    public TextMeshProUGUI livesText;
    public GameObject gameOverText;
    public SpriteRenderer PlayerSprite;

    private void Start()
    {
        PlayerSprite.material.color = Color.white;
    }

    void FixedUpdate()
    {
        frameTime += Time.deltaTime;
        flashTime += Time.deltaTime;
     
        // Get the input, move the charecter if it is in boundries
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (transform.position.x > xBoundry)
        {
            transform.position = new Vector2(xBoundry, transform.position.y);
        } 
        if (transform.position.x < -xBoundry)
        {
            transform.position = new Vector2(-xBoundry, transform.position.y);
        }

        if (transform.position.y > yBoundry)
        {
            transform.position = new Vector2(transform.position.x, yBoundry);
        }
        if (transform.position.y < -yBoundry)
        {
            transform.position = new Vector2(transform.position.x, -yBoundry);
        }

        // Slow with Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 4;
            zRotation = 7;
            hitbox.SetActive(true);
        }
        else
        {
            speed = 10;
            zRotation = 20;
            hitbox.SetActive(false);
        }

        // Shoot with Z
        if (Input.GetKey(KeyCode.Z))
        {
            if (frameTime > .07)
            {
                ShootStuff();
                frameTime = 0;
            }
        }
        
        // possibly make the movement a part of the border check?
        transform.Translate(Vector2.right * (horizontalInput * speed * Time.deltaTime));
        transform.Translate(Vector2.up * (verticalInput * speed * Time.deltaTime));

        // flash sprite when invincible, does not work for some reason????
        if (flashTime > .1)
        {
            if (invincibility)
            {
                if (PlayerSprite.material.color == Color.white) // if sprite is normal
                {
                    PlayerSprite.material.color = Color.cyan;
                    Debug.Log("blue");
                }
                else if (PlayerSprite.material.color == Color.cyan) // if sprite is flashing
                {
                    PlayerSprite.material.color = Color.white;
                    Debug.Log("white");
                }
            }

            flashTime = 0;
        }
        

        if (InvincibilityTimer < InitialScript.RealTime)
        {
            invincibility = false;
            PlayerSprite.material.color = Color.white;
        }

        if (lives == 0)
        {
            Destroy(gameObject);
            gameOverText.SetActive(true);
        }
    }

    private void ShootStuff()
    {
        Instantiate(projectile, new Vector2(transform.position.x + 0.3f, transform.position.y) , projectile.transform.rotation);
        Instantiate(projectile, new Vector2(transform.position.x - 0.3f, transform.position.y) , projectile.transform.rotation);
        
        Instantiate(levelProjectile, new Vector2(transform.position.x + 0.3f, transform.position.y) , Quaternion.Euler(0,0, -zRotation));
        Instantiate(levelProjectile, new Vector2(transform.position.x - 0.3f, transform.position.y) , Quaternion.Euler(0,0, zRotation));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") && !invincibility || other.gameObject.CompareTag("Enemy") && !invincibility)
        {
            Debug.Log("got hit");
            OnDeath();
        }
    }


    private void OnDeath()
    {
        // on death events
        
        invincibility = true;
        lives -= 1;
        livesText.text = "Lives: " + lives;
        transform.position = new Vector2(0, -3);
        InvincibilityTimer = InitialScript.RealTime + 3; // + is how much time the invincibility will be
    }
}
