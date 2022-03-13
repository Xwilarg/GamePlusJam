using GamesPlusJam.Action.Interaction;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class Mirror : AInteraction
    {
        [SerializeField]
        private GameObject _child;

        private AudioSource _source;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            if (Random.Range(0, 2) == 0)
            {
                Toggle(true);
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

        public void Toggle(bool bypassAudio = false)
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
            if (!bypassAudio)
            {
                _source.pitch = Random.Range(.75f, 1.25f);
                _source.Play();
            }
        }

        public bool Angle;
    }
}
