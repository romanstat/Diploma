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
                var result = context.PassedTests.OrderBy(p => p.PassedDate).ToList();
                for (int i = 0; i < result.Count; i++)
                {
                    dataGridView2.Rows.Add(result[i].FirstName, result[i].LastName, result[i].Group, result[i].Theme, result[i].Balls, result[i].Assessment, result[i].PassedDate);
                }
            }
        }
    }
}
