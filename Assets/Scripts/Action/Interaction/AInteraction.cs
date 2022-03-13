using UnityEngine;

namespace GamesPlusJam.Action.Interaction
{
    public abstract class AInteraction : MonoBehaviour
    {
        public abstract void InteractOn(PlayerController pc);
        public abstract void InteractOff(PlayerController pc);
        public abstract bool IsOneWay { get; }
        public abstract bool IsAvailable { get; }
    }
}
