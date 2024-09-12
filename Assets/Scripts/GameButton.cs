using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{

    public GameObject intro;

    // Start is called before the first frame update
    public  void Gameintro()
    {
        intro.SetActive(true);
    }

    public void Gameintroclose()
    {
        intro.SetActive(false);
    }
}
