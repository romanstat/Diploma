using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Product.Учащийся
{
    public partial class PassingFirstPart : Form
    {
        private readonly Test _test;
        private readonly Student _student;
        private int _numberOfQuestion = 0;
        private int _numberOfPoints = 0;
        private readonly double _allBalls = 0;
        private readonly List<string> _questionsFirstPart = new List<string>();
        private readonly List<string> _questionsSecondPart = new List<string>();
        private readonly List<string> _questionsThirdPart = new List<string>();
        private string _currentQuestion;
        private List<string> _currentAnswers;
        //bool, hisanswer, correct answer 
        private Dictionary<string, bool> _result = new Dictionary<string, bool>();
        private readonly DateTime _endOfTest;
        private readonly DateTime _startOfTest;

        public PassingFirstPart(Test test, Student student)
        {
            InitializeComponent();
            _test = test;

            if (_test.NumberOfQuestionsOfFirstPart == 0 && _test.NumberOfQuestionsOfSecondPart == 0 && _test.NumberOfQuestionsOfThirdPart == 0)
            {
                MessageBox.Show("Тесты отсутствуют");
                Close();
                return;
            }

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
                _result.Add(_currentQuestion, true);
            }
            else
            {
                _result.Add(_currentQuestion, false);
            }

            listBox1.Items.Clear();
            _numberOfQuestion++;
            Passing();
        }

        private async void Passing()
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

                timer1.Stop();

                var studentAssessment = Math.Round(_numberOfPoints / _allBalls * 10.0, 2);

                var result = new Result(_allBalls, _numberOfPoints, studentAssessment, _startOfTest, _result);
                result.ShowDialog();

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
                        PassedDate = DateTime.Now,
                    };

                    await context.PassedTests.AddAsync(passedTest);
                    await context.SaveChangesAsync();
                }

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
                timer1.Stop();
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
            var studentAssessment = Math.Round(_numberOfPoints / _allBalls * 10.0, 2);
            MessageBox.Show($"Количество баллов: {_numberOfPoints}/{_allBalls}\nОценка: {studentAssessment}");
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
                _result.Add(_currentQuestion, true);
            }
            else
            {
                _result.Add(_currentQuestion, false);
            }

            listBox1.Items.Clear();
            _numberOfQuestion++;
            Passing();
        }
    }
}
