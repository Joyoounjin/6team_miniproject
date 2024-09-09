using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public Transform cards;
    public GameObject card;

    void Start()
    {
        int count = 0;
        int[] arr = new int[(GameManager.Instance.difficulty + 4) * 2];
        for (int i = 0; i < arr.Length; i += 2)
        {
            arr[i] = count;
            arr[i + 1] = count;
            count++;
        }
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        GameManager.Instance.cardCount = arr.Length;

        
        for (int i = 0; i < (GameManager.Instance.difficulty + 4) * 2; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            if (GameManager.Instance.difficulty == 0)
            {
                float x = (i % 4) * 1.4f - 2.1f;
                float y = (i / 4) * 1.4f - 3.0f;

                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().Setting(arr[i]);
            }

            else if (GameManager.Instance.difficulty == 1)
            {
                float x, y = 0;

                if (i < 8)
                {
                    x = (i % 4) * 1.4f - 2.1f;
                }
                else
                {
                    x = (i % 2) * 1.4f - 0.7f;
                }
                y = (i / 4) * 1.4f - 3.0f;

                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().Setting(arr[i]);
            }

            else if (GameManager.Instance.difficulty == 2)
            {
                float x = (i % 4) * 1.4f - 2.1f;
                float y = (i / 4) * 1.4f - 3.0f;

                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().Setting(arr[i]);
            }

        }
    }
}
