using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.difficulty++;
        if (GameManager.Instance.difficulty >= 2)
        {
            GameManager.Instance.difficulty = 2;
        }
        SceneManager.LoadScene("MainScene");
    }
}