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
            var selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Не выбран вопрос");
                return;
            }

            try
            {
                Hide();
                Test test = _tests.First(s => s.Theme == listBox1.Items[selectedIndex].ToString());
                new PassingFirstPart(test, _student).ShowDialog();
                Show();
            }
            catch (ObjectDisposedException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                Process.Start($"{_tests.First(t => t.Theme == listBox1.Items[selectedIndex].ToString()).PathToMaterial}");
            }
        }
    }
}
