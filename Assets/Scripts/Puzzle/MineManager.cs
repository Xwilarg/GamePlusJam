using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class MineManager : MonoBehaviour
    {
        [SerializeField]
        private Mine[] _mines;

        public static MineManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ToggleAll(bool value)
        {
            foreach (var mine in _mines)
            {
                mine.Toggle(value);
            }
        }
    }
}
