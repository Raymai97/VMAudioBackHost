using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Frontend
{
    internal class Logging
    {
        public void d(string msg)
        {
            Debug.WriteLine("[DEBUG] " + msg);
        }

        public void i(string msg)
        {
            Debug.WriteLine("[INFO] " + msg);
        }
    }
}
