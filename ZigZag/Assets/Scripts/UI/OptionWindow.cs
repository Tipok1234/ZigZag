using UnityEngine;
using System;
using Assets.Scripts.Enum;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Manager;

namespace Assets.Scripts.UI
{
    public class OptionWindow : MonoBehaviour
    {
        public event Action<ColorType> SetupColorAction;
        public event Action<bool> ClickAIStateAction;

        [SerializeField] private Button _AIGameButton;
        [SerializeField] private Button _playerGameButton;
        [SerializeField] private Button _skinColorGreenButton;
        [SerializeField] private Button _skinColorBlueButton;
        [SerializeField] private Button _skinColorRedButton;
        [SerializeField] private Button _closeOptionButton;
        [SerializeField] private Canvas _optionCanvas;

        private void Awake()
        {
            _closeOptionButton.onClick.AddListener(Close);

            _skinColorGreenButton.onClick.AddListener(SetGreenColor);
            _skinColorBlueButton.onClick.AddListener(SetBlueColor);
            _skinColorRedButton.onClick.AddListener(SetRedColor);
            _AIGameButton.onClick.AddListener(AutoGame);
            _playerGameButton.onClick.AddListener(PlayerGame);
        }

        private void Close()
        {
            AudioManager.Instance.PlaySound();
            _optionCanvas.enabled = !_optionCanvas.enabled;
            Time.timeScale = 1f;
        }

        private void SetGreenColor()
        {
            AudioManager.Instance.PlaySound();
            SetupColorAction?.Invoke(ColorType.Green);
        }
        private void SetBlueColor()
        {
            AudioManager.Instance.PlaySound();
            SetupColorAction?.Invoke(ColorType.Blue);
        }
        private void SetRedColor()
        {
            AudioManager.Instance.PlaySound();
            SetupColorAction?.Invoke(ColorType.Red);
        }

        private void AutoGame()
        {
            AudioManager.Instance.PlaySound();

            ClickAIStateAction?.Invoke(true);
            _AIGameButton.gameObject.SetActive(false);
            _playerGameButton.gameObject.SetActive(true);
        }

        private void PlayerGame()
        {
            ClickAIStateAction?.Invoke(false);
            _AIGameButton.gameObject.SetActive(true);
            _playerGameButton.gameObject.SetActive(false);
        }
    }
}
