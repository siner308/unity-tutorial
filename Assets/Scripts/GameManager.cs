using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private PlayerController playerController;
    public Text restartButton;
    public Text gameOverText;

    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    void Update()
    {
        KeyCode keyCode = KeyCode.R;
        if (playerController.health <= 0 && Input.GetKeyDown(keyCode))
        {
            RestartGame();
        }
    }
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    
    public void RestartGame()
    {
        RemoveAllEnemies();
        CreatePlayer();
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }
    
    void RemoveAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy").Concat(GameObject.FindGameObjectsWithTag("ItemDropEnemy")).ToArray();
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (var item in items)
        {
            Destroy(item);
        }
        
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (var enemyBullet in enemyBullets)
        {
            Destroy(enemyBullet);
        }
    }

    void CreatePlayer()
    {
        GameObject player = Instantiate(playerPrefab);
        playerController = player.GetComponent<PlayerController>();
        playerController.Reset();
        UIController.Instance.ResetScore();
    }
}
