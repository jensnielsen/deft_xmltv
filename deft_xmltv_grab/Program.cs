using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace deft_xmltv_grab
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            settings s = new settings();
            grabber g = new xmltv_grabber(s);
            deft_xmltv_gui gui = new deft_xmltv_gui(g, s);
            if (File.Exists(s.xmltvpath + "xmltv.exe"))
            {
                s.SettingsCommit(s.sd);
                Application.Run(gui);
            }
            else
            {
                MessageBox.Show("Can't find xmltv.exe!");
            }
        }
    }
}
