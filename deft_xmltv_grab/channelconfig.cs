using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace deft_xmltv_grab
{
    public partial class channelconfig : Form
    {
        public bool savechannels = false;
        //public List<channelentry> channels;

        public channelconfig(List<channelentry> l)
        {
            InitializeComponent();

            setchannels(l);
        }

        public channelconfig()
        {
            InitializeComponent();
        }

        public void setchannels(List<channelentry> l)
        {
            foreach (channelentry c in l)
            {
                channellist.Items.Add(c);
                channellist.SetItemChecked(channellist.Items.IndexOf(c), c.selected);
            }
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            savechannels = true;
            this.Close();
        }

        public List<channelentry> getchannels()
        {
            List<channelentry> l = new List<channelentry>();

            foreach (channelentry c in this.channellist.Items)
            {
                if (channellist.CheckedItems.Contains(c))
                    c.selected = true;
                else
                    c.selected = false;

                l.Add(c);
            }
            //l.Sort();
            return l;
        }

        private void itemcheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class channelentry
    {
        public bool selected;
        public string displayname;
        public string id;

        public channelentry(bool s, string d, string i)
        {
            this.selected = s;
            this.displayname = d;
            this.id = i;
        }

        public override string ToString()
        {
            return displayname;
        }
    }
}
