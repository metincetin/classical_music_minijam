using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTimeline: MonoBehaviour
{
    [SerializeField]
    private CanvasGroup[] _pages;

    [SerializeField]
    private Image _background;

    private int _pageIndex;

    private void OnEnable()
    {
        ShowCurrentPage();
        _background.DOFade(1f, 1f);
    }

    private void ShowCurrentPage()
    {
        if (_pageIndex - 1 >= 0)
        {
            var prevPage = _pages[_pageIndex - 1];
            prevPage.interactable = false;
            prevPage.blocksRaycasts = false;
        }

        var curPage = _pages[_pageIndex];
        curPage.DOKill();
        var tw = curPage.DOFade(1, .7f).OnComplete(() =>
        {
            curPage.interactable = true;
            curPage.blocksRaycasts = true;
        });
    }

    public void NextPage()
    {
        _pageIndex++;
        ShowCurrentPage();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
