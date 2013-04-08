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
using System.IO;
using System.Windows.Forms;



namespace deft_xmltv_grab
{
    static class log
    {
        public enum loglvl{
            debug,
            notice,
            warning,
            error
        }
        private static loglvl _loglevel;


        #region Properties
        public static loglvl loglevel
        {
            set { _loglevel = value; }
        }
        #endregion

        private static void write (string txt)
        {
            DateTime now = DateTime.Now.ToLocalTime();
            string l = now.ToString() + ": " + txt;
            Console.WriteLine(l);
            StreamWriter sw = File.AppendText(Application.StartupPath + "\\deft-xmltv.log");
            sw.WriteLine(l);
            sw.Close();
        }

        public static void debug(string txt)
        {
            if (_loglevel <= loglvl.debug)
                write("DBG: " + txt);
        }

        public static void notice(string txt)
        {
            if (_loglevel <= loglvl.notice)
                write(txt);
        }

        public static void warning(string txt)
        {
            if (_loglevel <= loglvl.warning)
                write("WARN: " + txt);
        }

        public static void error(string txt)
        {
            if (_loglevel <= loglvl.error)
                write("ERR: " + txt);
        }
    }



}
