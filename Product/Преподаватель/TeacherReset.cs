using System;
using System.Windows.Forms;

namespace Product.Преподаватель
{
    public partial class TeacherReset : Form
    {
        public TeacherReset()
        {
            InitializeComponent();
            BackgroundImage = Background.Theme;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) || !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                using (var context = new ApplicationDbContext())
                {
                    var teacher = context.Teachers.Find(1);
                    teacher.Login = textBox1.Text;
                    teacher.Password = textBox2.Text;
                    context.SaveChanges();
                }

                Close();
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
    }
}
