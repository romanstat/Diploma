using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Product.Учащийся
{
    public partial class PassingThirdPart : Form
    {
        private readonly Test _test;
        private int _numberOfPoints;
        private int _numberOfQuestion = 0;
        private readonly DateTime _endOfTest;
        private readonly List<string> _questionsThirdPart;
        private string _currentQuestion;
        private List<string[]> _currentAnswers;
        private readonly Dictionary<string, bool> _result = new Dictionary<string, bool>();

        public PassingThirdPart(Test test, List<string> questionsThirdPart, int numberOfPoints, DateTime endOfTest, Dictionary<string, bool> result)
        {
            InitializeComponent();
            _test = test;
            _questionsThirdPart = questionsThirdPart;
            _numberOfPoints = numberOfPoints;
            _endOfTest = endOfTest;
            _result = result;
            timer1.Start();
            Passing();
        }

        public void Deconstruct(out Dictionary<string, bool> ResultOfThirdPart, out int numberOfPoints)
        {
            ResultOfThirdPart = _result;
            numberOfPoints = _numberOfPoints;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var comboBoxes = panel1.Controls.OfType<ComboBox>().ToList();
            for (int i = 0; i < comboBoxes.Count(); i++)
            {
                var answer = comboBoxes[i].SelectedItem?.ToString();
                var correctAnswer = _currentAnswers[i][1];
                if (answer == correctAnswer)
                {
                    _numberOfPoints++;
                }
                else
                {
                    if (!_result.ContainsKey(_currentQuestion))
                    {
                        _result.Add(_currentQuestion, false);
                    }
                }
            }

            if (!_result.ContainsKey(_currentQuestion))
            {
                _result.Add(_currentQuestion, true);
            }

            _numberOfQuestion++;
            Passing();
        }

        private void Passing()
        {
            panel1.Controls.Clear();
            AutoSize = false;
            Size = new Size(1000, 272);
            AutoSize = true;

            if (_questionsThirdPart.Count == _numberOfQuestion)
            {
                Close();
            }
            else
            {
                _currentQuestion = _questionsThirdPart[_numberOfQuestion];
                _currentAnswers = _test._questionThirdPart[_currentQuestion];

                label1.Text = $"Вопрос №{_numberOfQuestion + 1}";
                label2.Text = _currentQuestion;

                var answers = _currentAnswers.Select(c => c[1]).OrderBy(c => Guid.NewGuid()).ToArray();

                Graphics g = CreateGraphics();
                Font font = new Font("Comic Sans MS", 20);
                var comboBoxWidth = (int)g.MeasureString(answers.OrderByDescending(a => a.Length).First(), font).Width;
                var maxSizeOfQuestions = (int)g.MeasureString(_currentAnswers.Select(c => c[0]).OrderByDescending(a => a.Length).First(), font).Width;

                if (comboBoxWidth < 200)
                {
                    comboBoxWidth = 200;
                }

                for (int i = 0; i < _currentAnswers.Count; i++)
                {
                    Label label = new Label
                    {
                        Text = _currentAnswers[i][0],
                        Location = new Point(0, 35 * i),
                        Font = font,
                        AutoSize = true,
                    };

                    ComboBox comboBox = new ComboBox
                    {
                        Visible = true,
                        Location = new Point(maxSizeOfQuestions + 50, 35 * i),
                        Font = font,
                        Width = comboBoxWidth,
                    };

                    comboBox.Items.AddRange(answers);

                    panel1.Controls.Add(label);
                    panel1.Controls.Add(comboBox);
                }

                Height += button1.Height + 15;
                CenterToScreen();
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

            label4.Text = time.ToString(@"hh\:mm\:ss");
        }
    }
}