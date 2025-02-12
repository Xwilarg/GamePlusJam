﻿using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class MineManager : MonoBehaviour
    {
        [SerializeField]
        private Mine[] _mines;

        [SerializeField]
        private MineOutput _output;

        public static MineManager Instance { get; private set; }

        private bool _lost;
        public bool Lost
        {
            set
            {
                if (!_lost)
                {
                    _output.TouchMine();
                }
                _lost = value;
            }
            private get => _lost;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _output.Manager = this;
        }

        public void Win()
        {
            foreach (var mini in _mines)
            {
                mini.Win();
            }
        }

        public void ToggleAll(bool value)
        {
            foreach (var mine in _mines)
            {
                mine.Toggle(value);
            }
            _output.CanInteract = !value;
            if (!value && Lost)
            {
                Lost = false;
                foreach (var mine in _mines)
                {
                    mine.Randomize();
                }
            }
        }
    }
}
