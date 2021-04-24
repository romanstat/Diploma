using System.Windows.Forms;

namespace Product
{
    public class DoubleBufferPanel : Panel
    {
        public DoubleBufferPanel() : base()
        {
            DoubleBuffered = true;
        }
    }
}
