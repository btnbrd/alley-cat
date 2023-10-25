using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}