﻿using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class Toggeable : MonoBehaviour
    {
        [SerializeField]
        private Material _active, _notActive;

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            if (_notActive == null)
            {
                _notActive = _renderer.material;
            }
        }

        public void Toggle(bool status)
        {
            _renderer.material = status ? _active : _notActive;
        }
    }
}
