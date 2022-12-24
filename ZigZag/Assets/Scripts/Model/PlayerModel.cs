using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Vector3 _direction;


        private void Start()
        {
            _direction = Vector3.zero;
        }
        private void FixedUpdate()
        {       
            if (Input.GetKey(KeyCode.S))
            {
                if (_direction == Vector3.forward)
                {
                    _direction = Vector3.left;
                }
                else
                {
                    _direction = Vector3.forward;
                }
            }


            transform.position += _speed * _direction * Time.deltaTime;
        }
    }
}
