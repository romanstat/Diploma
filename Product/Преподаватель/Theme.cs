using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Product
{
    public partial class Theme : Form
    {
        private readonly ListBox _themes;
        private readonly List<Test> _tests;
        private string pathToMaterial;

        public Theme(ref ListBox themes, ref List<Test> tests)
        {
            InitializeComponent();
            _themes = themes;
            _tests = tests;
            checkedListBox1.Items.AddRange(themes.Items);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Название темы отсутствует");
            }
            else if (_themes.Items.Contains(textBox1.Text))
            {
                MessageBox.Show("Тема присутствует");
            }
            else
            {
                _themes.Items.Add(textBox1.Text);

                var addTests = new List<Test>();

                var selectedThemes = checkedListBox1.CheckedItems;

                for (int i = 0; i < selectedThemes.Count; i++)
                {
                    addTests.Add(_tests.First(t => t.Theme == selectedThemes[i].ToString()));
                }

                var test = new Test(textBox1.Text);
                for (int i = 0; i < addTests.Count; i++)
                {
                    test.AddRangeFirstPart(addTests[i]._questionFirstPart.Keys.ToList(), addTests[i]._questionFirstPart.Values.ToList());
                    test.AddRangeSecondPart(addTests[i]._questionSecondPart.Keys.ToList(), addTests[i]._questionSecondPart.Values.ToList());
                    test.AddRangeThirdPart(addTests[i]._questionThirdPart.Keys.ToList(), addTests[i]._questionThirdPart.Values.ToList());
                }

                test.PathToMaterial = pathToMaterial;
                test.NumberOfQuestionsOfFirstPart = test._questionFirstPart.Count;
                test.NumberOfQuestionsOfSecondPart = test._questionSecondPart.Count;
                test.NumberOfQuestionsOfThirdPart = test._questionThirdPart.Count;
                _tests.Add(test);
                Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathToMaterial = openFileDialog1.FileName;
            }

            pictureBox1.BackgroundImage = Properties.Resources.icons8_скачать_64;
        }
    }
}
