using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 게임 실행될 때 초기화 (생성) 오브젝트가 없어도 생성

    public string Name = "";    //Select Index What User Choose, (B, GD, TH, YJ)
    public int difficulty = 0;  //Select Difficulty (0~2, 0: Easy, 1: Normal, 2: Hard)

    public Text timeTxt;
    float time = 0.0f;

    public Card firstCard;
    public Card secondCard;

    public int cardCount = 0;
    public GameObject endTxt;

    //public AudioClip clip;
    //public AudioSource audioSource;


    public void isMatched()
    {
        if (firstCard.index == secondCard.index)
        {
            //audioSource.PlayOneShot(clip);
            AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.flip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if (cardCount == 0)
            {
                AudioManager.Instance.bgmSource.Stop();
                AudioManager.Instance.bgmSource.PlayOneShot(AudioManager.Instance.success);


                if (GameManager.Instance.difficulty != 2)
                {
                    FindObjectOfType<Canvas>().transform.GetChild(5).gameObject.SetActive(true);
                    FindObjectOfType<Canvas>().transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (GameManager.Instance.difficulty == 2)
                {
                    FindObjectOfType<Canvas>().transform.GetChild(4).gameObject.SetActive(true);
                }

                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    //public void checkMatched()
    //{
    //    if (firstCard.index == secondCard.index)
    //    {
    //        firstCard.DestroyCard();
    //        secondCard.DestroyCard();
    //    }
    //    else
    //    {
    //        firstCard.CloseCard();
    //        secondCard.CloseCard();
    //    }

    //    firstCard = null;
    //    secondCard = null;
    //}

    private void Awake()
    {
        if (Instance == null)
        {
            Time.timeScale = 1.0f;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Time.timeScale = 1.0f;
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        //audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        //if (timeTxt != null)
        //{
        //    time += Time.deltaTime;
        //    timeTxt.text = time.ToString("N2");
        //    if (time >= 5.0f)
        //    {
        //        FindObjectOfType<Canvas>().transform.GetChild(1).gameObject.SetActive(true);
        //        Time.timeScale = 0.0f;
        //    }
        //}
        //else
        //{
        //    if (FindObjectOfType<Canvas>().transform.GetChild(0).name == "TimeTxt")
        //    {
        //        timeTxt = FindObjectOfType<Canvas>().transform.GetChild(0).GetComponent<Text>();
        //    }
        //}

    }
}
