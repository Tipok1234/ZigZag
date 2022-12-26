using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance => _instance;

        [SerializeField] private AudioSource _clickButtonSound;
        [SerializeField] private AudioSource _mainSound;

        private static AudioManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

            _mainSound.Play();
        }

        public void PlaySound()
        {
            _clickButtonSound.Play();
        }
    }
}
