using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.Scripts.Enum;

namespace Assets.Scripts.UI
{
    public class OptionWindow : MonoBehaviour
    {
        public static event Action<ColorType> SetupColorAction;
        public static event Action<bool> ClickAIStateAction;

        [SerializeField] private Button _skinColorGreenButton;
        [SerializeField] private Button _skinColorBlueButton;
        [SerializeField] private Button _skinColorRedButton;
        [SerializeField] private Button _autoGameButton;


        [SerializeField] private Button _closeOptionButton;
        [SerializeField] private Canvas _optionCanvas;

        private void Awake()
        {
            _closeOptionButton.onClick.AddListener(Close);

            _skinColorGreenButton.onClick.AddListener(SetGreenColor);
            _skinColorBlueButton.onClick.AddListener(SetBlueColor);
            _skinColorRedButton.onClick.AddListener(SetRedColor);
            _autoGameButton.onClick.AddListener(AutoGame);
        }

        private void Close()
        {
            _optionCanvas.enabled = !_optionCanvas.enabled;
            Time.timeScale = 1f;
        }

        private void SetGreenColor()
        {
            SetupColorAction?.Invoke(ColorType.Green);
        }
        private void SetBlueColor()
        {
            SetupColorAction?.Invoke(ColorType.Blue);
        }
        private void SetRedColor()
        {
            SetupColorAction?.Invoke(ColorType.Red);
        }

        private void AutoGame()
        {
            ClickAIStateAction?.Invoke(true);
        }
    }
}
