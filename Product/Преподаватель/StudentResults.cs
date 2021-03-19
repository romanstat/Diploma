using Microsoft.EntityFrameworkCore;
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

        private async void очиститьДанныеToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE PassedTests");
                await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT('PassedTests', RESEED, 0)");
            }

            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
        }
    }
}
