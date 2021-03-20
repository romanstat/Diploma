using System;
using System.Windows.Forms;

namespace Product
{
    public partial class StudentSignIn : Form
    {
        public StudentSignIn()
        {
            InitializeComponent();
            BackgroundImage = Background.Theme;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Заполните поля");
            }
            else 
            {
                Hide();
                Student student = new Student(textBox1.Text, textBox2.Text, textBox3.Text);
                new StudentMenu(student).ShowDialog();
                Close();
            }
        }
    }
}
