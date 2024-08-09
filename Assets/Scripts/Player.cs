using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static int size = 30;
    private readonly Vector3 limitMax = new Vector3(size, size, 0);
    private readonly Vector3 limitMin = new Vector3(-size, -size, 0);

    public GameObject prefabBullet;
    private float time;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Player";
        this.transform.position = new Vector3(0, 0, 0);
        time = 0;
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FireBullet();
    }

    public void  Move()
    {
        var x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        
        transform.Translate(new Vector3(x, y, 0));

        if (transform.position.x > limitMax.x)
        {
            transform.position = new Vector3(limitMax.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -limitMax.x)
        {
            transform.position = new Vector3(-limitMax.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y > limitMax.y)
        {
            transform.position = new Vector3(transform.position.x, limitMax.y, transform.position.z);
        }
        if (transform.position.y < -limitMax.y)
        {
            transform.position = new Vector3(transform.position.x, -limitMax.y, transform.position.z);
        }
    }

    public void FireBullet()
    {
        float interval = 0.5f;
        time += Time.deltaTime; // 시간을 계속 더하다가
        Debug.Log("Fire " + time);
        if (time > interval) // 일정 시간이 지나면 미사일을 쏘고
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity);
            /**
             * 시간을 초기화 한다.
             * 0으로 만들지 않는 이유는, 시간이 0.1씩 천천히 올라가는 것이 아니기 때문에, 오차가 발생할 수 있다.
             * Time.deltaTime은 0.01 ~ 0.02 사이의 값을 가지기 때문에, 0.3이 되지 않을 수도 있다.
             * 0으로 만들어버리면, 총알이 덜 나갈 수 있다.
             */
            time -= interval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));
    }
}

