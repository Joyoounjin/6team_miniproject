using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    public void GameStart()
        {
            SceneManager.LoadScene("NameScene");
            //AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.click);
        }
    }
