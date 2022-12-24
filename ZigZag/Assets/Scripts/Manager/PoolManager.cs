using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance => _instance;

        [SerializeField] private CubeModel _cubePrefab;
        [SerializeField] private Transform _cubeParent;

        [SerializeField] private CubeModel _firstCube;

        private List<CubeModel> _cubeModels;

        private int _cubeCount = 30;
        public static PoolManager _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            _cubeModels = new List<CubeModel>();

            for (int i = 0; i < _cubeCount; i++)
            {
                _cubeModels.Add(Instantiate(_cubePrefab, _cubeParent));
            }
        }

        public CubeModel GetCube(Transform pos)
        {
            for (int i = 0; i < _cubeModels.Count; i++)
            {
                if (_cubeModels[i].gameObject.activeSelf == false)
                {
                    Debug.LogError("POOL");
                    _cubeModels[i].transform.position = pos.position;

                    _cubeModels[i].gameObject.SetActive(true);
                    return _cubeModels[i];
                }
            }

            int number1 = Random.Range(0, 2);
            var newCube = Instantiate(_cubePrefab, _cubeParent);

            if (number1 == 0)
                newCube.transform.position = _cubeModels[_cubeModels.Count - 1].Begin.position;
            else
                newCube.transform.position = _cubeModels[_cubeModels.Count - 1].End.position;

            newCube.gameObject.SetActive(true);
            _cubeModels.Add(newCube);

            if (_cubeModels.Count >= 50)
            {
                _cubeModels.RemoveAt(0);
                _cubeModels[0].gameObject.SetActive(false);
                Debug.LogError("Count: " + _cubeModels.Count);
            }

            return _cubeModels[0];
        }

        public float ReturnPosition()
        {
            return _cubeModels[_cubeModels.Count - 1].Begin.position.x;
        }
    }
}
