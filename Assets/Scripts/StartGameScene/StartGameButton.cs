using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}
