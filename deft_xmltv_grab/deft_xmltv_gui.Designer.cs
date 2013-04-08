namespace deft_xmltv_grab
{
    partial class deft_xmltv_gui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(deft_xmltv_gui));
            this.manual = new System.Windows.Forms.Button();
            this.autograbinterval = new System.Windows.Forms.ComboBox();
            this.autograb = new System.Windows.Forms.CheckBox();
            this.icons4TR = new System.Windows.Forms.CheckBox();
            this.iconsMP = new System.Windows.Forms.CheckBox();
            this.save = new System.Windows.Forms.Button();
            this.lastgrablabel = new System.Windows.Forms.Label();
            this.nextgrablabel = new System.Windows.Forms.Label();
            this.autograbtime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.grabdays = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.outputdlg = new System.Windows.Forms.OpenFileDialog();
            this.outputbrowsebutton = new System.Windows.Forms.Button();
            this.outputfile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.winstart = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.channelconfigbtn = new System.Windows.Forms.Button();
            this.mergefile = new System.Windows.Forms.TextBox();
            this.mergebrowsebutton = new System.Windows.Forms.Button();
            this.merge = new System.Windows.Forms.CheckBox();
            this.mergedlg = new System.Windows.Forms.OpenFileDialog();
            this.copyftr = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grabdays)).BeginInit();
            this.SuspendLayout();
            // 
            // manual
            // 
            this.manual.Location = new System.Drawing.Point(205, 305);
            this.manual.Name = "manual";
            this.manual.Size = new System.Drawing.Size(94, 25);
            this.manual.TabIndex = 0;
            this.manual.Text = "Manual grab";
            this.manual.UseVisualStyleBackColor = true;
            this.manual.Click += new System.EventHandler(this.manualgrab_Click);
            // 
            // autograbinterval
            // 
            this.autograbinterval.Enabled = false;
            this.autograbinterval.FormattingEnabled = true;
            this.autograbinterval.Items.AddRange(new object[] {
            "Daily"});
            this.autograbinterval.Location = new System.Drawing.Point(22, 132);
            this.autograbinterval.Name = "autograbinterval";
            this.autograbinterval.Size = new System.Drawing.Size(109, 21);
            this.autograbinterval.TabIndex = 1;
            this.autograbinterval.Text = "Daily";
            // 
            // autograb
            // 
            this.autograb.AutoSize = true;
            this.autograb.Location = new System.Drawing.Point(24, 111);
            this.autograb.Name = "autograb";
            this.autograb.Size = new System.Drawing.Size(108, 17);
            this.autograb.TabIndex = 2;
            this.autograb.Text = "Automatic grabs?";
            this.autograb.UseVisualStyleBackColor = true;
            this.autograb.CheckedChanged += new System.EventHandler(this.autograb_CheckedChanged);
            // 
            // icons4TR
            // 
            this.icons4TR.AutoSize = true;
            this.icons4TR.Location = new System.Drawing.Point(189, 218);
            this.icons4TR.Name = "icons4TR";
            this.icons4TR.Size = new System.Drawing.Size(110, 17);
            this.icons4TR.TabIndex = 3;
            this.icons4TR.Text = "Get icons for 4TR";
            this.icons4TR.UseVisualStyleBackColor = true;
            // 
            // iconsMP
            // 
            this.iconsMP.AutoSize = true;
            this.iconsMP.Location = new System.Drawing.Point(24, 218);
            this.iconsMP.Name = "iconsMP";
            this.iconsMP.Size = new System.Drawing.Size(145, 17);
            this.iconsMP.TabIndex = 4;
            this.iconsMP.Text = "Get icons for MediaPortal";
            this.iconsMP.UseVisualStyleBackColor = true;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(31, 305);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(76, 24);
            this.save.TabIndex = 5;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // lastgrablabel
            // 
            this.lastgrablabel.AutoSize = true;
            this.lastgrablabel.Location = new System.Drawing.Point(23, 249);
            this.lastgrablabel.Name = "lastgrablabel";
            this.lastgrablabel.Size = new System.Drawing.Size(54, 13);
            this.lastgrablabel.TabIndex = 7;
            this.lastgrablabel.Text = "Last grab:";
            // 
            // nextgrablabel
            // 
            this.nextgrablabel.AutoSize = true;
            this.nextgrablabel.Location = new System.Drawing.Point(23, 272);
            this.nextgrablabel.Name = "nextgrablabel";
            this.nextgrablabel.Size = new System.Drawing.Size(108, 13);
            this.nextgrablabel.TabIndex = 8;
            this.nextgrablabel.Text = "Next scheduled grab:";
            // 
            // autograbtime
            // 
            this.autograbtime.CustomFormat = "HH:mm";
            this.autograbtime.Enabled = false;
            this.autograbtime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.autograbtime.Location = new System.Drawing.Point(198, 132);
            this.autograbtime.Name = "autograbtime";
            this.autograbtime.ShowUpDown = true;
            this.autograbtime.Size = new System.Drawing.Size(59, 20);
            this.autograbtime.TabIndex = 9;
            this.autograbtime.Value = new System.DateTime(2010, 7, 28, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "at around";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "deft_xmltv";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // grabdays
            // 
            this.grabdays.Location = new System.Drawing.Point(90, 13);
            this.grabdays.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.grabdays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.grabdays.Name = "grabdays";
            this.grabdays.Size = new System.Drawing.Size(38, 20);
            this.grabdays.TabIndex = 11;
            this.grabdays.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Days to grab:";
            // 
            // outputdlg
            // 
            this.outputdlg.CheckFileExists = false;
            this.outputdlg.CheckPathExists = false;
            this.outputdlg.FileName = "tvguide.xml";
            this.outputdlg.RestoreDirectory = true;
            // 
            // outputbrowsebutton
            // 
            this.outputbrowsebutton.Location = new System.Drawing.Point(240, 55);
            this.outputbrowsebutton.Name = "outputbrowsebutton";
            this.outputbrowsebutton.Size = new System.Drawing.Size(75, 23);
            this.outputbrowsebutton.TabIndex = 13;
            this.outputbrowsebutton.Text = "Browse...";
            this.outputbrowsebutton.UseVisualStyleBackColor = true;
            this.outputbrowsebutton.Click += new System.EventHandler(this.outputbrowsebutton_Click);
            // 
            // outputfile
            // 
            this.outputfile.Location = new System.Drawing.Point(22, 57);
            this.outputfile.Name = "outputfile";
            this.outputfile.Size = new System.Drawing.Size(212, 20);
            this.outputfile.TabIndex = 14;
            this.outputfile.Text = "C:\\xmltv\\tvguide.xml";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Output file:";
            // 
            // winstart
            // 
            this.winstart.AutoSize = true;
            this.winstart.Location = new System.Drawing.Point(220, 16);
            this.winstart.Name = "winstart";
            this.winstart.Size = new System.Drawing.Size(100, 17);
            this.winstart.TabIndex = 16;
            this.winstart.Text = "start at win start";
            this.winstart.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "+/- 1h";
            // 
            // channelconfigbtn
            // 
            this.channelconfigbtn.Location = new System.Drawing.Point(142, 11);
            this.channelconfigbtn.Name = "channelconfigbtn";
            this.channelconfigbtn.Size = new System.Drawing.Size(66, 23);
            this.channelconfigbtn.TabIndex = 20;
            this.channelconfigbtn.Text = "Channels";
            this.channelconfigbtn.UseVisualStyleBackColor = true;
            this.channelconfigbtn.Click += new System.EventHandler(this.channelconfigbtn_Click);
            // 
            // mergefile
            // 
            this.mergefile.Location = new System.Drawing.Point(22, 186);
            this.mergefile.Name = "mergefile";
            this.mergefile.Size = new System.Drawing.Size(212, 20);
            this.mergefile.TabIndex = 22;
            // 
            // mergebrowsebutton
            // 
            this.mergebrowsebutton.Location = new System.Drawing.Point(240, 184);
            this.mergebrowsebutton.Name = "mergebrowsebutton";
            this.mergebrowsebutton.Size = new System.Drawing.Size(75, 23);
            this.mergebrowsebutton.TabIndex = 21;
            this.mergebrowsebutton.Text = "Browse...";
            this.mergebrowsebutton.UseVisualStyleBackColor = true;
            this.mergebrowsebutton.Click += new System.EventHandler(this.mergebrowsebutton_Click);
            // 
            // merge
            // 
            this.merge.AutoSize = true;
            this.merge.Location = new System.Drawing.Point(23, 165);
            this.merge.Name = "merge";
            this.merge.Size = new System.Drawing.Size(105, 17);
            this.merge.TabIndex = 23;
            this.merge.Text = "Merge grab with:";
            this.merge.UseVisualStyleBackColor = true;
            // 
            // mergedlg
            // 
            this.mergedlg.CheckPathExists = false;
            this.mergedlg.RestoreDirectory = true;
            // 
            // copyftr
            // 
            this.copyftr.AutoSize = true;
            this.copyftr.Location = new System.Drawing.Point(24, 83);
            this.copyftr.Name = "copyftr";
            this.copyftr.Size = new System.Drawing.Size(140, 17);
            this.copyftr.TabIndex = 24;
            this.copyftr.Text = "Copy to For The Record";
            this.copyftr.UseVisualStyleBackColor = true;
            // 
            // deft_xmltv_gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 345);
            this.Controls.Add(this.copyftr);
            this.Controls.Add(this.merge);
            this.Controls.Add(this.mergefile);
            this.Controls.Add(this.mergebrowsebutton);
            this.Controls.Add(this.channelconfigbtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.winstart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputfile);
            this.Controls.Add(this.outputbrowsebutton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grabdays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.autograbtime);
            this.Controls.Add(this.nextgrablabel);
            this.Controls.Add(this.lastgrablabel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.iconsMP);
            this.Controls.Add(this.icons4TR);
            this.Controls.Add(this.autograb);
            this.Controls.Add(this.autograbinterval);
            this.Controls.Add(this.manual);
            this.MaximizeBox = false;
            this.Name = "deft_xmltv_gui";
            this.ShowIcon = false;
            this.Text = "deft xmltv hack";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.deft_xmltv_gui_FormClosing);
            this.Load += new System.EventHandler(this.deft_xmltv_gui_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grabdays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button manual;
        private System.Windows.Forms.ComboBox autograbinterval;
        private System.Windows.Forms.CheckBox autograb;
        private System.Windows.Forms.CheckBox icons4TR;
        private System.Windows.Forms.CheckBox iconsMP;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label lastgrablabel;
        private System.Windows.Forms.Label nextgrablabel;
        private System.Windows.Forms.DateTimePicker autograbtime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.NumericUpDown grabdays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog outputdlg;
        private System.Windows.Forms.Button outputbrowsebutton;
        private System.Windows.Forms.TextBox outputfile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox winstart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button channelconfigbtn;
        private System.Windows.Forms.TextBox mergefile;
        private System.Windows.Forms.Button mergebrowsebutton;
        private System.Windows.Forms.CheckBox merge;
        private System.Windows.Forms.OpenFileDialog mergedlg;
        private System.Windows.Forms.CheckBox copyftr;
    }
}

