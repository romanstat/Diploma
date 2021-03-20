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
            if (textBox1.Text == "1" && textBox2.Text == "1")
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
