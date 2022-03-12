using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class UpDownManager : MonoBehaviour
    {
        [SerializeField]
        public UpDown[] _upDowns;

        private void Start()
        {
            for (int i = 0; i < _upDowns.Length; i++)
            {
                _upDowns[i].Index = i;
                _upDowns[i].Manager = this;
            }
        }

        public void Activate(int index)
        {
            _upDowns[index].Activate();
            if (index > 0)
            {
                _upDowns[index - 1].Activate();
            }
            if (index < _upDowns.Length - 1)
            {
                _upDowns[index + 1].Activate();
            }
        }
    }
}
