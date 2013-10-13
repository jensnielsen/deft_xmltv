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
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;


namespace deft_xmltv_grab
{
    public delegate void SettingsChangedHandler (/*SettingsData s*/);
    public delegate void AppDataChangedHandler(AppData a);

    public struct AppData
    {
        public DateTime lastgrab;
        public DateTime nextgrab;
    }

    public struct SettingsData/*: EventArgs*/
    {
        public bool winstart;

        public bool autograb/* = false*/;
        public DateTime autograbtime;
        public int autograbinterval;
        public int grabdays;
        public string outputfile;

        public bool copyftr;

        public bool geticons4tr;
        public bool geticonsmp;

        public bool mergegrab;
        public string mergefile;

        public string xmltvpath;
        public string ftrguidepath;
        public string ftriconspath;
        public string mpiconspath;

        /*private TriggersChange t;
        #region properties

        public bool autograb
        {
            get { return t._autograb; }
            set { t._autograb = value; }
        }*/

        //#endregion
        /*public SettingsData()
        {
        }

        public SettingsData(SettingsData orig)
        {
            this.autograb = orig.autograb;
        }

        public SettingsData Copy()
        {
            SettingsData sd = new SettingsData(this);
            return sd;
        }*/

        /*public bool Equals(SettingsData s)
        {
            return (this.t.Equals(s.t));
        }*/
    }

    public class settings
    {

        private SettingsData _sd;
        private AppData _ad;
        private bool _committed = false;

        public event SettingsChangedHandler SettingsChanged;
        public event AppDataChangedHandler AppDataChanged;

        #region properties

        public SettingsData sd
        {
            get { return _sd; } /* Returns copy of struct */
        }

        public bool initialised
        {
            get { return _committed; }
        }

        //Settings
        public bool autograb
        {
            get { return _sd.autograb; }
        }

        public DateTime autograbtime
        {
            get { return _sd.autograbtime; }
        }

        public int autograbinterval
        {
            get { return _sd.autograbinterval; }
        }

        public int grabdays
        {
            get { return _sd.grabdays; }
        }

        public string outputfile
        {
            get { return _sd.outputfile; }
        }

        public bool copyftr
        {
            get { return _sd.copyftr; }
        }

        public bool geticons4tr
        {
            get { return _sd.geticons4tr; }
        }

        public bool geticonsmp
        {
            get { return _sd.geticonsmp; }
        }

        public string ftriconspath
        {
            get { return _sd.ftriconspath; }
        }

        public string mpiconspath
        {
            get { return _sd.mpiconspath; }
        }

        public string xmltvpath
        {
            get { return _sd.xmltvpath; }
        }

        public string ftrguidepath
        {
            get { return _sd.ftrguidepath; }
        }

        public bool mergegrab
        {
            get { return _sd.mergegrab; }
        }
        
        public string mergefile
        {
            get { return _sd.mergefile; }
        }

        //Appdata
        public DateTime lastgrab
        {
            set { 
                _ad.lastgrab = value;
                OnAppDataChanged();       
            }
            get { return _ad.lastgrab; }
        }

        public DateTime nextgrab
        {
            set
            {
                _ad.nextgrab = value;
                OnAppDataChanged();
            }
        }

        #endregion
        
        public settings ()
        {
        }

        public void init(SettingsData defaultsettings)
        {
            /* default non-gui settings */
            RegistryKey rkApp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\For The Record\\Install", false);
            string ftrpath = ((rkApp != null) ? (string)rkApp.GetValue("", "") : "");

            defaultsettings.ftriconspath = (ftrpath != "" ? ftrpath + "Services\\Channel Logos\\" : ""); //"C:\\xmltv\\icons\\4tr\\"
            defaultsettings.ftrguidepath = (ftrpath != "" ? ftrpath + "Services\\XMLTV\\guide.xml" : "");
            defaultsettings.mpiconspath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE") + "\\Team MediaPortal\\Thumbs\\TV\\Logos\\"; /*"C:\\xmltv\\icons\\mp\\";*/
            defaultsettings.xmltvpath = "C:\\xmltv\\";

            if (!File.Exists(Application.StartupPath + "\\deft-xmltv.cfg"))
            {
                FileInputDialog d = new FileInputDialog("Path to xmltv", "C:\\xmltv");

                d.ShowDialog();
                defaultsettings.xmltvpath = d.path;
                defaultsettings.outputfile = d.path + (d.path.EndsWith("\\") ? "" : "\\") + "tvguide.xml";
            }
            
            _sd = defaultsettings;

            
            if (!readcfg())
            {
                log.warning("Error reading config file!");
            }
        }

        private void writecfg()
        {
            /*System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings["autograb"] == null)
                config.AppSettings.Settings.Add("autograb", _sd.autograb.ToString());
            else
                config.AppSettings.Settings["autograb"].Value = _sd.autograb.ToString();

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");*/

            StreamWriter sw = File.CreateText(Application.StartupPath + "\\deft-xmltv.cfg");
            sw.WriteLine("winstart = " + (_sd.winstart ? "1" : "0"));
            sw.WriteLine("grabdays = " + _sd.grabdays);
            sw.WriteLine("outputfile = " + _sd.outputfile);
            sw.WriteLine("copytoftr = " + (_sd.copyftr ? "1" : "0"));
            sw.WriteLine("autograb = " + (_sd.autograb ? "1" : "0"));
            sw.WriteLine("autograbtime = " + (_sd.autograbtime.ToString()));
            sw.WriteLine("autograbinterval = " + _sd.autograbinterval);
            sw.WriteLine("mergegrab = " + (_sd.mergegrab ? "1" : "0"));
            sw.WriteLine("mergefile = " + _sd.mergefile);

            sw.WriteLine("geticons4tr = " + (_sd.geticons4tr ? "1" : "0"));
            sw.WriteLine("geticonsmp = " + (_sd.geticonsmp ? "1" : "0"));

            sw.WriteLine("4trguidepath = " + _sd.ftrguidepath);
            sw.WriteLine("4triconspath = " + _sd.ftriconspath);
            sw.WriteLine("mpiconspath = " + _sd.mpiconspath);
            sw.WriteLine("xmltvpath = " + _sd.xmltvpath);
            
            sw.WriteLine("lastgrab = " + _ad.lastgrab.ToString());
            sw.Close();
        }

