using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Manager;
using System;

namespace Assets.Scripts.Controller
{
    public class DeathTrigger : MonoBehaviour
    {
        public event Action DeathAction;

        private float _speed = 5;

        private Transform _target;
        private Vector3 _triggerPos;

        public void GameStarted(Transform target)
        {
            _target = target;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            _triggerPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);

            if (_target.transform.localScale.x > 0f)
            {
                _triggerPos = new Vector3(_triggerPos.x, _triggerPos.y, _triggerPos.z);
            }
            else
            {
                _triggerPos = new Vector3(_triggerPos.x, _triggerPos.y, _triggerPos.z);
            }

            transform.position = Vector3.Lerp(transform.position, _triggerPos, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                DeathAction?.Invoke();
            }
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
