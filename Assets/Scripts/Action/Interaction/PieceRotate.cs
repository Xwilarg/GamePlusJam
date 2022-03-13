using GamesPlusJam.Action.Interaction;
using TMPro;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class PieceRotate : AInteraction
    {
        public enum RotateMode
        {
            Infinite,
            PingPong
        }

        [SerializeField]
        private RotateMode _rotateMode;

        [SerializeField]
        private int _nbTurns;

        [SerializeField]
        private TMP_Text _debugText;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private bool _isPillar;

        private float _oneTurnAngle;
        private float _orRot, _destRot;
        private float _timer = 1f;

        public int Index => _choiceIndex % 4;

        public override bool IsOneWay => true;

        public override bool IsAvailable => true;

        public override void InteractOff(PlayerController pc)
        { }

        public override void InteractOn(PlayerController pc)
        {
            if (_timer < 1f)
            {
                return;
            }
            if (_rotateMode == RotateMode.PingPong)
            {
                (_destRot, _orRot) = (_orRot, _destRot);
            }
            else
            {
                _choiceIndex++;
                _orRot = _destRot;
                _destRot += _oneTurnAngle;
                if (_debugText != null)
                {
                    _debugText.text = AnswerText.Instance.GetAnimal(_choiceIndex % 4);
                }
            }
            _timer = 0f;

            if (_isPillar)
            {
                AnswerText.Instance.CheckVictory();
            }
        }

        private int _choiceIndex;

        private void Start()
        {
            _oneTurnAngle = 360 / _nbTurns;
            _destRot = transform.rotation.eulerAngles.y;
            _orRot = transform.rotation.eulerAngles.y + _oneTurnAngle;
            if (_debugText != null)
            {
                _debugText.text = AnswerText.Instance.GetAnimal(0);
            }
        }

        private void Update()
        {
            if (_timer < 1f)
            {
                _timer += Time.deltaTime * _speed;
                transform.rotation = Quaternion.Euler(
                    x: transform.localRotation.eulerAngles.x,
                    y: Mathf.Lerp(_orRot, _destRot, _timer),
                    z: transform.localRotation.eulerAngles.z
                );
            }
        }
    }
}
