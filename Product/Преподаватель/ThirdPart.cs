using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Product.Преподаватель
{
    public partial class ThirdPart : Form
    {
        private readonly Test _test;

        public ThirdPart(ref Test test)
        {
            InitializeComponent();
            _test = test;

            foreach (var question in test._questionThirdPart)
            {
                listBox1.Items.Add(question.Key);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Поля не заполнены");
                return;
            }

            try
            {
                var answers = new List<string[]>();
                var leftMatching = new List<string>();
                var rightMatching = new List<string>();
                var matches = textBox4.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                leftMatching.AddRange(matches.Select(s => s.Split('-')[0]));
                rightMatching.AddRange(matches.Select(s => s.Split('-')[1]));

                if (leftMatching.Count != rightMatching.Count || leftMatching.Any(m => string.IsNullOrWhiteSpace(m)) || rightMatching.Any(m => string.IsNullOrWhiteSpace(m)))
                {
                    throw new Exception();
                }

                for (int i = 0; i < leftMatching.Count; i++)
                {
                    answers.Add(new string[] { leftMatching[i], rightMatching[i] });
                }

                _test.AddThirdPart(textBox1.Text, answers);
                listBox1.Items.Add(textBox1.Text);
                textBox1.Clear();
                textBox4.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Не правильно составлены соответствия");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбран вопрос");
                return;
            }

            textBox1.Text = listBox1.Items[selectedIndex].ToString();
            for (int i = 0; i < _test._questionThirdPart[textBox1.Text].Count; i++)
            {
                textBox4.Text += $"{_test._questionThirdPart[textBox1.Text][i][0]} - {_test._questionThirdPart[textBox1.Text][i][1]}\r\n";
            }

            _test.RemoveThirdPart(textBox1.Text);
            listBox1.Items.RemoveAt(selectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбран вопрос");
                return;
            }

            _test.RemoveThirdPart(listBox1.Items[selectedIndex].ToString());
            listBox1.Items.RemoveAt(selectedIndex);
        }
    }
}
