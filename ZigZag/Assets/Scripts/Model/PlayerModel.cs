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
        public event Action AIMoveAction;

        public bool IsAI => _isAI;

        [SerializeField] private float _sphereRadius;
        [SerializeField] private float _speed;
        [SerializeField] private Material _myMaterial;
        [SerializeField] private LayerMask _resourseLayer;

        private Vector3 _direction;
        private bool _isAI = false;


        private void Start()
        {
            _direction = Vector3.zero;
        }
        private void FixedUpdate()
        {

            Debug.DrawLine(transform.position, transform.forward * 50, Color.red);
            Debug.DrawLine(transform.position, -transform.right * 50, Color.yellow);

            if (Input.GetKey(KeyCode.S) && !_isAI)
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

            if (_isAI)
            {
                var ray = new Ray(transform.position, transform.forward * 50);


                if (Physics.Raycast(ray,out RaycastHit hit, 50f,_resourseLayer))
                {
                    if(hit.transform.TryGetComponent<CapsuleModel>(out CapsuleModel capsule))
                    {
                        transform.position += _speed * Vector3.forward * Time.deltaTime;
                    }
                }
                else
                    transform.position += _speed * Vector3.left * Time.deltaTime;
            }

            if (!_isAI)
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
            if (other.gameObject.tag == "Lifearea")
            {
                Debug.LogError("Stay");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Lifearea")
            {
                Debug.LogError("Death");
            }
        }

        public void SetAI(bool stateAI)
        {
            _isAI = stateAI;
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

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, 25);
        }
    }
}