using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Product.Преподаватель
{
    public partial class StudentResults : Form
    {
        public StudentResults()
        {
            InitializeComponent();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                dataGridView1.DataSource = context.PassedTests.ToList();
            }
        }
    }
}
