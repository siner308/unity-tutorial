using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    private float time;
    private Rigidbody2D rg2D;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10.0f;
        rotateSpeed = 300.0f;
        time = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        rg2D = GetComponent<Rigidbody2D>();
        
        FireBullet();
    }

    // Update is called once per frame
    void Update()
    {
        RotateBullet();
        DestroyBullet();
    }

    private void FireBullet()
    {
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;
        rg2D.velocity = dir * moveSpeed;
    }

    private void RotateBullet()
    {
        transform.rotation = Quaternion.Euler(0, 0, time * rotateSpeed);
    }

    private void DestroyBullet()
    {
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Boom") || other.CompareTag("Explosion"))
        {
            Debug.Log("Bullet is hit by " + other.tag);
            Destroy(gameObject);
        }
    }
}
