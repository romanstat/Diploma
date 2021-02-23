using System;
using System.Windows.Forms;

namespace Product
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new TeacherSignIn().ShowDialog();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new StudentSignIn().ShowDialog();
            Show();
        }
    }
}
