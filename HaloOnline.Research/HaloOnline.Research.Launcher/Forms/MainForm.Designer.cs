namespace HaloOnline.Research.Launcher.Forms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerateUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDetectedBuild = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnPlay = new System.Windows.Forms.Button();
            this.cmbBuild = new System.Windows.Forms.ComboBox();
            this.grpTargetBuild = new System.Windows.Forms.GroupBox();
            this.chkExtendedValidation = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpTargetBuild.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(206, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(92, 22);
            this.mnuExit.Text = "Exit";
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGenerateUpdate});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(47, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuGenerateUpdate
            // 
            this.mnuGenerateUpdate.Name = "mnuGenerateUpdate";
            this.mnuGenerateUpdate.Size = new System.Drawing.Size(209, 22);
            this.mnuGenerateUpdate.Text = "Generate Update Package";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDetectedBuild});
            this.statusStrip1.Location = new System.Drawing.Point(0, 149);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(206, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblDetectedBuild
            // 
            this.lblDetectedBuild.Name = "lblDetectedBuild";
            this.lblDetectedBuild.Size = new System.Drawing.Size(141, 17);
            this.lblDetectedBuild.Text = "Detected Build: 1.0.12.458";
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.Location = new System.Drawing.Point(12, 87);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(182, 33);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "Play Game";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // cmbBuild
            // 
            this.cmbBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBuild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuild.FormattingEnabled = true;
            this.cmbBuild.Location = new System.Drawing.Point(6, 19);
            this.cmbBuild.Name = "cmbBuild";
            this.cmbBuild.Size = new System.Drawing.Size(170, 21);
            this.cmbBuild.TabIndex = 3;
            // 
            // grpTargetBuild
            // 
            this.grpTargetBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTargetBuild.Controls.Add(this.cmbBuild);
            this.grpTargetBuild.Location = new System.Drawing.Point(12, 27);
            this.grpTargetBuild.Name = "grpTargetBuild";
            this.grpTargetBuild.Size = new System.Drawing.Size(182, 54);
            this.grpTargetBuild.TabIndex = 4;
            this.grpTargetBuild.TabStop = false;
            this.grpTargetBuild.Text = "Target Build";
            // 
            // chkExtendedValidation
            // 
            this.chkExtendedValidation.AutoSize = true;
            this.chkExtendedValidation.Checked = true;
            this.chkExtendedValidation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExtendedValidation.Location = new System.Drawing.Point(12, 126);
            this.chkExtendedValidation.Name = "chkExtendedValidation";
            this.chkExtendedValidation.Size = new System.Drawing.Size(120, 17);
            this.chkExtendedValidation.TabIndex = 5;
            this.chkExtendedValidation.Text = "Extended Validation";
            this.chkExtendedValidation.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 171);
            this.Controls.Add(this.chkExtendedValidation);
            this.Controls.Add(this.grpTargetBuild);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Anvil Online Test Launcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpTargetBuild.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerateUpdate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ComboBox cmbBuild;
        private System.Windows.Forms.GroupBox grpTargetBuild;
        private System.Windows.Forms.ToolStripStatusLabel lblDetectedBuild;
        private System.Windows.Forms.CheckBox chkExtendedValidation;
    }
}

