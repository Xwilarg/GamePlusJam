using GamesPlusJam.Action.Interaction;
using System.Linq;
using UnityEngine;

namespace GamesPlusJam.Action
{
    public class Interactible : MonoBehaviour
    {
        [SerializeField]
        private AInteraction[] _interactions;

        public bool IsOneWay => _interactions.All(x => x.IsOneWay);

        public void InteractOn(PlayerController pc)
        {
            foreach (var interaction in _interactions)
            {
                interaction.InteractOn(pc);
            }
        }

        public void InteractOff(PlayerController pc)
        {
            foreach (var interaction in _interactions)
            {
                interaction.InteractOff(pc);
            }
        }
    }
}
