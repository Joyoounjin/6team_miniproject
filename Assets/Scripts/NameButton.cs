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

    public Text NameTxt;

    // Start is called before the first frame update

    public void Select()
    {
        namegroup.SetActive(false);
        Levelgroup.SetActive(true);

        GameManager.Instance.Name = name;
    }

}
