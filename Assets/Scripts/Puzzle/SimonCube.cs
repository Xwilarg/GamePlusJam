using GamesPlusJam.Action.Interaction;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class SimonCube : AInteraction
    {
        [SerializeField]
        private Material _normal, _enabled;

        public SimonManager Manager { private get; set; }

        public override bool IsOneWay => true;

        public override bool IsAvailable => true;

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Toggle(bool value)
        {
            _renderer.material = value ? _normal : _enabled;
        }

        public override void InteractOn(PlayerController pc)
        {
            throw new System.NotImplementedException();
        }

        public override void InteractOff(PlayerController pc)
        {
            throw new System.NotImplementedException();
        }
    }
}
