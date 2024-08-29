using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemySpawnController : MonoBehaviour
{
    public Transform[] enemySpawns;

    public Object[] enemyGameObject;

    private float time;

    private float respawnTime;

    private int enemyCount;

    private int[] randomCount;

    private int wave;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCount = new int[enemyCount];
        wave = 0;;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        time += Time.deltaTime;
        if (time > respawnTime)
        {
            RandomPos();
            EnemyCreate();
            wave++;
            time -= time;
        }
    }
    
    void RandomPos()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            randomCount[i] = Random.Range(0, enemySpawns.Length);
        }
    }
    
    void EnemyCreate()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;
        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(enemyGameObject[Random.Range(0, enemyGameObject.Length)]) as GameObject;
            if (enemy == null) throw new ArgumentNullException(nameof(enemy));
            enemy.transform.position = enemySpawns[randomCount[i]].position;
            enemy.transform.position = new Vector3(
                Random.Range(enemySpawns[randomCount[i]].position.x - 1.0f, enemySpawns[randomCount[i]].position.x + 1.0f),
                Random.Range(enemySpawns[randomCount[i]].position.y - 1.0f, enemySpawns[randomCount[i]].position.y + 1.0f), 
                enemySpawns[randomCount[i]].position.z
                );
        }
    }
}