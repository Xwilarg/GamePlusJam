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
        public bool IsAvailable => _interactions.Any(x => x.IsAvailable);

        public void InteractOn(PlayerController pc)
        {
            foreach (var interaction in _interactions)
            {
                if (interaction.IsAvailable)
                {
                    interaction.InteractOn(pc);
                }
            }
        }

        public void InteractOff(PlayerController pc)
        {
            foreach (var interaction in _interactions)
            {
                if (interaction.IsAvailable)
                {
                    interaction.InteractOff(pc);
                }
            }
        }
    }
}
