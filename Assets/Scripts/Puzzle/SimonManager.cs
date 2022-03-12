using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class SimonManager : MonoBehaviour
    {
        private List<SimonCube> _cubes = new();
        private List<int> _bips = new();

        private bool _startPlaying;

        private void Start()
        {
            foreach (var cube in GetComponentsInChildren<SimonCube>())
            {
                cube.Manager = this;
                _cubes.Add(cube);
            }
            AddBip();
            StartCoroutine(RepeatKey());
        }

        private void AddBip()
        {
            _bips.Add(Random.Range(0, _cubes.Count));
        }

        private IEnumerator RepeatKey()
        {
            while (true)
            {
                if (_startPlaying)
                {
                    break;
                }
                _cubes[_bips[0]].Toggle(true);
                yield return new WaitForSeconds(1);
                if (_startPlaying)
                {
                    break;
                }
                _cubes[_bips[0]].Toggle(false);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
