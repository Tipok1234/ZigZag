using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class DataManager : MonoBehaviour
    {
        public int Score => _score;
        public int ScoreLife => _scoreLife;

        private int _score;
        private int _scoreLife;

        private string _scoreKey = "ScoreKey";
        private string _scoreLifeKey = "ScoreLife";

        public void Start()
        {
            _score = PlayerPrefs.GetInt(_scoreKey,0);
            _scoreLife = PlayerPrefs.GetInt(_scoreLifeKey,0);

        }

        public void AddScoreLife(int score)
        {
            _scoreLife += score;
            PlayerPrefs.SetInt(_scoreLifeKey, _scoreLife);
        }
        public void AddScore(int score)
        {
            _score += score;
            PlayerPrefs.SetInt(_scoreKey,_score);
        }
    }
}
