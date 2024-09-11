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

    public int categoryNumber = 1;
    private Sprite[] panelImages;

    private int currentIndex = 0;
    private float panelWidth = 760f;
    private float panelHeight = 430;

    public float autoScrollDelay = 3f;
    public float scrollSpeed = 0.2f;

    private bool isScrolling = false;
    private float timeSinceLastScroll = 0f;
    private float scrollLerpTime = 0f;   // ��ũ���� �ε巴�� �̵���Ű�� ���� ����
    private float targetPosition = 0f;   // ��ǥ ��ġ
    private float startPosition = 0f;    // ���� ��ġ

    void Start()
    {
        Time.timeScale = 1.0f;

        leftArrow.onClick.AddListener(SlideLeft);
        rightArrow.onClick.AddListener(SlideRight);

        if (GameManager.Instance.difficulty == 0)
        {
            totalPanels = 3;
            SetPanelCount(totalPanels);
            Debug.Log("easy");
        }

        else if (GameManager.Instance.difficulty == 1)
        {
            totalPanels = 4;
            SetPanelCount(totalPanels);

            Debug.Log("nomarl");
        }
        else if (GameManager.Instance.difficulty == 2)
        {
            totalPanels = 5;
            SetPanelCount(totalPanels);

            Debug.Log("hard");
        }

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

            Transform imageTransform = newPanel.transform.Find("Image");
            if (imageTransform != null)
            {
                Image img = imageTransform.GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = category1Images[i];
                    img.GetComponent<RectTransform>().sizeDelta = new Vector2(category1Images[i].bounds.size.x, category1Images[i].bounds.size.y);
                }
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
