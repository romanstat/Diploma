using System;
using System.Collections.Generic;
using System.Linq;
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
        private string _currentAnswer;
        private readonly Dictionary<string, bool> _result = new Dictionary<string, bool>();
        private readonly DateTime _endOfTest;

        public PassingSecondPart(Test test, List<string> questionsSecondPart, int numberOfPoints, DateTime endOfTest, Dictionary<string, bool> result)
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

        public void Deconstruct(out Dictionary<string, bool> ResultOfSecondPart, out int numberOfPoints)
        {
            ResultOfSecondPart = _result;
            numberOfPoints = _numberOfPoints;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var answer = textBox1.Text;

            if (string.Equals(_currentAnswer, answer, StringComparison.OrdinalIgnoreCase))
            {
                _numberOfPoints += 2;
                _result.Add(_currentQuestion, true);
            }
            else
            {
                _result.Add(_currentQuestion, false);
            }

            textBox1.Clear();
            _numberOfQuestion++;
            Passing();
        }

        private void Passing()
        {
            if (_questionsSecondPart.Count == _numberOfQuestion)
            {
                Close();
            }
            else
            {
                _currentQuestion = _questionsSecondPart[_numberOfQuestion];
                _currentAnswer = _test._questionSecondPart[_currentQuestion];

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
