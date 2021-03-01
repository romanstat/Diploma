using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Product.Учащийся
{
    public partial class PassingSecondPart : Form
    {
        private int _numberOfQuestion = 0;
        private int _numberOfPoints;
        private readonly Test _test;
        private readonly List<string> _questionsSecondPart;
        private string _currentQuestion;
        private string _correctAnswer;
        private List<Tuple<string, bool, string, string>> _result = new List<Tuple<string, bool, string, string>>();
        private readonly DateTime _endOfTest;

        public PassingSecondPart(Test test, List<string> questionsSecondPart, int numberOfPoints, DateTime endOfTest, List<Tuple<string, bool, string, string>> result)
        {
            InitializeComponent();
            _test = test;
            _questionsSecondPart = questionsSecondPart;
            _numberOfPoints = numberOfPoints;
            _endOfTest = endOfTest;
            _result = result;
            timer1.Start();
            Passing();
        }

        public void Deconstruct(out List<Tuple<string, bool, string, string>> ResultOfSecondPart, out int NumberOfPoints)
        {
            ResultOfSecondPart = _result;
            NumberOfPoints = _numberOfPoints;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedAnswer = textBox1.Text;

            if (string.Equals(_correctAnswer, selectedAnswer, StringComparison.OrdinalIgnoreCase))
            {
                _numberOfPoints += 2;
                _result.Add(new Tuple<string, bool, string, string>(_currentQuestion, true, _correctAnswer, selectedAnswer));
            }
            else
            {
                _result.Add(new Tuple<string, bool, string, string>(_currentQuestion, false, _correctAnswer, selectedAnswer));
            }

            textBox1.Clear();
            _numberOfQuestion++;
            Passing();
        }

        private void Passing()
        {
            if (_questionsSecondPart.Count == _numberOfQuestion)
            {
                timer1.Stop();
                Close();
            }
            else
            {
                _currentQuestion = _questionsSecondPart[_numberOfQuestion];
                _correctAnswer = _test._questionSecondPart[_currentQuestion];

                label1.Text = $"Вопрос №{_numberOfQuestion + 1}";
                label2.Text = _currentQuestion;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time = DateTime.Now - _endOfTest;

            if (time.TotalSeconds > 0)
            {
                timer1.Stop();
                Close();
            }

            label5.Text = time.ToString(@"hh\:mm\:ss");
        }
    }
}
