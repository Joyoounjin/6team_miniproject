using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Namebutton: MonoBehaviour
{
    public GameObject namegroup;
    public GameObject Levelgroup;

    public string name;

    public Image image;
    public Sprite imageB;
    public Sprite imageYJ;
    public Sprite imageGD;
    public Sprite imageTH;

    public Text NameTxt;

    // Start is called before the first frame update

    public void Select()
    {
        namegroup.SetActive(false);
        Levelgroup.SetActive(true);

        GameManager.Instance.Name = name;

        if (name == "B") image.sprite = imageB;
        else if (name == "YJ") image.sprite = imageYJ;
        else if (name == "GD") image.sprite = imageGD;
        else if (name == "TH") image.sprite = imageTH;
    }

}
