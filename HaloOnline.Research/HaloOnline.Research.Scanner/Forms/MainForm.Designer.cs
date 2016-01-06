namespace HaloOnline.Research.Scanner.Forms
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
            this.grpScanSettings = new System.Windows.Forms.GroupBox();
            this.lblMask = new System.Windows.Forms.Label();
            this.lblPattern = new System.Windows.Forms.Label();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.grpScanResults = new System.Windows.Forms.GroupBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpScanSettings.SuspendLayout();
            this.grpScanResults.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpScanSettings
            // 
            this.grpScanSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpScanSettings.Controls.Add(this.lblMask);
            this.grpScanSettings.Controls.Add(this.lblPattern);
            this.grpScanSettings.Controls.Add(this.txtMask);
            this.grpScanSettings.Controls.Add(this.txtPattern);
            this.grpScanSettings.Location = new System.Drawing.Point(12, 12);
            this.grpScanSettings.Name = "grpScanSettings";
            this.grpScanSettings.Size = new System.Drawing.Size(440, 208);
            this.grpScanSettings.TabIndex = 0;
            this.grpScanSettings.TabStop = false;
            this.grpScanSettings.Text = "Scan Settings";
            // 
            // lblMask
            // 
            this.lblMask.AutoSize = true;
            this.lblMask.Location = new System.Drawing.Point(6, 112);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(79, 13);
            this.lblMask.TabIndex = 3;
            this.lblMask.Text = "Mask (optional)";
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Location = new System.Drawing.Point(6, 20);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(41, 13);
            this.lblPattern.TabIndex = 2;
            this.lblPattern.Text = "Pattern";
            // 
            // txtMask
            // 
            this.txtMask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMask.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMask.Location = new System.Drawing.Point(6, 128);
            this.txtMask.Multiline = true;
            this.txtMask.Name = "txtMask";
            this.txtMask.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMask.Size = new System.Drawing.Size(428, 73);
            this.txtMask.TabIndex = 1;
            // 
            // txtPattern
            // 
            this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPattern.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPattern.Location = new System.Drawing.Point(6, 36);
            this.txtPattern.Multiline = true;
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPattern.Size = new System.Drawing.Size(428, 73);
            this.txtPattern.TabIndex = 0;
            // 
            // grpScanResults
            // 
            this.grpScanResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpScanResults.Controls.Add(this.btnScan);
            this.grpScanResults.Controls.Add(this.txtResults);
            this.grpScanResults.Location = new System.Drawing.Point(458, 12);
            this.grpScanResults.Name = "grpScanResults";
            this.grpScanResults.Size = new System.Drawing.Size(182, 208);
            this.grpScanResults.TabIndex = 1;
            this.grpScanResults.TabStop = false;
            this.grpScanResults.Text = "Scan Results";
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.Location = new System.Drawing.Point(6, 179);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(169, 23);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(6, 19);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(170, 154);
            this.txtResults.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 223);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(652, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 245);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpScanResults);
            this.Controls.Add(this.grpScanSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Simple Process Memory Scanner";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpScanSettings.ResumeLayout(false);
            this.grpScanSettings.PerformLayout();
            this.grpScanResults.ResumeLayout(false);
            this.grpScanResults.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpScanSettings;
        private System.Windows.Forms.Label lblMask;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.GroupBox grpScanResults;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

