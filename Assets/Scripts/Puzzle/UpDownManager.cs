using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class UpDownManager : MonoBehaviour
    {
        [SerializeField]
        private UpDown[] _upDowns;

        [SerializeField]
        private Toggeable _endPipe;

        private void Start()
        {
            for (int i = 0; i < _upDowns.Length; i++)
            {
                _upDowns[i].Index = i;
                _upDowns[i].Manager = this;
            }

            List<int> indexes = Enumerable.Range(0, _upDowns.Length).ToList();
            for (int i = 0; i < 3; i++)
            {
                var randIndex = Random.Range(0, indexes.Count);
                var value = indexes[randIndex];
                _upDowns[value].Activate();
                indexes.RemoveAt(randIndex);
                var before = indexes.IndexOf(value - 1);
                if (before != -1)
                {
                    indexes.RemoveAt(before);
                }
                var after = indexes.IndexOf(value + 1);
                if (after != -1)
                {
                    indexes.RemoveAt(after);
                }
            }
            UpdateDisplay();
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
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            bool active = true;
            foreach (var ud in _upDowns)
            {
                ud.TogglePipeStatus(active);
                if (ud.GoUp)
                {
                    active = false;
                }
            }
            _endPipe.Toggle(active);
            if (active)
            {
                foreach (var ud in _upDowns)
                {
                    ud.Available = false;
                }
            }
        }
    }
}
