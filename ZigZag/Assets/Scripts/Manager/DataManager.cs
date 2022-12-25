using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class DataManager : MonoBehaviour
    {
        public int Score => _score;

        private int _score;

        private string _scoreKey = "ScoreKey";

        public void Start()
        {
            _score = PlayerPrefs.GetInt(_scoreKey,0);

        }

        public void AddScore(int score)
        {
            _score += score;
            PlayerPrefs.SetInt(_scoreKey,_score);
        }
    }
}
