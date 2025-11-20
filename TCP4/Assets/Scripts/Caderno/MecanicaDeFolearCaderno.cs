using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MecanicaDeFolearCaderno : MonoBehaviour
{
    [Header("Display")]
    #region Display
    [SerializeField] TextMeshProUGUI minigameName;
    [SerializeField] Image minigameThumbnail;
    [SerializeField] TextMeshProUGUI PointsDisplay;
    [SerializeField] TextMeshProUGUI Description;
    #endregion

    [Header("")]
    [Header("page")]
    #region page
    [SerializeField] PaginaData[] pagnasInfo;
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

    public void Update()
    {
        int pageIndex = ChangePage();
    }

    private void UpdateBookPageDisplay(int pageIndex)
    {
        StartCoroutine(FadeIn());
        minigameName.text = pagnasInfo[pageIndex].Title;
        minigameThumbnail = pagnasInfo[pageIndex].minigameThumbnail;
        PointsDisplay.text = pagnasInfo[pageIndex].Points.ToString();
        Description.text = pagnasInfo[pageIndex].Description;
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
            currentPageNumber = pagnasInfo.Length - 1;
        }
        if(currentPageNumber > pagnasInfo.Length - 1)
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
