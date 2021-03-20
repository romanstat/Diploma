using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Product
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            try
            {
                using (FileStream fs = new FileStream("theme.dat", FileMode.OpenOrCreate))
                {
                    var theme = new BinaryFormatter().Deserialize(fs) as Bitmap;
                    Background.Theme = theme;
                }
            }
            catch
            {

            }

            BackgroundImage = Background.Theme;
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            new ChooseTheme().ShowDialog();
            BackgroundImage = Background.Theme;
            Show();
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (FileStream fs = new FileStream("theme.dat", FileMode.OpenOrCreate))
            {
                new BinaryFormatter().Serialize(fs, Background.Theme);
            }
        }
    }
}
