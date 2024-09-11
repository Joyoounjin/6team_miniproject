using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlideBar : MonoBehaviour
{
    public Image gauge;
    public Image gaugeImage;
    public float totalTime; // 10초로 설정
    private float currentTime;
    bool istime = false;
    bool isover= false;

    void Start()
    {
        AudioManager.Instance.bgmSource.pitch = 1.0f;
        AudioManager.Instance.bgmSource.clip = AudioManager.Instance.main;
        AudioManager.Instance.bgmSource.Play();

        currentTime = totalTime;
    }

    void Update()
    {
        currentTime = Mathf.Max(0, currentTime - Time.deltaTime);

        gauge.rectTransform.localScale = new Vector3(currentTime / totalTime, 1, 1);
        if(currentTime < totalTime/3 && !istime)
        {
            AudioManager.Instance.bgmSource.clip = AudioManager.Instance.time;
            AudioManager.Instance.bgmSource.Play();
            AudioManager.Instance.bgmSource.pitch = 1.5f;


            istime = true;
        }

        if (currentTime == 0 && !isover)
        {
            Time.timeScale = 0;
            Card[] cards = FindObjectsOfType<Card>();

            AudioManager.Instance.bgmSource.Stop();
            AudioManager.Instance.bgmSource.PlayOneShot(AudioManager.Instance.fail);

            foreach (Card card in cards)
            {
                card.IsGameOver = true;
            }

            //AudioManager.Instance.audioSource.Stop();
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            isover = true;
        }


    }

}
