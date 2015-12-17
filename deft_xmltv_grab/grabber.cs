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
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.IO;


namespace deft_xmltv_grab
{
    class downloadable
    {
        public string url;
        public string file;

        public downloadable(string _url, string _file)
        {
            url = _url;
            file = _file;
        }
    }

    public abstract class grabber
    {
        private System.Timers.Timer nextgrab = null;

        private settings committedsettings;

        public string xmltvpath;

        public grabber(settings _s)
        {
            committedsettings = _s;
            committedsettings.SettingsChanged += new SettingsChangedHandler(settingsChanged);
            nextgrab = new System.Timers.Timer();
            nextgrab.Elapsed += new System.Timers.ElapsedEventHandler(this.nextgrab_Tick);
        }

        private void fetchicons(SettingsData s)
        {
            log.notice("Fetching icons");
            //return;

            XmlTextReader reader = new XmlTextReader (/*s.outputfile*/s.xmltvpath + "tvguide.xml");
            try
            {
                string url = "";
                string channelname = "";
                string ext = "";
                List<downloadable> dlist = new List<downloadable>();
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if(reader.Name == "channel")
                            {
                                url = "";
                                channelname = "";
                                ext = "";
                                log.debug("startchannel");
                            }
                            else if (reader.Name == "icon")
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "src")
                                    {
                                        url = reader.Value;
                                        ext = Path.GetExtension(reader.Value);
                                        log.debug("src: " + url);
                                    }
                                }
                            }
                            else if(reader.Name == "display-name")
                            {
                                while (reader.Read() && reader.NodeType != XmlNodeType.Text) ;
                                channelname = reader.Value;
                                log.debug("displayname: " + channelname);
                            }
                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Name == "channel")
                            {
                                if (url != "" && channelname != "")
                                {
                                    string filename;
                                    string invalidchars = new string(Path.GetInvalidFileNameChars());
                                    if (s.geticons4tr)
                                    {
                                        filename = Regex.Replace(channelname, @"[" + invalidchars + "]", ""); //4tr wants to skip invalid chars
                                        filename = s.ftriconspath + filename + ext;
                                        log.debug("storing: " + url + " -> " + filename);
                                        dlist.Add(new downloadable(url, filename));
                                    }
                                    if (s.geticonsmp)
                                    {
                                        filename = Regex.Replace(channelname, @"[" + invalidchars + "]", "_"); //mp wants to replace invalid chars with underscore
                                        filename = s.mpiconspath + filename + ext;
                                        log.debug("storing: " + url + " -> " + filename);
                                        dlist.Add(new downloadable(url, filename));
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                WebClient Client = new WebClient();
                foreach (downloadable d in dlist)
                {
                    log.notice("downloading: " + d.url + " -> " + d.file);
                    try
                    {
                        string path = Path.GetDirectoryName(d.file);
                        if(!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        Client.DownloadFile(d.url, d.file);
                    }
                    catch
                    {
                        log.error("fail :(");
                    }
                }
            }
            catch
            {
                log.error("Failed parse of tvguide.xml");
            }
        }


        public bool work(SettingsData s)
        {
            try
            {
                doWork(committedsettings.sd);
                return true;
            }
            catch (Exception ex)
            {
                log.error("Failed grabbing, caught " + ex);
            }
            return false;
        }

        private void doWork( SettingsData s )
        {
            log.notice("Grabbing...");
            if (grab(s))
            {
                domerge(s);

                /* Create destination directory if it doesn't exist */
                string path = Path.GetDirectoryName(s.outputfile);
                if (!String.IsNullOrEmpty(path) && !Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!s.outputfile.Equals(s.xmltvpath + "tvguide.xml", StringComparison.CurrentCultureIgnoreCase))
                {
                    /* Copy completed file to destination */
                    log.notice("Copying finished file (" + s.xmltvpath + "tvguide.xml" + ") to destination " + s.outputfile);
                    File.Copy(s.xmltvpath + "tvguide.xml", s.outputfile, true);
                }

                if(s.copyftr && !s.ftrguidepath.Equals(s.xmltvpath + "tvguide.xml", StringComparison.CurrentCultureIgnoreCase))
                {
                    /* Copy completed file to FTR */
                    log.notice("Copying finished file (" + s.xmltvpath + "tvguide.xml" + ") to For The Record (" + s.ftrguidepath + ")");
                    File.Copy(s.xmltvpath + "tvguide.xml", s.ftrguidepath, true);
                }

                committedsettings.lastgrab = DateTime.Now.ToLocalTime();

                if (s.geticons4tr || s.geticonsmp)
                {
                    fetchicons(s);
                }
            }

            log.debug("Work done");
        }

        private void domerge(SettingsData s)
        {
            /* All files has to be in same encoding, begin with converting everything to utf-8 */
            if (!Directory.Exists(s.xmltvpath + "mergedir"))
                Directory.CreateDirectory(s.xmltvpath + "mergedir");

            if (Directory.Exists(s.xmltvpath + "xmltv_guides") )
            {
                string[] files = Directory.GetFiles(s.xmltvpath + "xmltv_guides");

                foreach (string file in files)
                {
                    log.debug("encoding " + file);

                    XDocument xd = XDocument.Load(file);
                    XDeclaration declaration = xd.Declaration;
                    if (declaration != null)
                    {
                        declaration.Encoding = "utf-8";
                    }
                    xd.Save(s.xmltvpath + "mergedir\\" + Path.GetFileNameWithoutExtension(file) + "-utf8.xml");
                }
            }

            if (!String.IsNullOrEmpty(s.mergefile) && File.Exists(s.mergefile))
            {
                log.debug("encoding mergefile");
                XDocument xd = XDocument.Load(s.mergefile);
                XDeclaration declaration = xd.Declaration;
                if (declaration != null)
                {
                    declaration.Encoding = "utf-8";
                }
                xd.Save(s.xmltvpath + "mergedir\\mergefile-utf8.xml");
            }
            /* Go! */
            string[] mergefiles = Directory.GetFiles(s.xmltvpath + "mergedir");
            string args = "tv_cat --output tvguide.xml";
            foreach (string file in mergefiles)
            {
                args += " " + file;
            }
            xmltv.exec(s.xmltvpath, args);

            /* Clean up evidence */
            Directory.Delete(s.xmltvpath + "mergedir", true);
        }

        public void enableAutoGrab()
        {
            log.debug("enabling autograb");
            setNextGrab(getDelayToNextGrab());
            nextgrab.AutoReset = false;
        }

        public void disableAutoGrab()
        {
            log.debug("disabling autograb");
            nextgrab.Stop();
            committedsettings.nextgrab = DateTime.MinValue;
        }

        private void setNextGrab(double timeout)
        {
            nextgrab.Interval = timeout;
            nextgrab.Start();
            committedsettings.nextgrab = DateTime.Now.ToLocalTime().AddMilliseconds(timeout);
        }

        private double getDelayToNextGrab()
        {
            DateTime now = DateTime.Now.ToLocalTime();
            double delay;
            Random rand = new Random();
            //DateTime last = s.lastgrab.TimeOfDay;
            if (committedsettings.lastgrab.Date == now.Date)
            {
                /*Last grab was today, next grab is tomorrow*/
                delay = (24*60*60*1000) - (now.TimeOfDay.TotalMilliseconds - committedsettings.autograbtime.TimeOfDay.TotalMilliseconds);
            }
            else
            {
                /*Today*/
                if (committedsettings.autograbtime.TimeOfDay.TotalMilliseconds <= now.TimeOfDay.TotalMilliseconds)
                {
                    /* Right away! */
                    delay = 0;
                }
                else
                {
                    /* Later */
                    delay = committedsettings.autograbtime.TimeOfDay.TotalMilliseconds - now.TimeOfDay.TotalMilliseconds;
                }
            }
            /* Add +/- 1 hour */
            delay += rand.Next(7200000);
            if (delay <= 3600000)
                delay = 1000;
            else
                delay -= 3600000;
            return delay;
        }

        private void nextgrab_Tick(object sender, EventArgs e)
        {
            if ( work(committedsettings.sd) )
            {
                setNextGrab(getDelayToNextGrab());
            }
            else
            {
                //log.error( "Caught " + ex );
                setNextGrab( 5 * 60 * 1000 );
            }
        }

        private void settingsChanged(/*SettingsData s*/)
        {
            log.notice("Whoa, grabber got settings");
            xmltvpath = committedsettings.xmltvpath;

            if (committedsettings.autograb)
                enableAutoGrab();
            else
                disableAutoGrab();
        }

        public abstract void setchannels(List<channelentry> l);
        public abstract List<channelentry> getchannels();
        protected abstract bool grab(SettingsData s);

    }
}
