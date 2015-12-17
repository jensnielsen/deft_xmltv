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
            bool ret = true;
            string cmd = xmltvpath + "xmltv.exe";
            log.notice("running \"" + cmd + "\", args \"" + args + "\"");
            //return true;

            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                System.Diagnostics.ProcessStartInfo procinf = new System.Diagnostics.ProcessStartInfo(cmd, args);///*"cmd", "/c " + */xmltvcmd);
                procinf.RedirectStandardOutput = true;
                procinf.UseShellExecute = false;
                procinf.CreateNoWindow = true;
                procinf.WorkingDirectory = xmltvpath;

                proc.StartInfo = procinf;
                try
                {
                    proc.Start();
                }
                catch ( Exception ex )
                {
                    log.error("Failed to start process: " + ex );
                    throw ex;
                }

                string result = "";
                result = proc.StandardOutput.ReadToEnd();
                if (!string.IsNullOrEmpty(result))
                {
                    log.notice("xmltv says: " + result);
                }

                if (!proc.WaitForExit(5 * 60 * 1000))
                {
                    log.error("Timeout");
                    ret = false;
                }
            }
            return ret;
        }
    }
}
