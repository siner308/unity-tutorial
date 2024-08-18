using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
public class EnemyController : MonoBehaviour
{
    Animator animator;
    public GameObject enemyBullet;
    private GameObject player;
    private bool isDead = false;
    private float deadTime = 0.0f;
    Rigidbody2D rg2D;
    private float moveSpeed;

    private float fireDelay;
    private static readonly int IsDead = Animator.StringToHash("isDead");

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rg2D = GetComponent<Rigidbody2D>();
        Move();
    }

    private void Move()
    {
        if (player == null)
        {
            Debug.Log("there is no player");
            return;
        }
        Vector3 distance = player.transform.position - transform.position;
        Vector3 direction = distance.normalized;
        moveSpeed = Random.Range(5.0f, 7.0f);
        rg2D.velocity = direction * moveSpeed;
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
        if (isDead)
        {
            deadTime += Time.deltaTime;
            if (!(deadTime > 1.0f)) return;
            Destroy(gameObject);
            return;
        }
        
        FireBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        
        // Destroy the Enemy
        animator.SetBool(IsDead, true);
        isDead = true;
    }
}