        private bool readcfg()
        {
            /*string s;
            s = ConfigurationManager.AppSettings["autograb"];
            _sd.autograb = string.IsNullOrEmpty(s) ? false : s.Equals("True");*/

            bool fileexists = false;
            if (File.Exists(Application.StartupPath + "\\deft-xmltv.cfg"))
            {
                fileexists = true;
                StreamReader sr;
                sr = File.OpenText(Application.StartupPath + "\\deft-xmltv.cfg");

                log.notice("reading config file");
                string[] l;
                while (!sr.EndOfStream)
                {
                    l = sr.ReadLine().Split('=');
                    if (l.GetLength(0) == 2)
                    {
                        l[0] = l[0].Trim();
                        l[1] = l[1].Trim();

                        log.notice("key " + l[0] + " = " + l[1]);

                        try
                        {
                            if (l[0].Equals("winstart"))
                            {
                                _sd.winstart = l[1].Equals("1");
                            }
                            else if (l[0].Equals("grabdays"))
                            {
                                _sd.grabdays = int.Parse(l[1]);
                            }
                            else if (l[0].Equals("outputfile"))
                            {
                                _sd.outputfile = l[1];
                            }
                            else if (l[0].Equals("copytoftr"))
                            {
                                _sd.copyftr = l[1].Equals("1");
                            }
                            else if (l[0].Equals("autograb"))
                            {
                                _sd.autograb = l[1].Equals("1");
                            }
                            else if (l[0].Equals("autograbtime"))
                            {
                                _sd.autograbtime = DateTime.Parse(l[1]);
                            }
                            else if (l[0].Equals("autograbinterval"))
                            {
                                _sd.autograbinterval = int.Parse(l[1]);
                            }
                            else if (l[0].Equals("lastgrab"))
                            {
                                lastgrab = DateTime.Parse(l[1]); //use lastgrab property to trigger event to gui
                            }
                            else if (l[0].Equals("geticons4tr"))
                            {
                                _sd.geticons4tr = l[1].Equals("1");
                            }
                            else if (l[0].Equals("geticonsmp"))
                            {
                                _sd.geticonsmp = l[1].Equals("1");
                            }
                            else if (l[0].Equals("4trguidepath") && l[1] != "")
                            {
                                _sd.ftrguidepath = l[1];
                            }
                            else if (l[0].Equals("4triconspath") && l[1] != "")
                            {
                                _sd.ftriconspath = l[1];
                            }
                            else if (l[0].Equals("mpiconspath") && l[1] != "")
                            {
                                _sd.mpiconspath = l[1];
                            }
                            else if (l[0].Equals("xmltvpath") && l[1] != "")
                            {
                                _sd.xmltvpath = l[1];
                            }
                            else if (l[0].Equals("mergefile"))
                            {
                                _sd.mergefile = l[1];
                            }
                            else if (l[0].Equals("mergegrab"))
                            {
                                _sd.mergegrab = l[1].Equals("1");
                            }
                        }
                        catch
                        {
                            log.notice("Failed parse of key value");
                        }
                    }
                }
                sr.Close();
            }

            if (_sd.ftrguidepath == "")
                _sd.copyftr = false;

            if (_sd.ftriconspath == "")
                _sd.geticons4tr = false;
            else if (!_sd.ftriconspath.EndsWith("\\"))
            {
                _sd.ftriconspath += "\\";
            }

            if (_sd.mpiconspath != "" && !_sd.mpiconspath.EndsWith("\\"))
            {
                _sd.mpiconspath += "\\";
            }

            if (_sd.xmltvpath != "" && !_sd.xmltvpath.EndsWith("\\"))
            {
                _sd.xmltvpath += "\\";
            }

/*            if (_sd.ftrguidepath != "" && !_sd.ftrguidepath.EndsWith("\\"))
            {
                _sd.ftrguidepath += "\\";
            }
            */
            return fileexists;
        }

        public void SettingsCommit(SettingsData sd)
        {
            if (_committed == false || !sd.Equals(_sd))
            {
                log.notice("Settings changed");
                _committed = true;
                _sd = sd;
                OnSettingsChanged(_sd);
                writecfg();

                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (sd.winstart)
                {
                    /*delete old path in case exe moved*/
                    rkApp.DeleteValue("deft_xmltv", false);
                    /*enable with current path*/
                    rkApp.SetValue("deft_xmltv", "\"" + Application.ExecutablePath.ToString() + "\" -m");
                }
                else if (!sd.winstart && rkApp.GetValue("deft_xmltv") != null)
                {
                    /*disable*/
                    rkApp.DeleteValue("deft_xmltv", false);
                }
            }
        }

        protected virtual void OnAppDataChanged()
        {
            AppDataChanged(_ad);
        }
        protected virtual void OnSettingsChanged(SettingsData s)
        {
            SettingsChanged(/*s*/);
        }
    }
}
