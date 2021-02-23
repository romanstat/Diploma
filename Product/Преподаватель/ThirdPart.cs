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
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Поля не заполнены");
                return;
            }

            try
            {
                List<string[]> answers = new List<string[]>();
                List<string> leftMatching = new List<string>();
                List<string> rightMatching = new List<string>();

                leftMatching.AddRange(textBox4.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList());
                rightMatching.AddRange(textBox5.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList());

                if (leftMatching.Count != rightMatching.Count)
                {
                    MessageBox.Show("Не хватает соответствий");
                    return;
                }

                for (int i = 0; i < leftMatching.Count; i++)
                {
                    answers.Add(new string[] { leftMatching[i], rightMatching[i] });
                }

                _test.AddThirdPart(textBox1.Text, answers);
                listBox1.Items.Add(textBox1.Text);
                textBox1.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
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
                textBox4.Text += $"{_test._questionThirdPart[textBox1.Text][i][0]}\r\n";
                textBox5.Text += $"{_test._questionThirdPart[textBox1.Text][i][1]}\r\n";
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

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
