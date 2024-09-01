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

    private bool bossCreate;
    public GameObject bossGameObject;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCount = new int[enemyCount];
        wave = 0;;
        bossCreate = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

        if (wave >= 5 && bossCreate == false)
        {
            createBoss();
        }
    }

    private void createBoss()
    {
        bossCreate = true;
        GameObject tmp = GameObject.Instantiate(bossGameObject);
        int randomCount = Random.Range(0, 9);
        tmp.transform.position = enemySpawns[randomCount].position;
        BossController bossController = tmp.GetComponent<BossController>();
        UIController.instance.isBossSpawn = true;
        UIController.instance.MaxHpFirst = bossController.hpFirst;
        UIController.instance.MaxHpSecond = bossController.hpSecond;
        UIController.instance.bossController = bossController;
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