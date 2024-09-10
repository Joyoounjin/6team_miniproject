using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventPanel : MonoBehaviour
{
    // Scroll View ���� ����
    public ScrollRect scrollRect;        // ScrollRect ������Ʈ ����
    public Button leftArrow;             // ���� ȭ��ǥ ��ư
    public Button rightArrow;            // ������ ȭ��ǥ ��ư
    public GameObject panelPrefab;       // �г� ������
    public RectTransform content;        // Content�� RectTransform
    public int totalPanels = 5;          // �г� ���� (ī�װ��� ���� �޶��� �� ����)

    // ī�װ��� �̹��� �迭
    public Sprite[] category1Images;     // ī�װ� 1~4�� �ش��ϴ� �̹���
    public Sprite[] category2Images;
    public Sprite[] category3Images;
    public Sprite[] category4Images;
    public int categoryNumber = 1;
    private Sprite[] panelImages;        // ���� ���õ� ī�װ��� �̹��� �迭

    private int currentIndex = 0;        // ���� �г� �ε���
    private float panelWidth = 760f;     // �г� �ϳ��� �ʺ�
    private float panelHeight = 300f;    // �г� �ϳ��� ����
    private float targetPosition = 0f;   // ��ǥ ��ġ

    public float autoScrollDelay = 1f;   // �ڵ� ��ũ�� ���� �ð� (1��)
    public float scrollSpeed = 0.2f;     // ��ũ�� �ӵ�

    private bool isScrolling = false;    // ���� �����̵� ������ Ȯ��

    void Start()
    {
        // �⺻������ ī�װ� 1�� ������ ���·� ����
        SetCategory(categoryNumber);

        // ��ư Ŭ�� �̺�Ʈ ����
        leftArrow.onClick.AddListener(SlideLeft);
        rightArrow.onClick.AddListener(SlideRight);

        // ȭ��ǥ ��ư ���� ������Ʈ
        UpdateArrowButtons();

        // 1�ʸ��� �ڵ����� ������ �гη� �̵��ϴ� �ڷ�ƾ ����
        StartCoroutine(AutoSlide());
    }

    // ī�װ� ���ÿ� ���� �̹��� ����
    public void SetCategory(int categoryNumber)
    {
        // ī�װ� ���ÿ� ���� �̹����� panelImages�� �Ҵ�
        switch (categoryNumber)
        {
            case 1:
                panelImages = category1Images;
                break;
            case 2:
                panelImages = category2Images;
                break;
            case 3:
                panelImages = category3Images;
                break;
            case 4:
                panelImages = category4Images;
                break;
        }

        // ���õ� ī�װ��� �̹��� ������ ���� �г� ���� ����
        totalPanels = panelImages.Length;

        // �г��� �ٽ� ���� (�̹����� �°�)
        SetPanelCount(totalPanels);
    }

    // �г� ������ ���� Content ũ�� ���� �� �г� ����
    public void SetPanelCount(int count)
    {
        totalPanels = count;

        // Content�� �ʺ� �г� ������ �°� ����
        float contentWidth = count * panelWidth;
        content.sizeDelta = new Vector2(contentWidth, panelHeight);

        // ���� �г� ���� (�ʱ�ȭ)
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // �� �г� �߰�
        for (int i = 0; i < count; i++)
        {
            GameObject newPanel = Instantiate(panelPrefab, content);
            RectTransform panelRectTransform = newPanel.GetComponent<RectTransform>();

            // �г� ũ�� ����
            panelRectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);

            // �г��� Content �ȿ� ���η� ��ġ
            panelRectTransform.localPosition = new Vector3(i * panelWidth, 0, 0);

            // �гο� �̹��� �Ҵ�
            Image panelImage = newPanel.transform.Find("Image").GetComponent<Image>();

            if (i < panelImages.Length)
            {
                panelImage.sprite = panelImages[i];
                Debug.Log("�̹����ֱ�");
            }
        }

        // ScrollRect�� �ʱ� ��ġ ���� (ù ��° �гη� �̵�)
        scrollRect.horizontalNormalizedPosition = 0;

        // ȭ��ǥ ��ư ���� ������Ʈ
        UpdateArrowButtons();
    }

    // ���� ȭ��ǥ ��ư Ŭ�� ��
    void SlideLeft()
    {
        if (!isScrolling) // �����̵� ���� �ƴ� ���� ����
        {
            // ���� �ε����� 0�̸� ������ �гη� �̵�
            if (currentIndex == 0)
            {
                currentIndex = totalPanels - 1; // ������ �г� �ε�����
            }
            else
            {
                currentIndex--;
            }

            StartCoroutine(SmoothScrollToIndex(currentIndex));
        }
    }

    // ������ ȭ��ǥ ��ư Ŭ�� ��
    void SlideRight()
    {
        if (!isScrolling) // �����̵� ���� �ƴ� ���� ����
        {
            // ���� �ε����� ������ �г��̸� ù ��° �гη� �̵�
            if (currentIndex == totalPanels - 1)
            {
                currentIndex = 0; // ù ��° �г� �ε�����
            }
            else
            {
                currentIndex++;
            }

            StartCoroutine(SmoothScrollToIndex(currentIndex));
        }
    }

    // �г��� �ε巴�� �̵��ϴ� �ڷ�ƾ
    IEnumerator SmoothScrollToIndex(int index)
    {
        isScrolling = true;

        // ��ǥ ��ġ ���
        float targetPosition = (float)index / (totalPanels - 1);
        float startPosition = scrollRect.horizontalNormalizedPosition;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / scrollSpeed;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPosition;
        isScrolling = false;

        // ȭ��ǥ ��ư ���� ������Ʈ
        UpdateArrowButtons();
    }

    // �г� �̵� ó�� (��� �̵��� �����ϰ� Lerp�� ��ü��)
    void MoveToIndex(int index)
    {
        StartCoroutine(SmoothScrollToIndex(index));
    }

    // ȭ��ǥ ��ư Ȱ��ȭ/��Ȱ��ȭ ������Ʈ
    void UpdateArrowButtons()
    {
        leftArrow.interactable = true;
        rightArrow.interactable = true;
    }

    // 1�ʸ��� �ڵ����� �г��� �ѱ�� �ڷ�ƾ
    IEnumerator AutoSlide()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoScrollDelay); // 1�ʸ��� ����

            SlideRight(); // �ڵ����� ���������� �ѱ�
        }
    }
}
