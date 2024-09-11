using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public GameObject nameOnNameScene; //NameScene�� Name ������Ʈ
    public GameObject levelOnNameScene; //NameScene�� Level ������Ʈ
    
    public void Back()
    {
        //NameScene�� ���
        if (SceneManager.GetActiveScene().name == "NameScene")
        {
            //���̵� ���� �� Ŭ�� �� �̸� �������� ���ư���
            if (levelOnNameScene.activeInHierarchy == true)
            {
                nameOnNameScene.SetActive(true);
                levelOnNameScene.SetActive(false);
            }
            //�̸� ���� �� Ŭ�� �� StartScene���� ���ư���
            else
            {
                SceneManager.LoadScene("StartScene");
            }
        }

        //MainScene�� ��� Ŭ�� �� �̸� �������� ���ư���
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.LoadScene("NameScene");
        }

        //NameScene, MainScene�� �ƴ� �ٸ� Scene�� ��� StartScene����
        else
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
