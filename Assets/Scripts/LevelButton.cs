using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int level;

    public void Levelselect ()
    {
        GameManager.Instance.difficulty = level;
        SceneManager.LoadScene("MainScene");
    }

}
