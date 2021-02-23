using Product.Преподаватель;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Product
{
    public partial class TeacherMenu : Form
    {
        private List<Test> _tests;
        private readonly List<PassedTest> _passedTests;

        public TeacherMenu()
        {
            InitializeComponent();
            try
            {
                using (FileStream fs1 = new FileStream("tests.dat", FileMode.OpenOrCreate))
                {
                    _tests = new BinaryFormatter().Deserialize(fs1) as List<Test>;
                    UpdateThemesList();
                }
            }
            catch
            {
                _tests = new List<Test>();
            }

            try
            {
                using (FileStream fs2 = new FileStream("passedTests.dat", FileMode.OpenOrCreate))
                {
                    _passedTests = new BinaryFormatter().Deserialize(fs2) as List<PassedTest>;
                }
            }
            catch
            {
                _passedTests = new List<PassedTest>();
            }
        }

        private void TeacherMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (FileStream fs = new FileStream("tests.dat", FileMode.OpenOrCreate))
            {
                new BinaryFormatter().Serialize(fs, _tests);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new Theme(ref listBox1, ref _tests).ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбрана тема");
                return;
            }

            Hide();
            try
            {
                Test test = _tests.First(s => s.Theme == listBox1.Items[listBox1.SelectedIndex].ToString());
                new CreateTest(ref test).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateThemesList();
            Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int indexOfTheme = listBox1.SelectedIndex;
            if (indexOfTheme != -1)
            {
                File.Delete($"{Environment.CurrentDirectory}\\{listBox1.Items[indexOfTheme]}.docx");
                _tests.Remove(_tests.First(f => f.Theme == listBox1.Items[indexOfTheme].ToString()));
                listBox1.Items.RemoveAt(indexOfTheme);
            }
            else
            {
                MessageBox.Show("Не выбрана тема");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                var results = new StudentResults(_passedTests);
                results.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Тесты не проходили");
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбрана тема");
                return;
            }

            Hide();
            try
            {
                Test test = _tests.First(s => s.Theme == listBox1.Items[listBox1.SelectedIndex].ToString());
                new CreateTest(ref test).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateThemesList();
            Show();
        }

        private void UpdateThemesList()
        {
            listBox1.Items.Clear();
            foreach (var test in _tests)
            {
                listBox1.Items.Add(test.Theme);
            }
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hide();
            new Theme(ref listBox1, ref _tests).ShowDialog();
            Show();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбрана тема");
                return;
            }

            Hide();
            try
            {
                Test test = _tests.First(s => s.Theme == listBox1.Items[listBox1.SelectedIndex].ToString());
                new CreateTest(ref test).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateThemesList();
            Show();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int indexOfTheme = listBox1.SelectedIndex;
            if (indexOfTheme != -1)
            {
                File.Delete($"{Environment.CurrentDirectory}\\{listBox1.Items[indexOfTheme]}.docx");
                _tests.Remove(_tests.First(f => f.Theme == listBox1.Items[indexOfTheme].ToString()));
                listBox1.Items.RemoveAt(indexOfTheme);
            }
            else
            {
                MessageBox.Show("Не выбрана тема");
            }
        }

        private void результатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var results = new StudentResults(_passedTests);
                results.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Тесты не проходили");
            }
        }
    }
}
