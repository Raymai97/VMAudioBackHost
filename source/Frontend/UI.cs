using System.Windows.Forms;

namespace Frontend
{
    internal class UI
    {
        public static void MsgErr(string text, string title = "Error")
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void MsgInfo(string text, string title = "Info")
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
