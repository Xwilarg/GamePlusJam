using GamesPlusJam.Action.Interaction;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class MineOutput : AInteraction
    {
        public override bool IsOneWay => true;

        public override bool IsAvailable => CanInteract && !_didWon;

        [SerializeField]
        private Material _ok, _notOk;


        private MeshRenderer _renderer;
        private bool _canInteract;
        public bool CanInteract
        {
            set
            {
                _canInteract = value;
                if (!_didWon)
                {
                    _renderer.material = value ? _ok : _notOk;
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
            _renderer.material = _ok;
        }

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
        }
    }
}
