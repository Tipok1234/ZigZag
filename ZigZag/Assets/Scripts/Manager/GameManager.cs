using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Model;
using Assets.Scripts.UI;
using Assets.Scripts.Enum;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public event Action<Transform> GameStartedAction;

        [SerializeField] private MainWindow _mainWindow;
        [SerializeField] private GameWindow _gameWindow;
        [SerializeField] private RestartWindow _restartWindow;
        [SerializeField] private PlayerModel _playerPrefab;
        [SerializeField] private DeathTrigger _deathTrigger;

        [SerializeField] private Transform _playerPos;
        [SerializeField] private Transform _cubePos;

        private PlayerModel _player;
        private DeathTrigger _deathTrig;
        private CubeModel _cubeModel;
        private Transform _spawnResource;

        private List<CapsuleModel> _capsuleModels = new List<CapsuleModel>();
        private DataManager _dataManager;

        private void Awake()
        {
            _dataManager = FindObjectOfType<DataManager>();
        }


        private void Start()
        {
            _gameWindow.SetupScore(_dataManager.Score);
            _mainWindow.StartGameAction += OnStartGame;
            _restartWindow.RestartGameAction += OnRestartGame;
            OptionWindow.SetupColorAction += OnSetupColor;
            OptionWindow.ClickAIStateAction += OnAIState;
        }

        private void OnDestroy()
        {
            // _deathTrigger.DeathAction -= OnDeath;
            _mainWindow.StartGameAction -= OnStartGame;
            _player.CollectedScoreAction -= OnCollectedScore;
            _restartWindow.RestartGameAction -= OnRestartGame;
            OptionWindow.SetupColorAction -= OnSetupColor;
            OptionWindow.ClickAIStateAction -= OnAIState;
        }

        private void Update()
        {
            if (_cubeModel == null || _player == null)
                return;

            if (_player.transform.position.x < PoolManager.Instance.ReturnPosition() + 100)
            {
                var cube = PoolManager.Instance.GetCube(PoolManager.Instance.ReturnLastPos());

                _spawnResource = cube.SpawnCapsule.transform;

                var capsule = PoolManager.Instance.GetCapsuleResource(_spawnResource);
                _capsuleModels.Add(capsule);
            }

            //if (_player.IsAI)
            //{
            //    for (int i = 0; i < _capsuleModels.Count; i++)
            //    {
            //        if (_player.transform.position == _capsuleModels[i].transform.position)
            //            _player.AIMove(_capsuleModels[i].transform);
            //    }
            //}
        }

        private void OnSetupColor(ColorType colorType)
        {
            _player.SetColor(colorType);
        }

        private void OnAIState(bool state)
        {
            _player.SetAI(state);

        }

        private void OnCollectedScore()
        {
            _dataManager.AddScore(1);
            _gameWindow.SetupScore(_dataManager.Score);
        }

        private void OnRestartGame()
        {
            _cubePos.position = new Vector3(-4.4f, 0f, 9.5f);
            _player.DestroyObject();
            _deathTrig.DestroyObject();
            PoolManager.Instance.ClearModels();
            StartGame();
        }
        private void OnStartGame()
        {
            _gameWindow.SetupGameCanvas();
            StartGame();
        }

        private void OnDeath()
        {
            Debug.LogError("Death");
            _restartWindow.ShowRestartWindow();
        }
        private void StartGame()
        {   
            InstantiateOnStart();

            for (int i = 0; i < PoolManager.Instance.CubeModels.Count; i++)
            {
                var newCube = PoolManager.Instance.GetCube(_cubePos);

                int number = UnityEngine.Random.Range(0, 2);
                if (number == 0)
                    _cubePos.position = newCube.End.position;
                else
                {
                    _cubePos.position = newCube.Begin.position;
                    var capsule = PoolManager.Instance.GetCapsuleResource(newCube.SpawnCapsule);
                    _capsuleModels.Add(capsule);
                }

                _cubeModel = newCube;
            }
        }
        private void InstantiateOnStart()
        {
            var newPlayer = Instantiate(_playerPrefab, _playerPos);
            _player = newPlayer;
            var newTrigger = Instantiate(_deathTrigger, _playerPos);
            _deathTrig = newTrigger;
            newTrigger.GameStarted(_player.transform);
            newTrigger.DeathAction += OnDeath;
            GameStartedAction?.Invoke(_player.transform);
            _player.CollectedScoreAction += OnCollectedScore;
        }
    }
}
