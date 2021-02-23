using Product.Преподаватель;
using System;
using System.Windows.Forms;

namespace Product
{
    public partial class CreateTest : Form
    {
        private Test _test;

        public CreateTest(ref Test test)
        {
            InitializeComponent();
            _test = test;
            textBox1.Text = test.Theme;
            UpdateMaximumOfQuestions();

            numericUpDown1.Value = _test.PassingTime;
            numericUpDown2.Value = _test.NumberOfQuestionsOfFirstPart;
            numericUpDown3.Value = _test.NumberOfQuestionsOfSecondPart;
            numericUpDown4.Value = _test.NumberOfQuestionsOfThirdPart;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new FirstPart(ref _test).ShowDialog();
            UpdateMaximumOfQuestions();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new SecondPart(ref _test).ShowDialog();
            UpdateMaximumOfQuestions();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            new ThirdPart(ref _test).ShowDialog();
            UpdateMaximumOfQuestions();
            Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _test.Theme = textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _test.PassingTime = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            _test.NumberOfQuestionsOfFirstPart = (int)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            _test.NumberOfQuestionsOfSecondPart = (int)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            _test.NumberOfQuestionsOfThirdPart = (int)numericUpDown4.Value;
        }

        private void UpdateMaximumOfQuestions()
        {
            numericUpDown2.Maximum = _test._questionFirstPart.Count;
            numericUpDown3.Maximum = _test._questionSecondPart.Count;
            numericUpDown4.Maximum = _test._questionThirdPart.Count;
        }
    }
}
