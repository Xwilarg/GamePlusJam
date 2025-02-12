﻿using GamesPlusJam.Action.Interaction;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class MineOutput : AInteraction
    {
        public MineManager Manager { private get; set; }

        public override bool IsOneWay => true;

        public override bool IsAvailable => CanInteract && !_didWon;

        [SerializeField]
        private AudioSource _sourceKo, _sourceOk;

        [SerializeField]
        private Material _ok, _notOk, _victory;


        private MeshRenderer _renderer;
        private bool _canInteract;
        public bool CanInteract
        {
            set
            {
                _canInteract = value;
                if (!_didWon)
                {
                    _renderer.materials[3] = value ? _ok : _notOk;
                }
            }
            get => _canInteract;
        }
        private bool _didWon;

        public override void InteractOff(PlayerController pc)
        { }

        public override void InteractOn(PlayerController pc)
        {
            _didWon = true;
            AnswerText.Instance.FindLetters();
            _renderer.materials[3] = _victory;
            Manager.Win();
            _sourceOk.Play();
        }

        public void TouchMine()
        {
            _sourceKo.Play();
        }

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
        }
    }
}
