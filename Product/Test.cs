using System;
using System.Collections.Generic;

namespace Product
{
    [Serializable]
    public class Test
    {
        private string _theme;

        public Dictionary<string, List<string>> _questionFirstPart = new Dictionary<string, List<string>>();

        public Dictionary<string, string> _questionSecondPart = new Dictionary<string, string>();

        public Dictionary<string, List<string[]>> _questionThirdPart = new Dictionary<string, List<string[]>>();

        public int PassingTime { get; set; } = 45;

        public int NumberOfQuestionsOfFirstPart { get; set; } = 0;

        public int NumberOfQuestionsOfSecondPart { get; set; } = 0;

        public int NumberOfQuestionsOfThirdPart { get; set; } = 0;

        public string PathToMaterial { get; set; }

        public string Theme
        {
            get => _theme;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(value)} is null");
                }
                else
                {
                    _theme = value;
                }
            }
        }

        public Test(string theme)
        {
            Theme = theme;
        }

        public void AddFirstPart(string question, List<string> answers)
        {
            if (!_questionFirstPart.ContainsKey(question))
            {
                _questionFirstPart.Add(question, answers);
            }
            else
            {
                throw new ArgumentException("Вопрос уже существует");
            }
        }

        public void AddRangeFirstPart(List<string> questions, List<List<string>> listAnswers)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                if (_questionFirstPart.ContainsKey(questions[i]))
                {
                    continue;
                }

                _questionFirstPart.Add(questions[i], listAnswers[i]);
            }
        }

        public void AddSecondPart(string question, string answer)
        {
            if (!_questionSecondPart.ContainsKey(question))
            {
                _questionSecondPart.Add(question, answer);
            }
            else
            {
                throw new ArgumentException("Вопрос уже существует");
            }
        }

        public void AddRangeSecondPart(List<string> questions, List<string> listAnswers)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                if (_questionSecondPart.ContainsKey(questions[i]))
                {
                    continue;
                }

                _questionSecondPart.Add(questions[i], listAnswers[i]);
            }
        }

        public void AddThirdPart(string question, List<string[]> answers)
        {
            if (!_questionThirdPart.ContainsKey(question))
            {
                _questionThirdPart.Add(question, answers);
            }
            else
            {
                throw new ArgumentException("Вопрос уже существует");
            }
        }

        public void AddRangeThirdPart(List<string> questions, List<List<string[]>> listAnswers)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                if (_questionThirdPart.ContainsKey(questions[i]))
                {
                    continue;
                }

                _questionThirdPart.Add(questions[i], listAnswers[i]);
            }
        }

        public void RemoveFirstPart(string question)
        {
            _questionFirstPart.Remove(question);
        }

        public void RemoveSecondPart(string question)
        {
            _questionSecondPart.Remove(question);
        }

        public void RemoveThirdPart(string question)
        {
            _questionThirdPart.Remove(question);
        }
    }
}
