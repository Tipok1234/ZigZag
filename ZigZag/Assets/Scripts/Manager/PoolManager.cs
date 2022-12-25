using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance => _instance;
        public List<CubeModel> CubeModels => _cubeModels;

        [SerializeField] private CubeModel _cubePrefab;
        [SerializeField] private CapsuleModel _capsulePrefab;

        [SerializeField] private Transform _cubeParent;
        [SerializeField] private Transform _capsuleParent;

        private List<CubeModel> _cubeModels;
        private List<CapsuleModel> _capsuleModels;

        private int _cubeCount = 30;
        private int _capsuleCount = 30;

        public static PoolManager _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            SetupModels();

        }

        public void SetupModels()
        {
            _cubeModels = new List<CubeModel>();
            _capsuleModels = new List<CapsuleModel>();

            for (int i = 0; i < _cubeCount; i++)
            {
                //var cube = Instantiate(_cubePrefab, _cubeParent);
                //_cubeModels.Add(cube);
                _cubeModels.Add(Instantiate(_cubePrefab, _cubeParent));
            }

            for (int i = 0; i < _capsuleCount; i++)
            {
                _capsuleModels.Add(Instantiate(_capsulePrefab, _capsuleParent));
            }

            Debug.LogError("Count: " + _cubeModels.Count);
        }
        public CubeModel GetCube(Transform pos)
        {
            for (int i = 0; i < _cubeModels.Count; i++)
            {
                if (_cubeModels[i].gameObject.activeSelf == false)
                {
                    //Debug.LogError("POOL");
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

            //if (_cubeModels.Count >= 50)
            //{
            //    _cubeModels[index].gameObject.SetActive(false);
            //    index++;
            //}

            return _cubeModels[_cubeModels.Count - 1];
        }

        public CapsuleModel GetCapsuleResource(Transform resourceTransform)
        {
            for (int i = 0; i < _capsuleModels.Count; i++)
            {
                if (_capsuleModels[i].gameObject.activeSelf == false)
                {
                    _capsuleModels[i].transform.position = resourceTransform.position;
                    _capsuleModels[i].gameObject.SetActive(true);
                    return _capsuleModels[i];
                }
            }

            for (int i = 0; i < _capsuleModels.Count; i++)
            {
                _capsuleModels[i].transform.position = resourceTransform.position;
                _capsuleModels[i].gameObject.SetActive(true);
                _capsuleModels.Add(Instantiate(_capsuleModels[i], resourceTransform));
                return _capsuleModels[_capsuleModels.Count - 1];
            }

            return null;
        }

        public void ClearModels()
        {
            for (int i = 0; i < _capsuleModels.Count; i++)
            {
                Destroy(_capsuleModels[i].gameObject);
            }

            _capsuleModels.Clear();

            for (int i = 0; i < _cubeModels.Count; i++)
            {
                Destroy(_cubeModels[i].gameObject);
            }

            _cubeModels.Clear();
            Debug.LogError("Count: " + _cubeModels.Count);
            SetupModels();
        }
        public float ReturnPosition()
        {
            return _cubeModels[_cubeModels.Count - 1].Begin.position.x;
        }

        public Transform ReturnLastPos()
        {
            int number = Random.Range(0, 2);

            Debug.LogError("Number: " + number);

            if (number == 0)
                return _cubeModels[_cubeModels.Count - 1].Begin;
            else
                return _cubeModels[_cubeModels.Count - 1].End;
        }
    }
}
