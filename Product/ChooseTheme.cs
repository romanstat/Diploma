using System.Windows.Forms;

namespace Product
{
    public partial class ChooseTheme : Form
    {
        public ChooseTheme()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, System.EventArgs e)
        {
            Background.Theme = Properties.Resources._1593666815_56_p_neitralnii_fon_66;
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            Background.Theme = Properties.Resources._2;
        }

        private void pictureBox3_Click(object sender, System.EventArgs e)
        {
            Background.Theme = Properties.Resources._3;
        }

        private void pictureBox4_Click(object sender, System.EventArgs e)
        {
            Background.Theme = Properties.Resources._4;
        }
    }
}
