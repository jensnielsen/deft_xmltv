/*
    Copyright © Jens Nielsen 2010

    This file is part of deft_xmltv.

    deft_xmltv is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    deft_xmltv is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with deft_xmltv.  If not, see <http://www.gnu.org/licenses/>.
*/

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
    public partial class deft_xmltv_gui : Form
    {
        private grabber g;
        private settings s;

        #region Properties


        #endregion

        public deft_xmltv_gui(grabber g, settings s)
        {
            InitializeComponent();
            this.g = g;
            this.s = s;
            
//#if DEBUG
//            log.loglevel = log.loglvl.debug;
//#else
            log.loglevel = log.loglvl.debug;
//#endif
            log.notice("");
            log.notice("App start");

            this.SizeChanged += new System.EventHandler(this.SizeChangedEv);

            s.AppDataChanged += new AppDataChangedHandler(appDataChanged);

            s.init(ConstructSettingsFromForm()); /*default values on form sets default values in settings*/

            SetFormFromSettings(s.sd);
            

            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                if (arg == "-m")
                {
                    this.ShowInTaskbar = false;
                    this.WindowState = FormWindowState.Minimized;
                }
            }
        }


        private void SizeChangedEv(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
            else
            {
                this.ShowInTaskbar = true;
            }
        }

        private void manualgrab_Click(object sender, EventArgs e)
        {
            g.work(ConstructSettingsFromForm());
        }

        private void deft_xmltv_gui_Load(object sender, EventArgs e)
        {
        }
        private void SetFormFromSettings(SettingsData s)
        {
            winstart.Checked = s.winstart;

            grabdays.Value = s.grabdays;
            outputfile.Text = /*String.IsNullOrEmpty(s.outputfile) ? "tvguide.xml" :*/ s.outputfile;

            copyftr.Checked = s.copyftr;

            autograb.Checked = s.autograb;
            autograbtime.Value = s.autograbtime;//new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, s.autograbtime.Hours, s.autograbtime.Minutes, 0);
            if (s.autograbinterval == 1)
                autograbinterval.SelectedIndex = 0;

            icons4TR.Checked = s.geticons4tr;
            iconsMP.Checked = s.geticonsmp;

            if (s.ftriconspath == "")
            {
                icons4TR.Enabled = false;
            }

            merge.Checked = s.mergegrab;
            mergefile.Text = s.mergefile;
        }

        private SettingsData ConstructSettingsFromForm()
        {
            SettingsData s = this.s.sd/*.Copy()*/;

            s.winstart = winstart.Checked;

            s.grabdays = (int)grabdays.Value;
            s.outputfile = String.IsNullOrEmpty(outputfile.Text) ? "tvguide.xml" : outputfile.Text;

            s.copyftr = copyftr.Checked;

            s.autograb = autograb.Checked;
            s.autograbtime = autograbtime.Value;
            if (autograbinterval.SelectedIndex == 0)
                s.autograbinterval = 1;
            else
                throw new System.Exception();

            s.geticons4tr = icons4TR.Checked;
            s.geticonsmp = iconsMP.Checked;

            s.mergefile = mergefile.Text;
            s.mergegrab = merge.Checked;

            return s;
        }

        private void SettingsCommit(SettingsData sd)
        {
            s.SettingsCommit(sd);
        }
            
        private void save_Click(object sender, EventArgs e)
        {
            SettingsData sd = ConstructSettingsFromForm();
            SettingsCommit(sd);
        }

        private void appDataChanged(AppData ad)
        {
            if (lastgrablabel.InvokeRequired || nextgrablabel.InvokeRequired)
            {
                // on a different thread.. callback to self
                log.debug("invoke...");
                AppDataChangedHandler d = new AppDataChangedHandler(appDataChanged);
                this.Invoke(d, new object[] { ad });
            }
            else
            {
                log.debug("setting grab labels...");
                if (ad.lastgrab == DateTime.MinValue)
                    lastgrablabel.Text = "Last grab: N/A";
                else
                    lastgrablabel.Text = "Last grab: " + ad.lastgrab.ToString();
                
                if (ad.nextgrab == DateTime.MinValue)
                    nextgrablabel.Text = "Next scheduled grab: N/A";
                else
                    nextgrablabel.Text = "Next scheduled grab: " + ad.nextgrab.ToString();
            }
        }

        private void autograb_CheckedChanged(object sender, EventArgs e)
        {
            autograbtime.Enabled = autograb.Checked;
            autograbinterval.Enabled = autograb.Checked;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.BringToFront();
            this.Focus();
            this.TopMost = false;
        }

        private void outputbrowsebutton_Click(object sender, EventArgs e)
        {
            if (outputdlg.ShowDialog() == DialogResult.OK)
            {
                outputfile.Text = outputdlg.FileName;
                outputdlg.InitialDirectory = Path.GetDirectoryName(outputdlg.FileName);

            }
        }

  /*      private void setpathmp_Click(object sender, EventArgs e)
        {
            outputfile.Text = s.xmltvpath + "tvguide.xml";
            outputdlg.InitialDirectory = s.xmltvpath;
            outputdlg.FileName = "tvguide.xml";
        }

        private void setpath4tr_Click(object sender, EventArgs e)
        {
            outputfile.Text = s.ftrguidepath + "guide.xml";
            outputdlg.InitialDirectory = s.ftrguidepath;
            outputdlg.FileName = "guide.xml";
        }*/

        private void channelconfigbtn_Click(object sender, EventArgs e)
        {
            channelconfig cc = new channelconfig(/*g.getchannels()*/);
            cc.FormClosed += new FormClosedEventHandler(cc_FormClosed);
            cc.Show();
            cc.setchannels(g.getchannels());
        }

        void cc_FormClosed(object sender, FormClosedEventArgs e)
        {
            channelconfig cc = (channelconfig)sender;
            if (cc.savechannels)
                g.setchannels(cc.getchannels());
        }

        private void deft_xmltv_gui_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.notice("App closing");
        }

        private void mergebrowsebutton_Click(object sender, EventArgs e)
        {
            if (mergedlg.ShowDialog() == DialogResult.OK)
            {
                mergefile.Text = mergedlg.FileName;
                mergedlg.InitialDirectory = Path.GetDirectoryName(mergedlg.FileName);

            }
        }
    }
}
