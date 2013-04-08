using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace deft_xmltv_grab
{
    class xmltv_grabber
    {
        public const string grabber_id = "tv_grab_se_tvzon";
        public const string rooturl = "http://xmltv.tvzon.se/xmltv/channels.xml.gz";
        /* http://tv.swedb.se/xmltv/channels.xml.gz */
        public string xmltvpath;

        public void setchannels(List<channelentry> l)
        {
            List<string> cfg = new List<string>();
            StreamReader sr;
            //string xmltvpath = committedsettings.xmltvpath;
            try
            {
                sr = File.OpenText(xmltvpath + ".xmltv\\" + grabber_id + ".conf");

                string line;
                while (!sr.EndOfStream)
                {
                    //l = sr.ReadLine().Split("!=".ToCharArray());
                    line = sr.ReadLine();
                    if (!line.StartsWith("channel"))
                    {
                        cfg.Add(line);
                    }
                }
                sr.Close();
            }
            catch
            {
                cfg.Clear();
                cfg.Add("root-url=" + rooturl);
                cfg.Add("cachedir=.\\cache");
            }

            if (!Directory.Exists(xmltvpath + ".xmltv"))
                Directory.CreateDirectory(xmltvpath + ".xmltv");
            StreamWriter sw = File.CreateText(xmltvpath + ".xmltv\\" + grabber_id + ".conf");

            foreach (string line in cfg)
            {
                sw.WriteLine(line);
            }

            foreach (channelentry c in l)
            {
                sw.WriteLine("channel" + (c.selected ? "=" : "!") + c.id);
            }

            sw.Close();
        }

        public List<channelentry> getchannels()
        {
            XmlTextReader reader;
            bool dynamicchannels = true;
            //string xmltvpath = committedsettings.xmltvpath;

            List<channelentry> l = new List<channelentry>();

            /* tiny maneuver to create a config file if it doesn't exist, otherwise we won't even be able to list channels */
            if ( !File.Exists(xmltvpath + ".xmltv\\" + grabber_id + ".conf") )
                setchannels(l);

            if (dynamicchannels || !File.Exists(xmltvpath + "channels_" + grabber_id +".xml"))
            {

                string args = "" + grabber_id + " --list-channels" + /*(dynamicchannels ? "" :*/ " --output " + xmltvpath + "channels_" + grabber_id +".xml"/*)*/;

                if (!xmltv.exec(xmltvpath, args))
                    return l;

                //reader = new XmlTextReader(new StringReader(result));
            }
            //else
            {
                reader = new XmlTextReader(xmltvpath + "channels_" + grabber_id +".xml");
            }

            try
            {
                string displayname = "";
                string id = "";
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "channel")
                            {
                                displayname = "";
                                id = "";
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "id")
                                    {
                                        id = reader.Value;
                                    }
                                }
                                log.debug("startchannel id " + id);
                            }
                            else if (reader.Name == "display-name")
                            {
                                while (reader.Read() && reader.NodeType != XmlNodeType.Text) ;
                                displayname = reader.Value;
                                log.debug("displayname: " + displayname);
                            }
                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Name == "channel")
                            {
                                if (id != "")
                                {
                                    l.Add(new channelentry(ischannelselected(id), displayname, id));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {
            }

            reader.Close();

            return l;
        }


        private bool ischannelselected(string id)
        {
            StreamReader sr;
            //string xmltvpath = committedsettings.xmltvpath;
            try
            {
                sr = File.OpenText(xmltvpath + ".xmltv\\" + grabber_id + ".conf");
            }
            catch
            {
                return false;
            }

            string l;
            while (!sr.EndOfStream)
            {
                l = sr.ReadLine();
                if (l == "channel=" + id)
                {
                    sr.Close();
                    return true;
                }
                else if (l == "channel!" + id)
                {
                    sr.Close();
                    return false;
                }
            }
            sr.Close();
            return false;
        }

        public bool grab(SettingsData s)
        {
            //string xmltvpath = s.xmltvpath;
            //string cmd = xmltvpath + "xmltv.exe";
            string args = "" + grabber_id + " --days " + s.grabdays + " --output xmltv_guides\\tvguide_" + grabber_id + ".xml";/*\"" + s.outputfile + "\""*/ ;
            //log.notice("running \"" + cmd + "\", args \"" + args + "\"");
            //return true;

            if (!Directory.Exists(s.xmltvpath + "xmltv_guides"))
                Directory.CreateDirectory(s.xmltvpath + "xmltv_guides");

            return xmltv.exec(s.xmltvpath, args);
            /*System.Diagnostics.ProcessStartInfo procinf = new System.Diagnostics.ProcessStartInfo(cmd, args);//xmltvcmd);
            procinf.RedirectStandardOutput = true;
            procinf.UseShellExecute = false;
            procinf.CreateNoWindow = true;
            procinf.WorkingDirectory = s.xmltvpath;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procinf;
            try
            {
                proc.Start();
            }
            catch
            {
                log.error("Failed to start process");
                return false;
            }
            log.debug("Started");

            string result = "";
            result = proc.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(result)) 
                log.notice("xmltv says: " + result);

            return proc.WaitForExit(300000);*/

        }
    }
}
