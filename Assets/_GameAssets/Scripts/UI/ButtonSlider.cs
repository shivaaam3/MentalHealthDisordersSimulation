using TMPro;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private Button plus;
    [SerializeField] private Button minus;
    [SerializeField] public Slider slider;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float incrementValue;

    [SerializeField] private TMP_Text sliderValueText;
    [SerializeField] private GameObject sliderValueGO;


    public Action<float> OnValueChanged;


    // Start is called before the first frame update
    private void Start() {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
    }

    void OnEnable() {
        plus.onClick.AddListener(OnPlusClicked);
        minus.onClick.AddListener(OnMinusClicked);
    }

    void OnDisable() {
        plus.onClick.RemoveListener(OnPlusClicked);
        minus.onClick.RemoveListener(OnMinusClicked);
    }

    private void OnPlusClicked() {
        float value = Mathf.Clamp(slider.value + incrementValue, minValue, maxValue);
        ApplyNewData(value);
    }

    private void OnMinusClicked() {
        float value = Mathf.Clamp(slider.value - incrementValue, minValue, maxValue);
        ApplyNewData(value);
    }

    public void ApplyNewData(float sliderVal) {
        slider.value = sliderVal;
        sliderValueText.text = slider.value.ToString();
        OnValueChanged?.Invoke(slider.value);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        sliderValueGO.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        sliderValueGO.SetActive(false);

    }
}
