using UnityEngine;

namespace GamesPlusJam.Action.Interaction
{
    public class ShowImage : AInteraction
    {
        [SerializeField]
        private GameObject _image;

        public override bool IsOneWay => false;

        public override void InteractOn(PlayerController pc)
        {
            _image.SetActive(true);
        }

        public override void InteractOff(PlayerController pc)
        {
            _image.SetActive(false);
        }
    }
}
