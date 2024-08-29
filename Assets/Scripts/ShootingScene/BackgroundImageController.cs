using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImageController : MonoBehaviour
{
    private float height;
    private float speed;
    private BoxCollider2D collider2D;
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        height = collider2D.size.y;
        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y <= -height)
        {
            Reposition();
        }
    }

    void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void Reposition()
    {
        Vector3 offset = new Vector3(0, height * 2, 0);
        transform.position = transform.position + offset;
    }
}
