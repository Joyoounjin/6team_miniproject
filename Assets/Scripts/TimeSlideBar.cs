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
        currentTime = totalTime;
        //gaugeImage.rectTransform.sizeDelta = new Vector2(55, 64);
        //gaugeImage.rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // 앵커를 부모의 가운데로 설정
        //gaugeImage.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        //gaugeImage.rectTransform.pivot = new Vector2(0.5f, 0.5f); // 피벗도 중앙으로
    }

    void Update()
    {
        currentTime = Mathf.Max(0, currentTime - Time.deltaTime);

        gauge.rectTransform.localScale = new Vector3(currentTime / totalTime, 1, 1);
        if(currentTime < totalTime/3 && !istime)
        {
            //AudioManager.Instance.audioSource.Stop();
            //AudioManager.Instance.audioSource.Play();
            istime = true;
        }

        if (currentTime == 0 && !isover)
        {
            Time.timeScale = 0;
            //AudioManager.Instance.audioSource.Stop();
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            isover = true;
        }


    }

}
