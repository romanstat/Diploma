using Product.Преподаватель;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Product
{
    public partial class CreateTest : Form
    {
        private Test _test;
        private readonly string[] FileExtensions = { ".txt", ".docx", ".pdf" };

        public CreateTest(ref Test test)
        {
            InitializeComponent();
            _test = test;
            textBox1.Text = test.Theme;
            UpdateMaximumOfQuestions();

            if (string.IsNullOrWhiteSpace(_test.PathToMaterial))
            {
                pictureBox1.BackgroundImage = Properties.Resources.icons8_скачать_64__2_;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.icons8_скачать_64;
            }

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

            label10.Text = $"из {_test._questionFirstPart.Count}";
            label11.Text = $"из {_test._questionSecondPart.Count}";
            label12.Text = $"из {_test._questionThirdPart.Count}";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;

                if (FileExtensions.Contains(Path.GetExtension(filename)))
                {
                    _test.PathToMaterial = filename;
                    pictureBox1.BackgroundImage = Properties.Resources.icons8_скачать_64;
                    return;
                }

                MessageBox.Show($"Доступные расширения {string.Join(" ", FileExtensions)}");
            }
        }
    }
}
