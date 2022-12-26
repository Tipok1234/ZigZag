using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.UI;

namespace Assets.Scripts.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance => _instance;
        public List<CubeModel> CubeModels => _cubeModels;

        [SerializeField] private CubeModel _cubePrefab;
        [SerializeField] private CapsuleModel _capsulePrefab;
        [SerializeField] private ScoreAnimationUI _scoreAnimationUI;

        [SerializeField] private Transform _cubeParent;
        [SerializeField] private Transform _capsuleParent;
        [SerializeField] private Transform _scoreParent;

        private List<CubeModel> _cubeModels;
        private List<CapsuleModel> _capsuleModels;
        private List<ScoreAnimationUI> _scoreAnimationUIs;

        private int _cubeCount = 30;
        private int _capsuleCount = 30;
        private int _scoreCount = 10;

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
            _scoreAnimationUIs = new List<ScoreAnimationUI>();

            for (int i = 0; i < _cubeCount; i++)
            {
                _cubeModels.Add(Instantiate(_cubePrefab, _cubeParent));
            }

            for (int i = 0; i < _capsuleCount; i++)
            {
                _capsuleModels.Add(Instantiate(_capsulePrefab, _capsuleParent));
            }

            for (int i = 0; i < _scoreCount; i++)
            {
                _scoreAnimationUIs.Add(Instantiate(_scoreAnimationUI, _scoreParent));
            }
        }
        public CubeModel GetCube(Transform pos)
        {
            for (int i = 0; i < _cubeModels.Count; i++)
            {
                if (_cubeModels[i].gameObject.activeSelf == false)
                {
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

            var newRes = Instantiate(_capsulePrefab, resourceTransform);
            newRes.transform.position = resourceTransform.position;
            newRes.gameObject.SetActive(true);
            _capsuleModels.Add(newRes);

            return newRes;
        }


        public ScoreAnimationUI GetScoreUI(Transform scorePos)
        {
            for (int i = 0; i < _scoreAnimationUIs.Count; i++)
            {
                if (_scoreAnimationUIs[i].gameObject.activeSelf == false)
                {
                    _scoreAnimationUIs[i].transform.position = scorePos.position;
                    _scoreAnimationUIs[i].gameObject.SetActive(true);

                    return _scoreAnimationUIs[i];
                }
            }

            var newModel = Instantiate(_scoreAnimationUI, scorePos);
            newModel.transform.position = scorePos.position;
            newModel.gameObject.SetActive(true);
            _scoreAnimationUIs.Add(newModel);

            return _scoreAnimationUIs[_scoreAnimationUIs.Count - 1];
        }

        public void ReturnScoreUIToPool(ScoreAnimationUI scoreUI)
        {
            scoreUI.gameObject.SetActive(false);
            scoreUI.transform.position = _scoreParent.position;
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
            SetupModels();
        }
    }
}
