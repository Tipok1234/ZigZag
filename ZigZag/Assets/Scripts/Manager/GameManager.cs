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

        [SerializeField] private OptionWindow _optionWindow;
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
        private DataManager _dataManager;

        private void Awake()
        {
            _dataManager = FindObjectOfType<DataManager>();
        }


        private void Start()
        {
            _gameWindow.SetupScore(_dataManager.Score);
            _gameWindow.SetupScoreLife(_dataManager.ScoreLife);
            _mainWindow.StartGameAction += OnStartGame;
            _restartWindow.RestartGameAction += OnRestartGame;
            _optionWindow.SetupColorAction += OnSetupColor;
            _optionWindow.ClickAIStateAction += OnAIState;
        }

        private void OnDestroy()
        {
            _deathTrig.DeathAction -= OnDeath;
            _mainWindow.StartGameAction -= OnStartGame;
            _player.CollectedScoreAction -= OnCollectedScore;
            _restartWindow.RestartGameAction -= OnRestartGame;
            _optionWindow.SetupColorAction -= OnSetupColor;
            _optionWindow.ClickAIStateAction -= OnAIState;
        }

        private void Update()
        {
            if (_cubeModel == null || _player == null)
                return;


            var lastPos = PoolManager.Instance.CubeModels[PoolManager.Instance.CubeModels.Count - 1];

            if (Vector3.Distance(_player.transform.position, lastPos.transform.position) < 100)
            {
                var cubes = PoolManager.Instance.CubeModels;
                var firstCube = cubes[0];
                firstCube.gameObject.SetActive(false);
                cubes.RemoveAt(0);

                if (GetNumber() == 0)
                {
                    var newPos = cubes[cubes.Count - 1].Begin.position;
                    firstCube.transform.position = newPos;
                }
                else
                {
                    var newPos = cubes[cubes.Count - 1].End.position;
                    firstCube.transform.position = newPos;
                }

                firstCube.gameObject.SetActive(true);
                cubes.Add(firstCube);

                if (GetNumber() == 0)
                    PoolManager.Instance.GetCapsuleResource(firstCube.SpawnCapsule.transform);
            }
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
            AudioManager.Instance.PlaySound();
            _dataManager.AddScore(1);
            var newScore = PoolManager.Instance.GetScoreUI(_player.transform);
            newScore.SetupScoreTextAnimation(1);
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
            _restartWindow.ShowRestartWindow();
            _dataManager.AddScoreLife(1);
        }
        private void StartGame()
        {
            _gameWindow.SetupScoreLife(_dataManager.ScoreLife);
            InstantiateOnStart();

            for (int i = 0; i < PoolManager.Instance.CubeModels.Count; i++)
            {
                var newCube = PoolManager.Instance.GetCube(_cubePos);

                if (GetNumber() == 0)
                    _cubePos.position = newCube.End.position;
                else
                {
                    _cubePos.position = newCube.Begin.position;
                    var capsule = PoolManager.Instance.GetCapsuleResource(newCube.SpawnCapsule);
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

        private int GetNumber()
        {
            int number = UnityEngine.Random.Range(0, 2);

            if (number == 0)
                return 0;
            else
                return 1;
        }
    }
}
