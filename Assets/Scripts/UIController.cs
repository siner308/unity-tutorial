using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    // singleton
    public static UIController Instance;
    public Text scoreText;
    public Text boomCountText;
    public int score;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        score = 0;
    }
    
    public void AddScore(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void UpdateBoomCount()
    {
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        boomCountText.text = playerController.boomCount.ToString();
    }

    public void GetBoom()
    {
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        boomCountText.text = playerController.boomCount.ToString();
    }
}
