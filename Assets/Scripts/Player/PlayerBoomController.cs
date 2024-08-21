using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBoomController: MonoBehaviour
    {
        public float speed;
        private float time;
        private Vector3 direction = Vector3.up;
        private bool isTagged = false;
        
        private void Start()
        {
            speed = 30.0f;
            time = 0;
        }

        void Update()
        {
            FireBoom();
            DestroyBoom();
        }

        void FireBoom()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        
        void DestroyBoom()
        {
            time += Time.deltaTime;
            if (time > 3f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.CompareTag("Enemy") || collider2D.CompareTag("ItemDropEnemy"))
            {
                // make explosion effect
                GameObject explosion = Instantiate(Resources.Load("DeadEffect") as GameObject);
                explosion.transform.position = transform.position;
                explosion.transform.localScale = new Vector3(2, 2, 2);
                explosion.tag = "Explosion";
                
                isTagged = true;
                Destroy(gameObject);
            }
        }
    }
}