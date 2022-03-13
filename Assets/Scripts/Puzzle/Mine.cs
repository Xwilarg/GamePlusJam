using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class Mine : MonoBehaviour
    {
        private Light _light;

        private void Start()
        {
            _light = GetComponent<Light>();
        }

        public void Toggle(bool value)
        {
            _light.enabled = value;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                MineManager.Instance.ToggleAll(true);
            }
        }
    }
}
