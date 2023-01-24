using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.sharmas4.MentalHealthDisorder
{
    public class TabManager : MonoBehaviour
    {
        [SerializeField] GameObject[] tabsGOs;
        [SerializeField] Button[] tabButtons;
        [SerializeField] private Color highlightedTabColor;
        [SerializeField] private Color defaultTabColor;


        private UnityAction[] actions;
        private GameObject currentTab;
        private Button currentButton;

        // Start is called before the first frame update
        private void Start()
        {
            OnTabButtonPressed(0);
        }



        void OnEnable()
        {
            actions = new UnityAction[tabButtons.Length];
            for (int i = 0; i < tabButtons.Length; i++)
            {
                // Storing index in a local variable since closures reference the same copy of the i variable (last value)
                int index = i;
                actions[index] = () => { OnTabButtonPressed(index); };
                tabButtons[index].onClick.AddListener(actions[index]);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < tabButtons.Length; i++)
            {
                int index = i;
                tabButtons[index].onClick.RemoveListener(actions[i]);
            }
        }

        void OnTabButtonPressed(int index)
        {
            if (currentTab != null)
            {
                currentTab.SetActive(false);
            }

            if (currentButton != null)
                currentButton.image.color = defaultTabColor;

            currentButton = tabButtons[index];
            currentTab = tabsGOs[index];
            currentButton.image.color = highlightedTabColor;
            currentTab.SetActive(true);
        }
    }
}


enum tabs
{
    Presets = 0,
    Controls,
    Symptoms,
    PostProcessing,
    Other
}