using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class ToggleRenderer : MonoBehaviour
    {
        [SerializeField]
        private Light[] _lights;

        [SerializeField]
        private bool SwitchOn;

        private void OnTriggerEnter(Collider other)
        {
            foreach (var light in _lights)
            {
                light.enabled = SwitchOn;
            }
        }
    }
}
