using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Manager;

namespace Assets.Scripts.UI
{
    public class GameWindow : MonoBehaviour
    {
        [SerializeField] private Button _settingButton;
        [SerializeField] private Canvas _optionCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private TMP_Text _scoreText;

        private void Awake()
        {
            _settingButton.onClick.AddListener(OptionMenu);
        }

        private void OptionMenu()
        {
            AudioManager.Instance.PlaySound();
            _optionCanvas.enabled = !_optionCanvas.enabled;
            Time.timeScale = 0f;
        }

        public void SetupScore(int score)
        {
            _scoreText.text = "Score: " + score.ToString();
        }

        public void SetupGameCanvas()
        {
            _gameCanvas.enabled = !_gameCanvas.enabled;
        }

    }
}
