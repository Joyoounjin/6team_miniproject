using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlideBar : MonoBehaviour
{
    public Image gauge;
    public float totalTime; // 10초로 설정
    private float currentTime;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        currentTime = Mathf.Max(0, currentTime - Time.deltaTime);

        gauge.rectTransform.localScale = new Vector3(currentTime / totalTime, 1, 1);

        if (currentTime == 0)
        {
            Time.timeScale = 0;
            Debug.Log($"{currentTime}");
        }
    }



}
