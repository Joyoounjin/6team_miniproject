using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public GameObject nameOnNameScene; //NameScene의 Name 오브젝트
    public GameObject levelOnNameScene; //NameScene의 Level 오브젝트
    
    public void Back()
    {
        //NameScene일 경우
        if (SceneManager.GetActiveScene().name == "NameScene")
        {
            //난이도 설정 중 클릭 시 이름 선택으로 돌아가기
            if (levelOnNameScene.activeInHierarchy == true)
            {
                nameOnNameScene.SetActive(true);
                levelOnNameScene.SetActive(false);
            }
            //이름 설정 중 클릭 시 StartScene으로 돌아가기
            else
            {
                SceneManager.LoadScene("StartScene");
            }
        }

        //MainScene일 경우 클릭 시 이름 선택으로 돌아가기
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.LoadScene("NameScene");
        }

        //NameScene, MainScene이 아닌 다른 Scene일 경우 StartScene으로
        else
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
