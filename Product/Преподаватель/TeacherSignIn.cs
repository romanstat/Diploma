using Product.Преподаватель;
using System;
using System.Windows.Forms;

namespace Product
{
    public partial class TeacherSignIn : Form
    {
        public TeacherSignIn()
        {
            InitializeComponent();
            BackgroundImage = Background.Theme;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var teacher = context.Teachers.Find(1);

                if (textBox1.Text == teacher.Login && textBox2.Text == teacher.Password)
                {
                    Hide();
                    new TeacherMenu().ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверные данные");
                }
            }
        }
    }
}
