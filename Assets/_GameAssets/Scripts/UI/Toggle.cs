using TMPro;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Toggle : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private GameObject leftOption;
    [SerializeField] private GameObject rightOption;

    [SerializeField] private Image highlighter;
    [SerializeField] private Color highlightedOptionColor;
    [SerializeField] private Color defaultOptionColor;
    [SerializeField] private float tweenTime;
    [SerializeField] GameObject[] options = new GameObject[2];

    private ToggleSide currentSide = ToggleSide.Left;
    private ToggleState currentState = ToggleState.Switched;

    public GameObject HighlightedOption { get; private set; }
    public GameObject NonHighlightedOption { get; private set; }
    public Action<ToggleSide> OnValueChanged;


    private void OnEnable()
    {
        //SwitchToggle(currentSide);
    }

    float GetInitialX()
    {
        return NonHighlightedOption.GetComponent<RectTransform>().anchoredPosition.x;
    }

    float GetFinalX()
    {
        return HighlightedOption.GetComponent<RectTransform>().anchoredPosition.x;
    }

    TMP_Text GetHighlightedOptionText()
    {
        return HighlightedOption.GetComponent<TMP_Text>();
    }

    TMP_Text GetNonHighlightedOptionText()
    {
        return NonHighlightedOption.GetComponent<TMP_Text>();
    }

    RectTransform GetHighLighterTransform()
    {
        return highlighter.GetComponent<RectTransform>();
    }


    private void SwitchToggle(float tweenTime = 0)
    {
        currentSide = 1 - currentSide;
        SwitchToggleTo(currentSide, tweenTime);
    }


    public void SwitchToggleTo(ToggleSide side, float tweenTime = 0)
    {
        print(side);
        currentSide = side;
        HighlightedOption = options[(int)currentSide];
        NonHighlightedOption = options[1 - (int)currentSide];

        OnValueChanged?.Invoke(currentSide);

        if (gameObject.activeInHierarchy && currentState == ToggleState.Switched)
            StartCoroutine(TweenToggle(tweenTime));
        else
        {
            MoveHighlighter(new Vector2(GetFinalX(), GetHighLighterTransform().anchoredPosition.y));
            ChangeTextColor(GetHighlightedOptionText(), highlightedOptionColor);
            ChangeTextColor(GetNonHighlightedOptionText(), defaultOptionColor);
        }
    }


    private IEnumerator TweenTextColor(TMP_Text text, Color color, float tweenTime)
    {
        Color initialColor = text.color;
        float t = 0;

        while (t <= tweenTime)
        {
            ChangeTextColor(text, Color.Lerp(text.color, color, t));
            t += Time.deltaTime;
            yield return null;
        }
        ChangeTextColor(text, color);
    }

    private IEnumerator TweenToggle(float tweenTime)
    {
        currentState = ToggleState.Switching;
        float currentX = GetInitialX();
        StartCoroutine(TweenTextColor(GetHighlightedOptionText(), highlightedOptionColor, tweenTime));
        StartCoroutine(TweenTextColor(GetNonHighlightedOptionText(), defaultOptionColor, tweenTime));

        float t = 0;
        while (t <= tweenTime)
        {
            currentX = Mathf.Lerp(currentX, GetFinalX(), t);
            MoveHighlighter(new Vector2(currentX, GetHighLighterTransform().anchoredPosition.y));
            t += Time.deltaTime;
            yield return null;
        }

        MoveHighlighter(new Vector2(GetFinalX(), GetHighLighterTransform().anchoredPosition.y));
        currentState = ToggleState.Switched;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        SwitchToggle(tweenTime);
    }

    private void MoveHighlighter(Vector2 finalPos)
    {
        GetHighLighterTransform().anchoredPosition = finalPos;
    }

    private void ChangeTextColor(TMP_Text text, Color color)
    {
        text.color = color;
    }

}



public enum ToggleSide
{
    Left,
    Right
}

public enum ToggleState
{
    Switching,
    Switched
}
