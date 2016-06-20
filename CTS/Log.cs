using System.Windows.Forms;
using CTS;

namespace BotSpeaker
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
            richTextBox1.Text = LogController.GetLog();
        }
    }
}