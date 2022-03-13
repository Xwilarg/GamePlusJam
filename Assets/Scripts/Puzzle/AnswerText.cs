using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class AnswerText : MonoBehaviour
    {
        public static AnswerText Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        private string[] _possibilities = new[]
        {
            "beetle",
            "beluga",
            "falcon",
            "beaver"
        };

        private List<string> _baseText = new()
        {
            "To access the chamber you must move the pillars"
        };

        private char _replacementCharacter = '□';

        private TMP_Text _text;

        private List<char> _lettersUsed;
        private List<char> _lettersFound = new();
        private int _diffLetterCount;

        private int _pillarCount = 4;

        private string[] _finalAnswer;

        private string[] _intToText = new[]
        {
            "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "ninth", "tenth"
        };

        private void Start()
        {
            _finalAnswer = new string[_pillarCount];
            List<int> pillars = Enumerable.Range(0, _pillarCount).ToList();
            for (int i = 0; i < 4; i++)
            {
                var randPillar = Random.Range(0, pillars.Count);
                var pillarValue = pillars[randPillar];
                pillars.RemoveAt(randPillar);

                var animal = _possibilities[Random.Range(0, _possibilities.Length)];
                _finalAnswer[pillarValue] = animal;
                _baseText.Add($"The {_intToText[pillarValue]} pillar must be the {animal}");
            }

            _text = GetComponent<TMP_Text>();
            _lettersUsed = string.Join("", _baseText).ToLowerInvariant().Where(x => char.IsLetter(x)).Distinct().ToList();
            _diffLetterCount = _lettersUsed.Count;
            FindOneLetter();
            DisplayText();
        }

        private void FindOneLetter()
        {
            var randIndex = Random.Range(0, _lettersUsed.Count);
            _lettersFound.Add(_lettersUsed[randIndex]);
            _lettersUsed.RemoveAt(randIndex);
        }

        public void FindLetters()
        {
            for (int i = 0; i < _diffLetterCount / 4; i++)
            {
                FindOneLetter();
            }
            DisplayText();
        }

        private void DisplayText()
        {
            _text.text = string.Join("", string.Join("\n", _baseText).Select(x =>
            {
                if (char.IsLetter(x))
                {
                    if (_lettersFound.Contains(char.ToLowerInvariant(x)))
                    {
                        return x;
                    }
                    return _replacementCharacter;
                }
                return x;
            }));
        }
    }
}
