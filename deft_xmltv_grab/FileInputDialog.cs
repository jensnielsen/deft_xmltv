using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace deft_xmltv_grab
{
    public partial class FileInputDialog : Form
    {
        #region properties

        public string path
        {
            get { return filetxtbox.Text; }
        }

        #endregion

        public FileInputDialog(string labeltext, string defaultpath)
        {
            init(labeltext, defaultpath);
        }

        public FileInputDialog(string labeltext)
        {
            init(labeltext, "");
        }

        private void init(string labeltext, string defaultpath)
        {
            InitializeComponent();

            label1.Text = labeltext;

            filetxtbox.Text = defaultpath;

            if (defaultpath != "")
            {
                pathdlg.SelectedPath = defaultpath;
            }

        }

        private void browsebutton_Click(object sender, EventArgs e)
        {
            if (pathdlg.ShowDialog() == DialogResult.OK)
            {
                filetxtbox.Text = pathdlg.SelectedPath;
                //pathdlg.InitialDirectory = pathdlg.SelectedPath;

            }
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
