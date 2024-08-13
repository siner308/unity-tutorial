using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;

    private bool isDead = false;
    private float deadTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            deadTime += Time.deltaTime;
            if (deadTime > 1.0f)
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by bullet");
            animator.SetBool("isDead", true);
            isDead = true;
        }
    }
}
