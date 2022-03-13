using GamesPlusJam.Action.Interaction;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class Mirror : AInteraction
    {
        [SerializeField]
        private GameObject _child;

        private void Start()
        {
            if (Random.Range(0, 2) == 0)
            {
                Toggle();
            }
        }

        private bool _status;

        public override bool IsOneWay => true;

        public override bool IsAvailable => !LaserInput.Instance.DidWon;

        public override void InteractOff(PlayerController pc)
        { }

        public override void InteractOn(PlayerController pc)
        {
            Toggle();
        }

        public void Toggle()
        {
            _status = !_status;
            _child.SetActive(!_status);
            if (_status)
            {
                gameObject.layer = 7;
            }
            else
            {
                gameObject.layer = 0;
            }
        }

        public bool Angle;
    }
}
