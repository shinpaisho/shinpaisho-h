using UnityEngine;

namespace Enemies.BasicEnemy1
{
    public class BasicEnemy1Script : MainEnemy
    {
        
        // Start is called before the first frame update
        void Start()
        {
            shootInterval = .8f;    
            speed = .5f;
            scoreGive = 100;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            
        }
        
    }
}
