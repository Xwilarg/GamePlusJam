using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class Mine : MonoBehaviour
    {
        private Light _light;

        [SerializeField]
        private float _min, _max;

        private bool _didWin = false;

        private void Start()
        {
            _light = GetComponent<Light>();
            transform.localPosition = new Vector3(
                x: Random.Range(_min, _max),
                y: transform.localPosition.y,
                z: transform.localPosition.z
            );
        }

        public void Toggle(bool value)
        {
            if (!_didWin)
            {
                _light.enabled = value;
            }
        }

        public void Win()
        {
            Toggle(true);
            _didWin = true;
            _light.color = Color.green;

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
