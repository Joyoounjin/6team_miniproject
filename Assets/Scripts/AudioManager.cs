using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip start;
    public AudioClip main;
    public AudioClip gallery;
    public AudioClip matched;
    public AudioClip unmatched;
    public AudioClip time;
    public AudioClip flip;
    public AudioClip success;
    public AudioClip fail;
    public AudioClip click;

    public Scene currentScene;
    public AudioClip newClip = null;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    private void Start()
    {
        AudioManager.Instance.bgmSource.pitch = 1.0f;

    }
    public void SceneCheck()
    {
        currentScene = SceneManager.GetActiveScene();


        // 씬 이름에 따른 처리
        if (currentScene.name == "StartScene" || currentScene.name == "NameScene")
        {
            newClip = start;
        }
        else if (currentScene.name == "MainScene")
        {
            newClip = main;
            bgmSource.clip = main;
            bgmSource.Play();
        }
        else if (currentScene.name == "GalleryScene_YJ" || currentScene.name == "GalleryScene_GD" || currentScene.name == "GalleryScene_TH" || currentScene.name == "GalleryScene_B")
        {
            newClip = gallery;

        }

        if (bgmSource.clip != newClip)
        {
            bgmSource.Stop();
            bgmSource.clip = newClip;
            bgmSource.Play();
            AudioManager.Instance.bgmSource.pitch = 1.0f;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneCheck();
    }

}
