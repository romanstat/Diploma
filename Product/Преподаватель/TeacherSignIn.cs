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

        private async void button1_Click(object sender, EventArgs e)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var teacher = await context.Teachers.FindAsync(1);

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

        private void label4_Click(object sender, EventArgs e)
        {
            Hide();
            new TeacherReset().ShowDialog();
            Show();
        }
    }
}
