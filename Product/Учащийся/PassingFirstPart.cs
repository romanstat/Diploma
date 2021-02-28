using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product.Учащийся
{
    public partial class PassingFirstPart : Form
    {
        private readonly Test _test;
        private readonly Student _student;
        private readonly List<string> _questionsFirstPart = new List<string>();
        private readonly List<string> _questionsSecondPart = new List<string>();
        private readonly List<string> _questionsThirdPart = new List<string>();
        private List<Tuple<string, bool, string, string>> _result = new List<Tuple<string, bool, string, string>>();
        private string _currentQuestion;
        private List<string> _currentAnswers;
        private int _numberOfQuestion = 0;
        private int _numberOfPoints = 0;
        private readonly double _allBalls = 0;
        private readonly DateTime _endOfTest;
        private readonly DateTime _startOfTest;

        public PassingFirstPart(Test test, Student student)
        {
            InitializeComponent();
            _test = test;
            _student = student;
            _questionsFirstPart = _test._questionFirstPart.Keys.OrderBy(g => Guid.NewGuid()).Take(_test.NumberOfQuestionsOfFirstPart).ToList();
            _questionsSecondPart = _test._questionSecondPart.Keys.OrderBy(g => Guid.NewGuid()).Take(_test.NumberOfQuestionsOfSecondPart).ToList();
            _questionsThirdPart = _test._questionThirdPart.Keys.OrderBy(g => Guid.NewGuid()).Take(_test.NumberOfQuestionsOfThirdPart).ToList();
            _allBalls = _questionsFirstPart.Count + _questionsSecondPart.Count * 2 + _test._questionThirdPart.Where(q => _questionsThirdPart.Contains(q.Key)).Sum(q => q.Value.Count);
            _startOfTest = DateTime.Now;
            _endOfTest = DateTime.Now.AddMinutes(test.PassingTime);
            timer1.Start();
            Passing();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбран ответ");
                return;
            }

            var selectedAnswer = listBox1.Items[listBox1.SelectedIndex].ToString();
            var corretAnswer = _currentAnswers.Find(f => f.Contains("*")).Remove(0, 1);

            if (corretAnswer == selectedAnswer)
            {
                _numberOfPoints += 1;
                _result.Add(new Tuple<string, bool, string, string>(_currentQuestion, true, corretAnswer, selectedAnswer));
            }
            else
            {
                _result.Add(new Tuple<string, bool, string, string>(_currentQuestion, false, corretAnswer, selectedAnswer));
            }

            listBox1.Items.Clear();
            _numberOfQuestion++;
            Passing();
        }

        private void Passing()
        {
            if (_questionsFirstPart.Count == _numberOfQuestion)
            {
                Hide();

                if (_test._questionSecondPart.Count != 0)
                {
                    PassingSecondPart secondPart = new PassingSecondPart(_test, _questionsSecondPart, _numberOfPoints, _endOfTest, _result);
                    secondPart.ShowDialog();
                    secondPart.Deconstruct(out _result, out _numberOfPoints);
                }

                if (_test._questionThirdPart.Count != 0)
                {
                    PassingThirdPart thirdPart = new PassingThirdPart(_test, _questionsThirdPart, _numberOfPoints, _endOfTest, _result);
                    thirdPart.ShowDialog();
                    thirdPart.Deconstruct(out _result, out _numberOfPoints);
                }

                ShowResult();

                Close();
            }
            else
            {
                _currentQuestion = _questionsFirstPart[_numberOfQuestion];
                _currentAnswers = _test._questionFirstPart[_currentQuestion];

                label1.Text = $"Вопрос №{_numberOfQuestion + 1}";
                label2.Text = _currentQuestion;
                listBox1.Items.AddRange(_currentAnswers.Select(s => s.Contains("*") ? s.Remove(0, 1) : s)
                    .OrderBy(g => Guid.NewGuid().ToString())
                    .ToArray());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time = DateTime.Now - _endOfTest;

            if (time.TotalSeconds > 0)
            {
                Hide();
                ShowResult();
                Close();
            }

            label4.Text = time.ToString(@"hh\:mm\:ss"); ;
        }

        private void PassingFirstPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void ShowResult()
        {
            timer1.Stop();
            var studentAssessment = Math.Round(_numberOfPoints / _allBalls * 10.0, 2);
            var finishedDate = DateTime.Now;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var passedTest = new PassedTest()
                {
                    FirstName = _student.FirstName,
                    LastName = _student.LastName,
                    Group = _student.Group,
                    Theme = _test.Theme,
                    Assessment = $"Оценка: {studentAssessment}/10",
                    Balls = $"Баллы: {_numberOfPoints}/{_allBalls}",
                    PassedDate = finishedDate,
                };

                context.PassedTests.Add(passedTest);
                context.SaveChanges();
            }

            var result = new Result(_allBalls, _numberOfPoints, studentAssessment, _startOfTest, finishedDate, _result);
            result.ShowDialog();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбран ответ");
                return;
            }

            var selectedAnswer = listBox1.Items[listBox1.SelectedIndex].ToString();
            var corretAnswer = _currentAnswers.Find(f => f.Contains("*")).Remove(0, 1);

            if (corretAnswer == selectedAnswer)
            {
                _numberOfPoints += 1;
                _result.Add(new Tuple<string, bool, string, string>(_currentQuestion, true, corretAnswer, selectedAnswer));
            }
            else
            {
                _result.Add(new Tuple<string, bool, string, string>(_currentQuestion, false, corretAnswer, selectedAnswer));
            }

            listBox1.Items.Clear();
            _numberOfQuestion++;
            Passing();
        }
    }
}
