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

        [SerializeField]
        private Transform _doorTransform;

        [SerializeField]
        private GameObject _finalLight;

        private string[] _possibilities = new[]
        {
            "beetle",
            "beaver",
            "beluga",
            "badger"
        };

        public string GetAnimal(int index) => _possibilities[index];

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

        [SerializeField]
        private PieceRotate[] _pillars;

        public void CheckVictory()
        {
            for (int i = 0; i < _pillars.Length; i++)
            {
                if (GetAnimal(_pillars[i].Index) != _finalAnswer[i])
                {
                    return;
                }
            }
            _isWon = true;
            _finalLight.SetActive(true);
        }

        private void Update()
        {
            if (_isWon && _timer < 1f)
            {
                _timer += Time.deltaTime;
                _timer = Mathf.Clamp01(_timer);
                _doorTransform.position = new Vector3(
                    x: _doorTransform.position.x,
                    y: Mathf.Lerp(_orY, _destY, _timer),
                    z: _doorTransform.position.z
                );
            }
        }

        private float _timer;
        private float _orY;
        private float _destY = -2.05f;
        private bool _isWon = false;

        private void Start()
        {
            _orY = transform.position.y;
            _finalAnswer = new string[_pillarCount];
            List<int> pillars = Enumerable.Range(0, _pillarCount).ToList();
            for (int i = 0; i < 4; i++)
            {
                var randPillar = Random.Range(0, pillars.Count);
                var pillarValue = pillars[randPillar];
                pillars.RemoveAt(randPillar);

                _finalAnswer[pillarValue] = _possibilities[Random.Range(0, _possibilities.Length)];
            }
            for (int i = 0; i < 4; i++)
            {
                _baseText.Add($"The {_intToText[i]} pillar must be the {_finalAnswer[i]}");
            }

            _text = GetComponent<TMP_Text>();
            _lettersUsed = string.Join("", _baseText).ToLowerInvariant().Where(x => char.IsLetter(x)).Distinct().ToList();
            _diffLetterCount = _lettersUsed.Count;
            FindLetters();
            DisplayText();
        }

        private void FindOneLetter()
        {
            if (_lettersUsed.Any())
            {
                var randIndex = Random.Range(0, _lettersUsed.Count);
                _lettersFound.Add(_lettersUsed[randIndex]);
                _lettersUsed.RemoveAt(randIndex);
            }
        }

        public void FindLetters()
        {
            for (int i = 0; i < _diffLetterCount / 5; i++)
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
                    if (char.IsUpper(x))
                    {
                        return (char)Random.Range('A', 'Z' + 1);
                    }
                    return (char)Random.Range('a', 'z' + 1);
                }
                return x;
            }));
        }
    }
}
