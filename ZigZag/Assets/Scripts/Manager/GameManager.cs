using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Model;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public event Action<Transform> GameStartedAction;

        [SerializeField] private PlayerModel _playerPrefab;

        [SerializeField] private Transform _playerPos;
        [SerializeField] private Transform _cubePos;

        private PlayerModel _player;
        private CubeModel _cubeModel;
        private void Start()
        {
            InstantiateOnStart();

            for (int i = 0; i < 20; i++)
            {
                var newCube =  PoolManager.Instance.GetCube(_cubePos);

                int number = UnityEngine.Random.Range(0, 2);
                if (number == 0)
                    _cubePos.position = newCube.End.position;
                else
                    _cubePos.position = newCube.Begin.position;

                _cubeModel = newCube;
            }
        }

        private void Update()
        {
            if (_cubeModel == null)
                return;

            if (_player.transform.position.x < PoolManager.Instance.ReturnPosition() + 100)
                PoolManager.Instance.GetCube(_cubeModel.transform);
        }
        private void InstantiateOnStart()
        {
            var newPlayer = Instantiate(_playerPrefab, _playerPos);
            _player = newPlayer;
            GameStartedAction?.Invoke(_player.transform);
        }
    }
}
