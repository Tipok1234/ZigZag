using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class CubeModel : MonoBehaviour
    {
        public Transform Begin => _begin;
        public Transform End => _end;

        [SerializeField] private Transform _begin;
        [SerializeField] private Transform _end;
    }
}
