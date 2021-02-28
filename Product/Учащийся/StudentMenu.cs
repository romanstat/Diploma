using Product.Учащийся;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Product
{
    public partial class StudentMenu : Form
    {
        private readonly List<Test> _tests;
        private readonly Student _student;

        public StudentMenu(Student student)
        {
            InitializeComponent();
            _student = student;
            try
            {
                using (FileStream fs = new FileStream("tests.dat", FileMode.OpenOrCreate))
                {
                    _tests = new BinaryFormatter().Deserialize(fs) as List<Test>;
                    foreach (var test in _tests)
                    {
                        listBox1.Items.Add(test.Theme);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenTest();
        }

        private void OpenTest()
        {
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбран вопрос");
                return;
            }

            try
            {
                Test test = _tests.First(s => s.Theme == listBox1.Items[selectedIndex].ToString());
                if (test.NumberOfQuestionsOfFirstPart == 0 && test.NumberOfQuestionsOfSecondPart == 0 && test.NumberOfQuestionsOfThirdPart == 0)
                {
                    MessageBox.Show("Тест отсутствует");
                }
                else
                {
                    Hide();
                    new PassingFirstPart(test, _student).ShowDialog();
                    Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void пройтиПроверочныйТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTest();
        }

        private void пройтиПроверочныйТестToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenTest();
        }

        private void открытьУчебныйМатериалToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;

            try
            {
                if (selectedIndex != -1)
                {
                    Process.Start($"{_tests.First(t => t.Theme == listBox1.Items[selectedIndex].ToString()).PathToMaterial}");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Файл с учебным материалом отсутствует");
            }
        }
    }
}
