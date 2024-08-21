using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    protected GameObject player;
    protected float speed;
    protected PlayerController playerController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);   
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            ItemGain();
        }
        
        if (other.CompareTag("BlockCollider"))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ItemGain()
    {
        throw new NotImplementedException();
    }
}
