using System;
using System.Windows.Forms;

namespace Product.Преподаватель
{
    public partial class SecondPart : Form
    {
        private readonly Test _test;

        public SecondPart(ref Test test)
        {
            InitializeComponent();
            BackgroundImage = Background.Theme;
            _test = test;

            foreach (var question in test._questionSecondPart)
            {
                listBox1.Items.Add(question.Key);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Поля не заполнены");
                return;
            }

            try
            {
                _test.AddSecondPart(textBox1.Text, textBox2.Text);
                listBox1.Items.Add(textBox1.Text);
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception ex)
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
            textBox2.Text = _test._questionSecondPart[textBox1.Text];
            _test.RemoveSecondPart(textBox1.Text);
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

            _test.RemoveSecondPart(listBox1.Items[selectedIndex].ToString());
            listBox1.Items.RemoveAt(selectedIndex);
        }
    }
}
