using System.Collections.Generic;
using System.Windows.Forms;

namespace Product.Преподаватель
{
    public partial class StudentResults : Form
    {
        public StudentResults(List<PassedTest> passedTests)
        {
            InitializeComponent();
            for (int i = 0; i < passedTests?.Count; i++)
            {
                dataGridView1.Rows.Add(passedTests[i].LastName, passedTests[i].FirstName, passedTests[i].Group, passedTests[i].Theme,
                    passedTests[i].Balls, passedTests[i].Assessment, passedTests[i].PassedDate);
            }
        }
    }
}
