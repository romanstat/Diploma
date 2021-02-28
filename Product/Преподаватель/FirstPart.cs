using System;
using System.Linq;
using System.Windows.Forms;

namespace Product.Преподаватель
{
    public partial class FirstPart : Form
    {
        private readonly Test _test;

        public FirstPart(ref Test test)
        {
            InitializeComponent();
            _test = test;

            foreach (var question in test._questionFirstPart)
            {
                listBox1.Items.Add(question.Key);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            var splitedText = textBox2.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (!splitedText.Any(t => "*".Contains(t.First())))
            {
                MessageBox.Show("Нет правильного ответа");
                return;
            }

            try
            {
                _test.AddFirstPart(textBox1.Text, splitedText);
                listBox1.Items.Add(textBox1.Text);
                textBox1.Clear();
                textBox2.Clear();
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
            textBox2.Text = string.Join("\r\n", _test._questionFirstPart[textBox1.Text]);
            _test.RemoveFirstPart(textBox1.Text);
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

            _test.RemoveFirstPart(listBox1.Items[selectedIndex].ToString());
            listBox1.Items.RemoveAt(selectedIndex);
        }
    }
}
