using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int index = 0;

    public SpriteRenderer frontImage;

    public GameObject front;
    public GameObject back;

    public Animator anim;

    public AudioClip clip;
    public AudioSource audioSource;

    public void Setting(int idx)
    {
        index = idx;
        frontImage.sprite = Resources.Load<Sprite>($"{GameManager.Instance.Name}/Card{index}");
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.isMatched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestoryCardInvoke", 0.5f);
    }

    void DestoryCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

}
