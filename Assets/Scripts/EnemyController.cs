using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyController : MonoBehaviour
{
    Animator animator;
    public GameObject enemyBullet;
    private bool isDead = false;
    private float deadTime = 0.0f;

    private float fireDelay;
    
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FireBullet()
    {
        float firePeriod = 1.5f;
        fireDelay += Time.deltaTime;
        if (fireDelay > firePeriod)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return;
            }
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            fireDelay -= Random.Range(firePeriod - 0.5f, firePeriod + 0.5f);;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
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
