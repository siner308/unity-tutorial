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
        private float deadTime = 0.0f;
        Animator animator;
        private static readonly int IsTagged = Animator.StringToHash("isTagged");
        Rigidbody2D rg2D;
        
        private void Start()
        {        
            rg2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            speed = 30.0f;
            time = 0;
        }

        void Update()
        {
            if (isTagged)
            {
                deadTime += Time.deltaTime;
                if (!(deadTime > 1.0f)) return;
                Destroy(gameObject);
                return;
            }
            
            Move();
            DestroyBoom();
        }

        void Move()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        
        void DestroyBoom()
        {
            time += Time.deltaTime;
            if (time > 5f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            Debug.Log("collider2D: " + collider2D);
            if (collider2D.CompareTag("Enemy") || collider2D.CompareTag("ItemDropEnemy"))
            {
                Debug.Log("EnemyType: " + collider2D);
                // make explosion effect
                animator.SetBool(IsTagged, true);
                rg2D.velocity = Vector2.zero;
                gameObject.transform.localScale = new Vector3(5, 5, 5);
                gameObject.tag = "Explosion";
                
                isTagged = true;
            }
        }
    }
}