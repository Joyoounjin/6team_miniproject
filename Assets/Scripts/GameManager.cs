using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string Name = "";    //Select Index What User Choose, (B, GD, TH, YJ)
    public int difficulty = 0;  //Select Difficulty (0~2, 0: Easy, 1: Normal, 2: Hard)

    public Text timeTxt;
    float time = 0.0f;

    public Card firstCard;
    public Card secondCard;

    public int cardCount = 0;
    public GameObject endTxt;

    public AudioClip matchedclip;
    public AudioClip unmatchedclip;
    public AudioClip failclip;
    public AudioClip successclip;
    public AudioClip timeclip;


    public AudioSource audioSource;
    public AudioManager audioManager;


    public void isMatched()
    {
        if (firstCard.index == secondCard.index)
        {
            audioSource.PlayOneShot(matchedclip);

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if (cardCount == 0)
            {
                audioSource.PlayOneShot(successclip);
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            audioSource.PlayOneShot(unmatchedclip);
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void checkMatched()
    {
        if (firstCard.index == secondCard.index)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.audioSource.clip = AudioManager.Instance.mainclip;
        AudioManager.Instance.audioSource.Play();

    }

    //private void Update()
    //{
    //    time += Time.deltaTime;
    //    timeTxt.text = time.ToString("N2");
    //    if (time >= 30.0f)
    //    {
    //        endTxt.SetActive(true);
    //        Time.timeScale = 0.0f;
    //    }

    //}
}
