using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));

        if (transform.position.y > 4 || transform.position.y < -6 )
        {
            Destroy(gameObject);
        }
    }
}
