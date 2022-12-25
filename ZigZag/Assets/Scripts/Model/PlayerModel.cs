using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enum;
using System;

namespace Assets.Scripts.Model
{
    public class PlayerModel : MonoBehaviour
    {
        public event Action CollectedScoreAction;

        [SerializeField] private float _sphereRadius;
        [SerializeField] private float _speed;
        [SerializeField] private Material _myMaterial;
        [SerializeField] private LayerMask _resourseLayer;

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


            if (Physics.CheckSphere(transform.position, _sphereRadius, _resourseLayer))
            {
                var hit = Physics.OverlapSphere(transform.position, _sphereRadius, _resourseLayer);
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.TryGetComponent<CapsuleModel>(out CapsuleModel capsuleModel))
                    {
                        capsuleModel.gameObject.SetActive(false);
                        CollectedScoreAction?.Invoke();
                    }
                }
            }
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == "Lifearea")
            {
                Debug.LogError("Stay");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Lifearea")
            {
                Debug.LogError("Death");
            }
        }

        public void SetColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Green:
                    _myMaterial.color = Color.green;
                    break;
                case ColorType.Blue:
                    _myMaterial.color = Color.blue;
                    break;
                case ColorType.Red:
                    _myMaterial.color = Color.red;
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _sphereRadius);
        }
    }
}