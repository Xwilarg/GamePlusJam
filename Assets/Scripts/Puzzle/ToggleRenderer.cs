using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class ToggleRenderer : MonoBehaviour
    {
        [SerializeField]
        private bool SwitchOn;

        private void OnTriggerEnter(Collider other)
        {
            MineManager.Instance.ToggleAll(SwitchOn);
        }
    }
}
