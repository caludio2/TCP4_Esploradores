using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MecanicaDeFolearCaderno : MonoBehaviour
{
    [Header("Display")]
    #region Display
    [SerializeField] GameObject[] starsDisplay;
    [SerializeField] TextMeshProUGUI minigameName;
    [SerializeField] RawImage minigameThumbnail;
    [SerializeField] TextMeshProUGUI PointsDisplay;
    [SerializeField] TextMeshProUGUI Description;
    #endregion

    [Header("")]
    [Header("page")]
    #region page
    [SerializeField] Texture2D[] thumb;
    int currentPageNumber;
    #endregion

    [Header("")]
    [Header("book")]
    # region book
    [SerializeField] GameObject bookDisplay;
    #endregion

    #region fadeIn
    [SerializeField] CanvasGroup canvas;
    public float duration = 1f;
    #endregion

    private Vector2 startPos;
    private float minSwipeDistance = 50f;

    public bool? swipeRight = null;

    public void Update()
    {
        int pageIndex = ChangePage();
        VerifySuwip();
    }

    void DetectSwipe(Vector2 swipeDelta)
    {
        if (swipeDelta.magnitude < minSwipeDistance)
            return; // se arrastou pouco, ignora

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            // Movimento horizontal
            if (swipeDelta.x > 0)
            {
                swipeRight = true;
                backPage();
                Debug.Log("Swipe para DIREITA");
            }
            else
            {
                swipeRight = false;
                passPage();
                Debug.Log("Swipe para ESQUERDA");
            }
        }
    }

    public void VerifySuwip()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                swipeRight = null;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 endPos = touch.position;
                DetectSwipe(endPos - startPos);
            }
        }
    }

    private void UpdateBookPageDisplay(int pageIndex)
    {
        StartCoroutine(FadeIn());

        minigameName.text = PointManager.Instance.paginaDatas[pageIndex].Title;

        for (int i = 0; i < starsDisplay.Length; i++)
        {
            if(PointManager.Instance.paginaDatas[pageIndex].Points > PointManager.Instance.paginaDatas[pageIndex].PointsPerStar[i])
                starsDisplay[i].SetActive(true);
            else starsDisplay[i].SetActive(false);
        }

        minigameThumbnail.texture = thumb[pageIndex];

        int temp = PointManager.Instance.paginaDatas[pageIndex].Points;
        PointsDisplay.text = temp.ToString();

        if (PointManager.Instance.paginaDatas[pageIndex].Points > PointManager.Instance.paginaDatas[pageIndex].PointsPerStar[0])
            Description.text = PointManager.Instance.paginaDatas[pageIndex].Description;
        else Description.text = "";
    }

    public void backPage()
    {
        currentPageNumber--;
    }
    public void passPage()
    {
        currentPageNumber++;
    }

    public void openBook()
    {
        if(!bookDisplay.activeSelf)
            DisplayBook(true);
        else if (bookDisplay.activeSelf)
            DisplayBook(false);
    }

    public int ChangePage()
    {
        if(currentPageNumber < 0)
        {
            currentPageNumber = PointManager.Instance.paginaDatas.Length - 1;
        }
        if(currentPageNumber > PointManager.Instance.paginaDatas.Length - 1)
        {
            currentPageNumber = 0;
        }

        changePageVerification(currentPageNumber);
        return currentPageNumber;
    }

    int lastpage = -1;
    bool changePageVerification(int newPage)
    {
        if (lastpage != newPage)
        {
            lastpage = newPage;
            print("Page has changed to page : " + newPage);
            UpdateBookPageDisplay(newPage);
            return true;
        }
        return false;
    }

    public void DisplayBook(bool enabled)
    {
        bookDisplay.SetActive(enabled);
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        // cores originais
        Color nameColor = minigameName.color;
        Color pointsColor = PointsDisplay.color;
        Color descColor = Description.color;

        // zera o alpha antes de começar
        nameColor.a = 0;
        pointsColor.a = 0;
        descColor.a = 0;

        minigameName.color = nameColor;
        PointsDisplay.color = pointsColor;
        Description.color = descColor;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / duration);

            nameColor.a = alpha;
            pointsColor.a = alpha;
            descColor.a = alpha;

            minigameName.color = nameColor;
            PointsDisplay.color = pointsColor;
            Description.color = descColor;

            yield return null;
        }

        // garante alpha final = 1
        nameColor.a = 1;
        pointsColor.a = 1;
        descColor.a = 1;

        minigameName.color = nameColor;
        PointsDisplay.color = pointsColor;
        Description.color = descColor;
    }
}
