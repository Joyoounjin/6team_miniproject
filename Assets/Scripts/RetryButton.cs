using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void NextLevel()
    {
        if (GameManager.Instance.difficulty < 2) GameManager.Instance.difficulty++;
        SceneManager.LoadScene("MainScene");
    }
}