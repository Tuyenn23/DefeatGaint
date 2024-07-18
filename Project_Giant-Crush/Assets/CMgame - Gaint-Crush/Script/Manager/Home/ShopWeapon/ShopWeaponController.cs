using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopWeaponController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _content;

    [SerializeField] private List<RectTransform> L_Page;
    [SerializeField] private List<Image> L_CountPage;

    [SerializeField] private float _lastScrollPos = 0f;
    [SerializeField] private int _currentPage;
    [SerializeField] private float _currentStep;

    private float Clamp;

    Tweener T_ChangePage;

    private void OnEnable()
    {
        ResetShop();
        _scrollRect.onValueChanged.AddListener(OnScrollViewValueChanged);
    }

    private void OnScrollViewValueChanged(Vector2 normalizedPosition)
    {

    }

    private void ResetShop()
    {
        _content.localPosition = Vector3.zero;
        _currentPage = 0;
        _currentStep = 0;
        InitCountPage(_currentPage);
    }

    private void NextPage()
    {
        if (_currentPage > 2) return;

        _currentPage++;
        InitCountPage(_currentPage);

        _lastScrollPos = 0.33f * (_currentPage + 1);
    }

    private void PreviousPage()
    {
        if (_currentPage <= 0) return;

        _currentPage--;
        InitCountPage(_currentPage);

        _lastScrollPos = 0.33f * (_currentPage + 1);
    }

    private void ChangePage(int _currentPage)
    {

    }

    private void InitCountPage(int CurrentPage)
    {
        _currentPage = CurrentPage;

        for (int i = 0; i < L_Page.Count; i++)
        {
            L_CountPage[i].color = Color.white;
        }

        L_CountPage[CurrentPage].color = Color.blue;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float scrollDifference = _scrollRect.normalizedPosition.x - _lastScrollPos;

        if (Mathf.Abs(scrollDifference) > 0.1f)
        {
            if (scrollDifference < 0 && _currentPage <= L_Page.Count)
            {
                PreviousPage();

            }
            else if (scrollDifference > 0 && _currentPage >= 0)
            {
                NextPage();
            }
        }
    }

    private void OnDisable()
    {
        _scrollRect.onValueChanged.AddListener(OnScrollViewValueChanged);
    }
}
