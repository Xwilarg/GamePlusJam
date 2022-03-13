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

        private string[] _baseText = new[]
        {
            "Wow this is a super thingy",
            "That I'm using to display hints"
        };

        private char _replacementCharacter = '□';

        private TMP_Text _text;

        private List<char> _lettersUsed;
        private List<char> _lettersFound = new();
        private int _diffLetterCount;

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

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _lettersUsed = string.Join("", _baseText).ToLowerInvariant().Where(x => char.IsLetter(x)).Distinct().ToList();
            _diffLetterCount = _lettersUsed.Count;
            FindOneLetter();
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
