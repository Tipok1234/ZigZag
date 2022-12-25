using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RestartWindow : MonoBehaviour
    {
        public event Action RestartGameAction;

        [SerializeField] private Canvas _restartCanvas;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        public void RestartGame()
        {
            _restartCanvas.enabled = !_restartCanvas.enabled;
            Time.timeScale = 1f;
            RestartGameAction?.Invoke();
        }

        public void ShowRestartWindow()
        {
            Time.timeScale = 0f;
            _restartCanvas.enabled = !_restartCanvas.enabled;
        }
    }
}
