using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AdvancedButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    public readonly UnityEvent OnClick = new UnityEvent();
    public readonly UnityEvent OnEnter = new UnityEvent();
    public readonly UnityEvent OnExit = new UnityEvent();

    #region UNITY_INSPECTOR

    [Header("Text")]
    [SerializeField] private RectTransform mainTextMaskRect;
    [SerializeField] private string text;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private Color mainTextColor = Color.white;
    [SerializeField] private TMP_Text reversedColorText;
    [SerializeField] private Color reversedTextColor = Color.black;
    [SerializeField] private float fontSize = 20;

    [Header("Indicator")]
    [SerializeField] private RectTransform indicator;
    [SerializeField] private float indicatorDeactiveSizeRatio = 0.05f;
    [SerializeField] private float indicatorAnimationDuration = 0.5f;

    [Header("Event")]
    [SerializeField] private UnityEvent _onClick;
    [SerializeField] private UnityEvent _onEnter;
    [SerializeField] private UnityEvent _onExit;
    #endregion

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();

        SetText(text);

        SetIndicatorAnimtaion(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick.Invoke();
        OnClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onEnter.Invoke();
        OnEnter.Invoke();

        SetIndicatorAnimtaion(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _onExit.Invoke();
        OnExit.Invoke();

        SetIndicatorAnimtaion(false);
    }

    private void SetIndicatorAnimtaion(bool isActive)
    {
        if (isActive)
        {
            indicator.DOSizeDelta(new Vector2(rectTransform.rect.width,
                indicator.sizeDelta.y), indicatorAnimationDuration);

            mainTextMaskRect.DOSizeDelta(new Vector2(rectTransform.rect.width * indicatorDeactiveSizeRatio,
                mainTextMaskRect.sizeDelta.y), indicatorAnimationDuration);
        }
        else
        {
            indicator.DOSizeDelta(new Vector2(rectTransform.rect.width * indicatorDeactiveSizeRatio,
                indicator.sizeDelta.y), indicatorAnimationDuration);

            mainTextMaskRect.DOSizeDelta(new Vector2(rectTransform.rect.width,
                mainTextMaskRect.sizeDelta.y), indicatorAnimationDuration);
        }
    }

    public void SetText(string text)
    {
        this.text = text;
        UpdateUI();
    }

    public void UpdateUI()
    {
        var mainTextRect = mainText.GetComponent<RectTransform>();
        mainTextRect.sizeDelta = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        mainTextRect.position = rectTransform.position;
        mainText.text = text;
        mainText.color = mainTextColor;
        mainText.fontSize = fontSize;

        var reversedTextRect = reversedColorText.GetComponent<RectTransform>();
        reversedTextRect.sizeDelta = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        reversedTextRect.position = rectTransform.position;
        reversedColorText.text = text;
        reversedColorText.color = reversedTextColor;
        reversedColorText.fontSize = fontSize;
    }
}
