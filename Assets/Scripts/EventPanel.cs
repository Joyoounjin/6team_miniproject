using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    // Scroll View 관련 변수
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
    private float scrollLerpTime = 0f;   // 스크롤을 부드럽게 이동시키기 위한 변수
    private float targetPosition = 0f;   // 목표 위치
    private float startPosition = 0f;    // 시작 위치

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
        // 자동 슬라이드 처리
        timeSinceLastScroll += Time.deltaTime;

        if (timeSinceLastScroll >= autoScrollDelay && !isScrolling)
        {
            SlideRight();
            timeSinceLastScroll = 0f;
        }

        // 부드러운 스크롤 처리
        if (isScrolling)
        {
            scrollLerpTime += Time.deltaTime / scrollSpeed;

            // Lerp를 통해 시작 위치에서 목표 위치로 부드럽게 이동
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, scrollLerpTime);

            if (scrollLerpTime >= 1f)
            {
                scrollRect.horizontalNormalizedPosition = targetPosition;
                isScrolling = false;  // 스크롤 완료
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

    // 부드러운 스크롤을 위한 함수
    void StartSmoothScroll(int index)
    {
        isScrolling = true;
        scrollLerpTime = 0f; // Lerp 타이머 초기화
        startPosition = scrollRect.horizontalNormalizedPosition;
        targetPosition = (float)index / (totalPanels - 1); // 목표 위치 설정
    }

    void UpdateArrowButtons()
    {
        leftArrow.interactable = true;
        rightArrow.interactable = true;
    }
}
