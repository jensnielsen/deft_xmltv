using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deft_xmltv_grab
{
    static class xmltv
    {
        public static bool exec(string xmltvpath, string args)
        {
            string cmd = xmltvpath + "xmltv.exe";
            log.notice("running \"" + cmd + "\", args \"" + args + "\"");
            //return true;

            System.Diagnostics.ProcessStartInfo procinf = new System.Diagnostics.ProcessStartInfo(cmd, args);///*"cmd", "/c " + */xmltvcmd);
            procinf.RedirectStandardOutput = true;
            procinf.UseShellExecute = false;
            procinf.CreateNoWindow = true;
            procinf.WorkingDirectory = xmltvpath;

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

            string result = "";
            result = proc.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(result))
            {
                log.notice("xmltv says: " + result);
            }

            if (!proc.WaitForExit(5*60*1000))
            {
                log.error("Timeout");
                return false;
            }
            return true;
        }
    }
}
