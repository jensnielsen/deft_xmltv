namespace deft_xmltv_grab
{
    partial class FileInputDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.filetxtbox = new System.Windows.Forms.TextBox();
            this.filedlg = new System.Windows.Forms.OpenFileDialog();
            this.okbutton = new System.Windows.Forms.Button();
            this.browsebutton = new System.Windows.Forms.Button();
            this.pathdlg = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // filetxtbox
            // 
            this.filetxtbox.Location = new System.Drawing.Point(12, 29);
            this.filetxtbox.Name = "filetxtbox";
            this.filetxtbox.Size = new System.Drawing.Size(237, 20);
            this.filetxtbox.TabIndex = 1;
            // 
            // filedlg
            // 
            this.filedlg.CheckFileExists = false;
            this.filedlg.FileName = "xmltv.exe";
            this.filedlg.RestoreDirectory = true;
            // 
            // okbutton
            // 
            this.okbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okbutton.Location = new System.Drawing.Point(12, 60);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 2;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // browsebutton
            // 
            this.browsebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.browsebutton.Location = new System.Drawing.Point(93, 60);
            this.browsebutton.Name = "browsebutton";
            this.browsebutton.Size = new System.Drawing.Size(75, 23);
            this.browsebutton.TabIndex = 4;
            this.browsebutton.Text = "Browse...";
            this.browsebutton.UseVisualStyleBackColor = true;
            this.browsebutton.Click += new System.EventHandler(this.browsebutton_Click);
            // 
            // pathdlg
            // 
            this.pathdlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.pathdlg.ShowNewFolderButton = false;
            // 
            // FileInputDialog
            // 
            this.AcceptButton = this.okbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 95);
            this.Controls.Add(this.browsebutton);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.filetxtbox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileInputDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "deft xmltv";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog filedlg;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.Button browsebutton;
        private System.Windows.Forms.TextBox filetxtbox;
        private System.Windows.Forms.FolderBrowserDialog pathdlg;
    }
}