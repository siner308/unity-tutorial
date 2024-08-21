using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    // singleton
    public static UIController Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        UpdateBoomCount();
    }
    
    public void UpdateBoomCount()
    {
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Text boomCountText = GameObject.FindGameObjectWithTag("BoomCount").GetComponent<Text>();
        Debug.Log(boomCountText);
        boomCountText.text = playerController.boomCount.ToString();
        Debug.Log(boomCountText.text);
    }
}
