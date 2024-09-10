using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    // Scroll View ���� ����
    public ScrollRect scrollRect;
    public Button leftArrow;
    public Button rightArrow;
    public GameObject panelPrefab;
    public RectTransform content;
    public int totalPanels = 5;

    public Sprite[] category1Images;
    public Sprite[] category2Images;
    public Sprite[] category3Images;
    public Sprite[] category4Images;
    public int categoryNumber = 1;
    private Sprite[] panelImages;

    private int currentIndex = 0;
    private float panelWidth = 760f;
    private float panelHeight = 430;

    public float autoScrollDelay = 1f;
    public float scrollSpeed = 0.2f;

    private bool isScrolling = false;
    private float timeSinceLastScroll = 0f;
    private float scrollLerpTime = 0f;   // ��ũ���� �ε巴�� �̵���Ű�� ���� ����
    private float targetPosition = 0f;   // ��ǥ ��ġ
    private float startPosition = 0f;    // ���� ��ġ

    void Start()
    {
        Time.timeScale = 1.0f;

        SetCategory(categoryNumber);

        leftArrow.onClick.AddListener(SlideLeft);
        rightArrow.onClick.AddListener(SlideRight);

        UpdateArrowButtons();
    }

    void Update()
    {
        // �ڵ� �����̵� ó��
        timeSinceLastScroll += Time.deltaTime;

        if (timeSinceLastScroll >= autoScrollDelay && !isScrolling)
        {
            SlideRight();
            timeSinceLastScroll = 0f;
        }

        // �ε巯�� ��ũ�� ó��
        if (isScrolling)
        {
            scrollLerpTime += Time.deltaTime / scrollSpeed;

            // Lerp�� ���� ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, scrollLerpTime);

            if (scrollLerpTime >= 1f)
            {
                scrollRect.horizontalNormalizedPosition = targetPosition;
                isScrolling = false;  // ��ũ�� �Ϸ�
                UpdateArrowButtons();
            }
        }
    }

    public void SetCategory(int categoryNumber)
    {
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

        totalPanels = panelImages.Length;
        SetPanelCount(totalPanels);
    }

    public void SetPanelCount(int count)
    {
        totalPanels = count;
        float contentWidth = count * panelWidth;
        content.sizeDelta = new Vector2(contentWidth, panelHeight);

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject newPanel = Instantiate(panelPrefab, content);
            RectTransform panelRectTransform = newPanel.GetComponent<RectTransform>();
            panelRectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
            panelRectTransform.localPosition = new Vector3(i * panelWidth, 0, 0);

            Image panelImage = newPanel.transform.Find("Image").GetComponent<Image>();

            if (i < panelImages.Length)
            {
                panelImage.sprite = panelImages[i];
                panelImage.GetComponent<RectTransform>().sizeDelta = new Vector2(panelImages[i].bounds.size.x, panelImages[i].bounds.size.y);
            }
        }

        scrollRect.horizontalNormalizedPosition = 0;
        UpdateArrowButtons();
    }

    void SlideLeft()
    {
        if (!isScrolling)
        {
            if (currentIndex == 0)
            {
                currentIndex = totalPanels - 1;
            }
            else
            {
                currentIndex--;
            }

            StartSmoothScroll(currentIndex);
        }
    }

    void SlideRight()
    {
        if (!isScrolling)
        {
            if (currentIndex == totalPanels - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            StartSmoothScroll(currentIndex);
        }
    }

    // �ε巯�� ��ũ���� ���� �Լ�
    void StartSmoothScroll(int index)
    {
        isScrolling = true;
        scrollLerpTime = 0f; // Lerp Ÿ�̸� �ʱ�ȭ
        startPosition = scrollRect.horizontalNormalizedPosition;
        targetPosition = (float)index / (totalPanels - 1); // ��ǥ ��ġ ����
    }

    void UpdateArrowButtons()
    {
        leftArrow.interactable = true;
        rightArrow.interactable = true;
    }
}
