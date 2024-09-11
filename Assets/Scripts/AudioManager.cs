using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip start;
    public AudioClip main;
    public AudioClip matched;
    public AudioClip unmatched;
    public AudioClip time;
    public AudioClip flip;
    public AudioClip success;
    public AudioClip fail;

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
}
