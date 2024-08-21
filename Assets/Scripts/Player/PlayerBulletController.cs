using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletController: MonoBehaviour
    {
        public float speed;
        private float time;
        private Vector3 direction = Vector3.up;
        
        private void Start()
        {
            speed = 30.0f;
            time = 0;
            
            if (direction == Vector3.up)
            {
                // create two more bullets
                GameObject bullet1 = Instantiate(gameObject, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
                GameObject bullet2 = Instantiate(gameObject, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
                
                // set the direction of the new bullets
                bullet1.GetComponent<BulletController>().direction = new Vector3(0.5f, 1, 0);
                bullet2.GetComponent<BulletController>().direction = new Vector3(-0.5f, 1, 0);
            }
        }

        void Update()
        {
            FireBullet();
            DestroyBullet();
        }

        void FireBullet()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        
        void DestroyBullet()
        {
            time += Time.deltaTime;
            if (time > 1.5f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.CompareTag("Enemy") || collider2D.CompareTag("ItemDropEnemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}