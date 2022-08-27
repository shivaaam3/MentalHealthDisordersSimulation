using TMPro;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Toggle : MonoBehaviour, IPointerDownHandler {

    [SerializeField] private GameObject leftOption;
    [SerializeField] private GameObject rightOption;


    [SerializeField] private Image highlighter;
    [SerializeField] private Color highlightedOptionColor;
    [SerializeField] private Color defaultOptionColor;
    [SerializeField] private float tweenTime;

    private ToggleSide currentSide;
    private ToggleState currentState = ToggleState.Switched;
    private RectTransform highlighterTransform;

    public GameObject HighlightedOption { get; private set; }
    public GameObject NonHighlightedOption { get; private set; }
    public Action<ToggleSide> OnValueChanged;


    public void OnEnable() {
        SwitchToggle(currentSide);
    }

    public void SwitchToggle(float tweenTime = 0) {
        if (currentState == ToggleState.Switching) return;

        currentState = ToggleState.Switching;
        switch (currentSide) {
            case ToggleSide.Left:
                currentSide = ToggleSide.Right;
                HighlightedOption = rightOption;
                NonHighlightedOption = leftOption;
                break;
            case ToggleSide.Right:
                currentSide = ToggleSide.Left;
                HighlightedOption = leftOption;
                NonHighlightedOption = rightOption;
                break;
            default:
                break;
        }
        OnValueChanged?.Invoke(currentSide);

        if (gameObject.activeInHierarchy)
            StartCoroutine(TweenToggle(tweenTime));
    }

    public void SwitchToggle(ToggleSide side, float tweenTime = 0) {
        if (currentState == ToggleState.Switching) return;

        currentState = ToggleState.Switching;
        switch (side) {
            case ToggleSide.Right:
                currentSide = ToggleSide.Right;
                HighlightedOption = rightOption;
                NonHighlightedOption = leftOption;
                break;
            case ToggleSide.Left:
                currentSide = ToggleSide.Left;
                HighlightedOption = leftOption;
                NonHighlightedOption = rightOption;
                break;
            default:
                break;
        }
        OnValueChanged?.Invoke(currentSide);

        if (gameObject.activeInHierarchy)
            StartCoroutine(TweenToggle(tweenTime));
    }


    private IEnumerator TweenTextColor(TMP_Text text, Color color, float tweenTime) {
        Color initialColor = text.color;
        float t = 0;

        while (t <= tweenTime) {
            text.color = Color.Lerp(text.color, color, t);
            t += Time.deltaTime;
            yield return null;
        }
        text.color = color;
    }

    private IEnumerator TweenToggle(float tweenTime) {
        float initialX = NonHighlightedOption.GetComponent<RectTransform>().anchoredPosition.x;
        float finalX = HighlightedOption.GetComponent<RectTransform>().anchoredPosition.x;
        float currentX = 0;
        TMP_Text highlightedOptionText = HighlightedOption.GetComponent<TMP_Text>();
        TMP_Text nonHighlightedOptionText = NonHighlightedOption.GetComponent<TMP_Text>();

        StartCoroutine(TweenTextColor(highlightedOptionText, highlightedOptionColor, tweenTime));
        StartCoroutine(TweenTextColor(nonHighlightedOptionText, defaultOptionColor, tweenTime));

        float t = 0;
        while (t <= tweenTime) {
            currentX = Mathf.Lerp(currentX, finalX, t);
            highlighterTransform.anchoredPosition = new Vector2(currentX, highlighterTransform.anchoredPosition.y);
            t += Time.deltaTime;
            yield return null;
        }

        highlighterTransform.anchoredPosition = new Vector2(finalX, highlighterTransform.anchoredPosition.y);
        currentState = ToggleState.Switched;
    }

    void Start() {
        highlighterTransform = highlighter.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        SwitchToggle(tweenTime);
    }


}

public enum ToggleSide {
    Left,
    Right
}

public enum ToggleState {
    Switching,
    Switched
}
