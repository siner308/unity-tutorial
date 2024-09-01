using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    // singleton
    public static UIController instance;
    
    // playing info
    public int score;
    public Text scoreText;
    public Text boomCountText;
    
    // stat info in gameover
    public int highScore;
    public Text highScoreText;

    public Image blackOut_Curtain;
    private float blackOut_Curtain_Value;
    private float blackOut_Curtain_speed;

    public Image gameOverImage;
    
    // boss
    public Image hpBarFrame;
    public Image hpBarFirst;
    public Image hpBarSecond;
    public float MaxHpFirst;
    public float MaxHpSecond;
    public BossController bossController;
    public bool isBossSpawn;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        blackOut_Curtain_Value = 1.0f;
        blackOut_Curtain_speed = 0.5f;
        
        isBossSpawn = false;
    }

    void Update()
    {
        if (blackOut_Curtain_Value > 0)
        {
            HideBlackOutCurtain();
        }

        if (isBossSpawn)
        {
            BossHpBarCheck();
        }
    }

    public void BossHpBarCheck()
    {
        hpBarFrame.gameObject.SetActive(isBossSpawn);
        hpBarFirst.gameObject.SetActive(isBossSpawn);
        hpBarSecond.gameObject.SetActive(isBossSpawn);
        
        hpBarFirst.fillAmount = bossController.hpFirst / MaxHpFirst;
        hpBarSecond.fillAmount = bossController.hpSecond / MaxHpSecond;
    }
    
    public void BossHpBarHide()
    {
        hpBarFrame.gameObject.SetActive(false);
        hpBarFirst.gameObject.SetActive(false);
        hpBarSecond.gameObject.SetActive(false);
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

    public void HideBlackOutCurtain()
    {
        blackOut_Curtain_Value -= blackOut_Curtain_speed * Time.deltaTime;
        Color color = new Color(0, 0, 0, blackOut_Curtain_Value);
        blackOut_Curtain.color = color;
    }

    public void GameOver()
    {
        gameOverImage.gameObject.SetActive(true);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        highScoreText.text = highScore.ToString();
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("Scenes/Title");
        
        Destroy(gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(SoundManager.instance.gameObject);
    }
}
