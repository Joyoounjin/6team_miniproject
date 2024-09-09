using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventPanel : MonoBehaviour
{
    // Scroll View 관련 변수
    public ScrollRect scrollRect;        // ScrollRect 컴포넌트 참조
    public Button leftArrow;             // 왼쪽 화살표 버튼
    public Button rightArrow;            // 오른쪽 화살표 버튼
    public GameObject panelPrefab;       // 패널 프리팹
    public RectTransform content;        // Content의 RectTransform
    public int totalPanels = 5;          // 패널 개수 (카테고리에 따라 달라질 수 있음)

    // 카테고리별 이미지 배열
    public Sprite[] category1Images;     // 카테고리 1~4에 해당하는 이미지
    public Sprite[] category2Images;
    public Sprite[] category3Images;
    public Sprite[] category4Images;
    public int categoryNumber = 1;
    private Sprite[] panelImages;        // 현재 선택된 카테고리의 이미지 배열

    private int currentIndex = 0;        // 현재 패널 인덱스
    private float panelWidth = 760f;     // 패널 하나의 너비
    private float panelHeight = 300f;    // 패널 하나의 높이
    private float targetPosition = 0f;   // 목표 위치

    public float autoScrollDelay = 1f;   // 자동 스크롤 지연 시간 (1초)
    public float scrollSpeed = 0.2f;     // 스크롤 속도

    private bool isScrolling = false;    // 현재 슬라이드 중인지 확인

    void Start()
    {
        // 기본적으로 카테고리 1을 선택한 상태로 시작
        SetCategory(categoryNumber);

        // 버튼 클릭 이벤트 연결
        leftArrow.onClick.AddListener(SlideLeft);
        rightArrow.onClick.AddListener(SlideRight);

        // 화살표 버튼 상태 업데이트
        UpdateArrowButtons();

        // 1초마다 자동으로 오른쪽 패널로 이동하는 코루틴 시작
        StartCoroutine(AutoSlide());
    }

    // 카테고리 선택에 따른 이미지 설정
    public void SetCategory(int categoryNumber)
    {
        // 카테고리 선택에 따라 이미지를 panelImages에 할당
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

        // 선택된 카테고리의 이미지 개수에 따라 패널 개수 조정
        totalPanels = panelImages.Length;

        // 패널을 다시 세팅 (이미지에 맞게)
        SetPanelCount(totalPanels);
    }

    // 패널 개수에 따라 Content 크기 조정 및 패널 생성
    public void SetPanelCount(int count)
    {
        totalPanels = count;

        // Content의 너비를 패널 개수에 맞게 설정
        float contentWidth = count * panelWidth;
        content.sizeDelta = new Vector2(contentWidth, panelHeight);

        // 기존 패널 삭제 (초기화)
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 새 패널 추가
        for (int i = 0; i < count; i++)
        {
            GameObject newPanel = Instantiate(panelPrefab, content);
            RectTransform panelRectTransform = newPanel.GetComponent<RectTransform>();

            // 패널 크기 설정
            panelRectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);

            // 패널을 Content 안에 가로로 배치
            panelRectTransform.localPosition = new Vector3(i * panelWidth, 0, 0);

            // 패널에 이미지 할당
            Image panelImage = newPanel.transform.Find("Image").GetComponent<Image>();

            if (i < panelImages.Length)
            {
                panelImage.sprite = panelImages[i];
                Debug.Log("이미지넣기");
            }
        }

        // ScrollRect의 초기 위치 설정 (첫 번째 패널로 이동)
        scrollRect.horizontalNormalizedPosition = 0;

        // 화살표 버튼 상태 업데이트
        UpdateArrowButtons();
    }

    // 왼쪽 화살표 버튼 클릭 시
    void SlideLeft()
    {
        if (!isScrolling) // 슬라이딩 중이 아닐 때만 실행
        {
            // 현재 인덱스가 0이면 마지막 패널로 이동
            if (currentIndex == 0)
            {
                currentIndex = totalPanels - 1; // 마지막 패널 인덱스로
            }
            else
            {
                currentIndex--;
            }

            StartCoroutine(SmoothScrollToIndex(currentIndex));
        }
    }

    // 오른쪽 화살표 버튼 클릭 시
    void SlideRight()
    {
        if (!isScrolling) // 슬라이딩 중이 아닐 때만 실행
        {
            // 현재 인덱스가 마지막 패널이면 첫 번째 패널로 이동
            if (currentIndex == totalPanels - 1)
            {
                currentIndex = 0; // 첫 번째 패널 인덱스로
            }
            else
            {
                currentIndex++;
            }

            StartCoroutine(SmoothScrollToIndex(currentIndex));
        }
    }

    // 패널을 부드럽게 이동하는 코루틴
    IEnumerator SmoothScrollToIndex(int index)
    {
        isScrolling = true;

        // 목표 위치 계산
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

        // 화살표 버튼 상태 업데이트
        UpdateArrowButtons();
    }

    // 패널 이동 처리 (즉시 이동은 제거하고 Lerp로 대체됨)
    void MoveToIndex(int index)
    {
        StartCoroutine(SmoothScrollToIndex(index));
    }

    // 화살표 버튼 활성화/비활성화 업데이트
    void UpdateArrowButtons()
    {
        leftArrow.interactable = true;
        rightArrow.interactable = true;
    }

    // 1초마다 자동으로 패널을 넘기는 코루틴
    IEnumerator AutoSlide()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoScrollDelay); // 1초마다 실행

            SlideRight(); // 자동으로 오른쪽으로 넘김
        }
    }
}
