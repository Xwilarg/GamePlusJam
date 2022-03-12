using GamesPlusJam.Action.Interaction;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class PuzzleInput : AInteraction
    {
        [SerializeField]
        private int _nbTurns;

        [SerializeField]
        private float _speed;

        private float _oneTurnAngle;
        private float _destRot;
        private bool _isRotDone = true;

        public override bool IsOneWay => true;

        public override void InteractOff(PlayerController pc)
        { }

        public override void InteractOn(PlayerController pc)
        {
            if (!_isRotDone)
            {
                return;
            }
            _destRot += _oneTurnAngle;
            if (_destRot > 360)
            {
                _destRot = 360 - _destRot;
            }
            _isRotDone = false;
        }

        private void Start()
        {
            _oneTurnAngle = 360 / _nbTurns;
            _destRot = transform.localRotation.eulerAngles.y;
        }

        private void Update()
        {
            if (!_isRotDone)
            {
                var old = transform.localRotation.eulerAngles.y;
                transform.Rotate(Vector3.up, Time.deltaTime * _speed);
                if (transform.localRotation.eulerAngles.y > _destRot || old > transform.localRotation.eulerAngles.y)
                {
                    _isRotDone = true;
                }
            }
        }
    }
}
