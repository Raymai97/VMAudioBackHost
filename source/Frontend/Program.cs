using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Frontend
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
