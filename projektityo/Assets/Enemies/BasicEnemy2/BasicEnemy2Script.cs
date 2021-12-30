using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy2Script : MainEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        bulletAngle = 160;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (bulletAngle == 160)
        {
            bulletAngle = 200;
        }
        else if (bulletAngle == 200)
        {
            bulletAngle = 160;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
            
    }
}
