﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class SimonManager : MonoBehaviour
    {
        private List<SimonCube> _cubes = new();
        private List<int> _bips = new();

        private bool _startPlaying;
        private bool _canPlay = true;

        public bool IsDone { private set; get; }

        private int _maxIndex = 5;

        private int _currentObjective;

        private void Start()
        {
            int index = 0;
            foreach (var cube in GetComponentsInChildren<SimonCube>())
            {
                cube.Manager = this;
                cube.Index = index;
                _cubes.Add(cube);
                index++;
            }
            AddBip();
            StartCoroutine(RepeatKey());
        }

        private void AddBip()
        {
            int newBip;
            do
            {
                newBip = Random.Range(0, _cubes.Count);
            } while (_bips.Count > 1 && _bips[^1] == newBip && _bips[^2] == newBip);
            _bips.Add(newBip);
        }

        public void AddAnswer(int index)
        {
            if (!_canPlay)
            {
                return;
            }
            foreach (var c in _cubes)
            {
                c.Toggle(false);
            }
            _startPlaying = true;
            _cubes[index].Toggle(true);
            if (_bips[_currentObjective] == index)
            {
                if (_currentObjective == _maxIndex)
                {
                    _canPlay = false;
                    IsDone = true;
                    foreach (var c in _cubes)
                    {
                        c.Toggle(true);
                    }
                    AnswerText.Instance.FindLetters();
                }
                else if (_currentObjective == _bips.Count - 1)
                {
                    AddBip();
                    _currentObjective = 0;
                    StartCoroutine(DisplayBips());
                }
                else
                {
                    _currentObjective++;
                    _refQueueId++;
                    StartCoroutine(WaitAndHideBips(_refQueueId));
                }
            }
            else
            {
                _bips.Clear();
                AddBip();
                _currentObjective = 0;
                StartCoroutine(ResetGame());
            }
        }

        private int _refQueueId;

        private IEnumerator DisplayBips()
        {
            _canPlay = false;
            yield return new WaitForSeconds(.5f);
            foreach (var c in _cubes)
            {
                c.Toggle(false);
            }
            yield return new WaitForSeconds(1f);
            foreach (var b in _bips)
            {
                _cubes[b].Toggle(true);
                yield return new WaitForSeconds(.5f);
                _cubes[b].Toggle(false);
                yield return new WaitForSeconds(.5f);
            }
            _canPlay = true;
        }

        private IEnumerator WaitAndHideBips(int queueId)
        {
            yield return new WaitForSeconds(.5f);
            if (queueId == _refQueueId)
            {
                foreach (var c in _cubes)
                {
                    c.Toggle(false);
                }
            }
        }

        private IEnumerator ResetGame()
        {
            _canPlay = false;

            yield return new WaitForSeconds(.5f);
            foreach (var c in _cubes)
            {
                c.Toggle(false);
            }
            yield return new WaitForSeconds(1f);
            foreach (var c in _cubes)
            {
                c.Toggle(true);
            }
            yield return new WaitForSeconds(1f);
            foreach (var c in _cubes)
            {
                c.Toggle(false);
            }
            yield return new WaitForSeconds(1f);

            _canPlay = true;
            _startPlaying = false;
            yield return RepeatKey();
        }

        private IEnumerator RepeatKey()
        {
            while (true)
            {
                if (_startPlaying)
                {
                    break;
                }
                _cubes[_bips[0]].Toggle(true, true);
                yield return new WaitForSeconds(1);
                if (_startPlaying)
                {
                    break;
                }
                _cubes[_bips[0]].Toggle(false);
                yield return new WaitForSeconds(1);
            }
            foreach (var c in _cubes)
            {
                c.Toggle(false);
            }
        }
    }
}
