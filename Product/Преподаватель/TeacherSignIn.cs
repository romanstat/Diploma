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
            if (textBox1.Text == "college" && textBox2.Text == "MRCCollegeMRC")
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
