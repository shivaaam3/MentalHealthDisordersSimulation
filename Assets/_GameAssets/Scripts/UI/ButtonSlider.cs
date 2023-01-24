using TMPro;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class ButtonSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button plus;
    [SerializeField] private Button minus;
    [SerializeField] private Slider slider;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float incrementValue;

    [SerializeField] private TMP_Text sliderValueText;
    [SerializeField] private GameObject sliderValueGO;


    public Action<float> OnValueChanged;
    private bool isSliderInit = false;


    // Since the slider is disabled by default.
    // min, max values can't be set in init methods like Awake, Start etc.
    private void InitSlider()
    {
        if (isSliderInit) return;
        isSliderInit = true;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        sliderValueText.text = slider.value.ToString();
    }

    void OnEnable()
    {
        plus.onClick.AddListener(OnPlusClicked);
        minus.onClick.AddListener(OnMinusClicked);
        InitSlider();
    }

    void OnDisable()
    {
        plus.onClick.RemoveListener(OnPlusClicked);
        minus.onClick.RemoveListener(OnMinusClicked);
    }

    private void OnPlusClicked()
    {
        float value = Mathf.Clamp(slider.value + incrementValue, minValue, maxValue);
        ApplyNewData(value);
    }

    private void OnMinusClicked()
    {
        float value = Mathf.Clamp(slider.value - incrementValue, minValue, maxValue);
        ApplyNewData(value);
    }

    public void ApplyNewData(float sliderVal)
    {
        InitSlider();
        slider.value = sliderVal;
        sliderValueText.text = sliderVal.ToString();
        OnValueChanged?.Invoke(sliderVal);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sliderValueGO.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sliderValueGO.SetActive(false);

    }
}
