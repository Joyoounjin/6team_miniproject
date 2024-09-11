using Unity.VisualScripting;
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
    public int totalPanels = 6;

    public Sprite[] category1Images;
    public Sprite lockedImage;
    public GameObject[] blindPanel;



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

        SetPanelCount(totalPanels);

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
        float contentWidth = count * panelWidth;
        content.sizeDelta = new Vector2(contentWidth, panelHeight);


        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        int numPanelsToShow = 4 + GameManager.Instance.difficulty; 

        for (int i = 0; i < category1Images.Length; i++)
        {
            GameObject newPanel = Instantiate(panelPrefab, content);

            RectTransform panelRectTransform = newPanel.GetComponent<RectTransform>();
            panelRectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
            panelRectTransform.localPosition = new Vector3(i * panelWidth, 0, 0);

            Transform imageTransform = newPanel.transform.Find("Image");
            Image img = imageTransform.GetComponent<Image>();

            if (i < numPanelsToShow)
            {
                //img.gameObject.SetActive(true); // �г� Ȱ��ȭ
                img.sprite = category1Images[i];
            }
            else
            {
                //img.gameObject.SetActive(true); // ��� �гε� Ȱ��ȭ �ʿ� (���� ����� ����)
                img.sprite = category1Images[i];
                img.color = new Color(1, 1, 1, 0.1f); //�����ϰ�


                //lock�̹��� �߰�
                GameObject blindObj = new GameObject("blind");
                blindObj.transform.SetParent(newPanel.transform, false);
                Image blindImg= blindObj.AddComponent<Image>();
                blindImg.rectTransform.sizeDelta = panelRectTransform.sizeDelta;
                blindImg.color = new Color(0, 0, 0, 0.97f);

                GameObject lockIconObj = new GameObject("LockIcon");
                lockIconObj.transform.SetParent(newPanel.transform, false); // �г��� �ڽ����� �߰�

                Image lockImg = lockIconObj.AddComponent<Image>(); // Image ������Ʈ �߰�
                lockImg.sprite = lockedImage; // �ڹ��� �̹��� ����

                // ũ�⸦ �гΰ� ���߱�
                //RectTransform rectTransform = lockIconObj.GetComponent<RectTransform>();
                //rectTransform.sizeDelta = newPanel.GetComponent<RectTransform>().sizeDelta/4;
                //rectTransform.localPosition = Vector3.zero; // ��ġ�� �߾����� ����

                //�ؽ�Ʈ ����ε�
                blindPanel[i-4].SetActive(true);
            }

        }

        scrollRect.horizontalNormalizedPosition = 0;
        UpdateArrowButtons();
    }

    void AddLockIcon(GameObject panel)
    {
        GameObject lockIconObj = new GameObject("LockIcon");
        lockIconObj.transform.SetParent(panel.transform, false); // �г��� �ڽ����� �߰�

        Image lockImage = lockIconObj.AddComponent<Image>(); // Image ������Ʈ �߰�
        lockImage.sprite = lockedImage; // �ڹ��� �̹��� ����

        // ũ�⸦ �гΰ� ���߱�
        RectTransform rectTransform = lockIconObj.GetComponent<RectTransform>();
        rectTransform.sizeDelta = panel.GetComponent<RectTransform>().sizeDelta;
        rectTransform.localPosition = Vector3.zero; // ��ġ�� �߾����� ����
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
