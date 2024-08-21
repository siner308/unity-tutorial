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
    PlayerController playerController;
    public GameObject enemyBullet;
    private GameObject player;
    private bool isDead = false;
    private float deadTime = 0.0f;
    Rigidbody2D rg2D;
    private float moveSpeed;

    private float fireDelay;
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private int hp;

    public GameObject[] items;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        rg2D = GetComponent<Rigidbody2D>();
        if (gameObject.CompareTag("ItemDropEnemy"))
        {
            hp = 3;
        }
        else
        {
            hp = 1;
        }
        Move();
    }

    private void Move()
    {
        if (player == null)
        {
            return;
        }
        Vector3 distance = player.transform.position - transform.position;
        Vector3 direction = distance.normalized;
        moveSpeed = Random.Range(15.0f, 17.0f);
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
            if (gameObject.CompareTag("ItemDropEnemy"))
            {
                Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
            }
            return;
        }
        
        FireBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hp -= playerController.damage;
            if (hp <= 0)
            {
                SetDead();
            }
        }
        
        if (other.CompareTag("Explosion"))
        {
            Debug.Log("Enemy is hit by " + other.tag);
            SetDead();
        }
        
        
        if (other.CompareTag("BlockCollider"))
        {
            
            Disappear();
        }
    }

    private void SetDead()
    {
        // Destroy the Enemy
        animator.SetBool(IsDead, true);
        isDead = true;
        rg2D.velocity = Vector2.zero;
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
