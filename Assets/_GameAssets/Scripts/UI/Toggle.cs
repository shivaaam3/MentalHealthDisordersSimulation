using TMPro;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//using static System.Net.Mime.MediaTypeNames;
//using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;

public class Toggle : MonoBehaviour, IPointerDownHandler {

    [SerializeField] private GameObject leftOption;
    [SerializeField] private GameObject rightOption;

    [SerializeField] private Image highlighter;
    [SerializeField] private Color highlightedOptionColor;
    [SerializeField] private Color defaultOptionColor;
    [SerializeField] private float tweenTime;
    [SerializeField] GameObject[] options = new GameObject[2];

    private ToggleSide currentSide = ToggleSide.Undefined;
    private ToggleState currentState = ToggleState.Switched;

    public GameObject HighlightedOption { get; private set; }
    public GameObject NonHighlightedOption { get; private set; }
    public Action<ToggleSide> OnValueChanged;


    private float InitialX { get { return NonHighlightedOption.GetComponent<RectTransform>().anchoredPosition.x; } }
    private float FinalX { get { return HighlightedOption.GetComponent<RectTransform>().anchoredPosition.x; } }
    private TMP_Text HighlightedOptionText { get { return HighlightedOption.GetComponent<TMP_Text>(); } }
    private TMP_Text NonHighlightedOptionText { get { return NonHighlightedOption.GetComponent<TMP_Text>(); } }
    private RectTransform highlighterTransform { get { return highlighter.GetComponent<RectTransform>(); } }


    public void OnEnable() {
        //SwitchToggle(currentSide);
    }


    public void SwitchToggle(float tweenTime = 0) {

        currentSide = 1 - currentSide;
        SwitchToggleTo(currentSide, tweenTime);
    }


    public void SwitchToggleTo(ToggleSide side, float tweenTime = 0) {
        currentSide = side;
        HighlightedOption = options[(int)currentSide];
        NonHighlightedOption = options[1 - (int)currentSide];

        OnValueChanged?.Invoke(currentSide);

        if (gameObject.activeInHierarchy && currentState == ToggleState.Switched)
            StartCoroutine(TweenToggle(tweenTime));
        else {
            MoveHighlighter(new Vector2(FinalX, highlighterTransform.anchoredPosition.y));
            ChangeTextColor(HighlightedOptionText, highlightedOptionColor);
            ChangeTextColor(NonHighlightedOptionText, defaultOptionColor);
        }
    }


    private IEnumerator TweenTextColor(TMP_Text text, Color color, float tweenTime) {
        Color initialColor = text.color;
        float t = 0;

        while (t <= tweenTime) {
            ChangeTextColor(text, Color.Lerp(text.color, color, t));
            t += Time.deltaTime;
            yield return null;
        }
        ChangeTextColor(text, color);
    }

    private IEnumerator TweenToggle(float tweenTime) {
        currentState = ToggleState.Switching;
        float currentX = InitialX;
        StartCoroutine(TweenTextColor(HighlightedOptionText, highlightedOptionColor, tweenTime));
        StartCoroutine(TweenTextColor(NonHighlightedOptionText, defaultOptionColor, tweenTime));

        float t = 0;
        while (t <= tweenTime) {
            currentX = Mathf.Lerp(currentX, FinalX, t);
            MoveHighlighter(new Vector2(currentX, highlighterTransform.anchoredPosition.y));
            t += Time.deltaTime;
            yield return null;
        }

        MoveHighlighter(new Vector2(FinalX, highlighterTransform.anchoredPosition.y));
        currentState = ToggleState.Switched;
    }



    public void OnPointerDown(PointerEventData eventData) {
        SwitchToggle(tweenTime);
    }

    private void MoveHighlighter(Vector2 finalPos) {
        highlighterTransform.anchoredPosition = finalPos;
    }

    private void ChangeTextColor(TMP_Text text, Color color) {
        text.color = color;
    }

}



public enum ToggleSide {
    Left,
    Right,
    Undefined
}

public enum ToggleState {
    Switching,
    Switched
}
