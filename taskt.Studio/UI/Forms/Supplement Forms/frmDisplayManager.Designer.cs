﻿namespace taskt.UI.Forms.Supplement_Forms
{
    partial class frmDisplayManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplayManager));
            this.lstEventLogs = new System.Windows.Forms.ListBox();
            this.dgvMachines = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAddMachine = new System.Windows.Forms.Button();
            this.tmrCheck = new System.Windows.Forms.Timer(this.components);
            this.tlpDisplayManager = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblResolution = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.chkStartMinimized = new System.Windows.Forms.CheckBox();
            this.chkHideScreen = new System.Windows.Forms.CheckBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblScheduledScripts = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachines)).BeginInit();
            this.tlpDisplayManager.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstEventLogs
            // 
            this.tlpDisplayManager.SetColumnSpan(this.lstEventLogs, 2);
            this.lstEventLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEventLogs.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEventLogs.FormattingEnabled = true;
            this.lstEventLogs.ItemHeight = 15;
            this.lstEventLogs.Location = new System.Drawing.Point(3, 411);
            this.lstEventLogs.Name = "lstEventLogs";
            this.lstEventLogs.Size = new System.Drawing.Size(1011, 240);
            this.lstEventLogs.TabIndex = 0;
            // 
            // dgvMachines
            // 
            this.dgvMachines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMachines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMachines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpDisplayManager.SetColumnSpan(this.dgvMachines, 2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMachines.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMachines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMachines.Location = new System.Drawing.Point(3, 75);
            this.dgvMachines.Name = "dgvMachines";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMachines.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMachines.Size = new System.Drawing.Size(1011, 290);
            this.dgvMachines.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(10, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 27);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(90, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 27);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAddMachine
            // 
            this.btnAddMachine.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMachine.Location = new System.Drawing.Point(240, 12);
            this.btnAddMachine.Name = "btnAddMachine";
            this.btnAddMachine.Size = new System.Drawing.Size(126, 27);
            this.btnAddMachine.TabIndex = 4;
            this.btnAddMachine.Text = "Add Machine";
            this.btnAddMachine.UseVisualStyleBackColor = true;
            this.btnAddMachine.Click += new System.EventHandler(this.btnAddMachine_Click);
            // 
            // tmrCheck
            // 
            this.tmrCheck.Interval = 5000;
            this.tmrCheck.Tick += new System.EventHandler(this.tmrCheck_Tick);
            // 
            // tlpDisplayManager
            // 
            this.tlpDisplayManager.BackColor = System.Drawing.Color.DimGray;
            this.tlpDisplayManager.ColumnCount = 2;
            this.tlpDisplayManager.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDisplayManager.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDisplayManager.Controls.Add(this.pnlBottom, 0, 2);
            this.tlpDisplayManager.Controls.Add(this.dgvMachines, 0, 1);
            this.tlpDisplayManager.Controls.Add(this.lstEventLogs, 0, 3);
            this.tlpDisplayManager.Controls.Add(this.pnlTop, 0, 0);
            this.tlpDisplayManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDisplayManager.Location = new System.Drawing.Point(0, 0);
            this.tlpDisplayManager.Name = "tlpDisplayManager";
            this.tlpDisplayManager.RowCount = 4;
            this.tlpDisplayManager.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.52862F));
            this.tlpDisplayManager.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.47138F));
            this.tlpDisplayManager.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDisplayManager.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 245F));
            this.tlpDisplayManager.Size = new System.Drawing.Size(1017, 654);
            this.tlpDisplayManager.TabIndex = 5;
            // 
            // pnlBottom
            // 
            this.tlpDisplayManager.SetColumnSpan(this.pnlBottom, 2);
            this.pnlBottom.Controls.Add(this.lblResolution);
            this.pnlBottom.Controls.Add(this.txtHeight);
            this.pnlBottom.Controls.Add(this.txtWidth);
            this.pnlBottom.Controls.Add(this.chkStartMinimized);
            this.pnlBottom.Controls.Add(this.chkHideScreen);
            this.pnlBottom.Controls.Add(this.btnStart);
            this.pnlBottom.Controls.Add(this.btnStop);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(3, 371);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1011, 34);
            this.pnlBottom.TabIndex = 6;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResolution.ForeColor = System.Drawing.Color.White;
            this.lblResolution.Location = new System.Drawing.Point(427, 9);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(123, 15);
            this.lblResolution.TabIndex = 9;
            this.lblResolution.Text = "Desktop Window Size:";
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(590, 6);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(32, 22);
            this.txtHeight.TabIndex = 8;
            this.txtHeight.Text = "1080";
            // 
            // txtWidth
            // 
            this.txtWidth.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidth.Location = new System.Drawing.Point(552, 6);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(32, 22);
            this.txtWidth.TabIndex = 7;
            this.txtWidth.Text = "1920";
            // 
            // chkStartMinimized
            // 
            this.chkStartMinimized.AutoSize = true;
            this.chkStartMinimized.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStartMinimized.ForeColor = System.Drawing.Color.White;
            this.chkStartMinimized.Location = new System.Drawing.Point(274, 6);
            this.chkStartMinimized.Name = "chkStartMinimized";
            this.chkStartMinimized.Size = new System.Drawing.Size(118, 21);
            this.chkStartMinimized.TabIndex = 6;
            this.chkStartMinimized.Text = "Start Minimized";
            this.chkStartMinimized.UseVisualStyleBackColor = true;
            // 
            // chkHideScreen
            // 
            this.chkHideScreen.AutoSize = true;
            this.chkHideScreen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHideScreen.ForeColor = System.Drawing.Color.White;
            this.chkHideScreen.Location = new System.Drawing.Point(171, 6);
            this.chkHideScreen.Name = "chkHideScreen";
            this.chkHideScreen.Size = new System.Drawing.Size(97, 21);
            this.chkHideScreen.TabIndex = 5;
            this.chkHideScreen.Text = "Hide Screen";
            this.chkHideScreen.UseVisualStyleBackColor = true;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.tlpDisplayManager.SetColumnSpan(this.pnlTop, 2);
            this.pnlTop.Controls.Add(this.lblScheduledScripts);
            this.pnlTop.Controls.Add(this.lblHeader);
            this.pnlTop.Controls.Add(this.btnAddMachine);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1017, 72);
            this.pnlTop.TabIndex = 7;
            // 
            // lblScheduledScripts
            // 
            this.lblScheduledScripts.AutoSize = true;
            this.lblScheduledScripts.BackColor = System.Drawing.Color.Transparent;
            this.lblScheduledScripts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScheduledScripts.ForeColor = System.Drawing.Color.White;
            this.lblScheduledScripts.Location = new System.Drawing.Point(6, 44);
            this.lblScheduledScripts.Name = "lblScheduledScripts";
            this.lblScheduledScripts.Size = new System.Drawing.Size(598, 21);
            this.lblScheduledScripts.TabIndex = 7;
            this.lblScheduledScripts.Text = "Automatically invoke remote desktop connections which keep virtual desktops activ" +
    "e.";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(3, 4);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(231, 40);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Display Manager";
            // 
            // frmDisplayManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundChangeIndex = 3;
            this.ClientSize = new System.Drawing.Size(1017, 654);
            this.Controls.Add(this.tlpDisplayManager);
            this.Icon = taskt.Core.Properties.Resources.openbots_ico;
            this.Name = "frmDisplayManager";
            this.Text = "Display Manager for VMs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmDisplayManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachines)).EndInit();
            this.tlpDisplayManager.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstEventLogs;
        private System.Windows.Forms.DataGridView dgvMachines;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAddMachine;
        private System.Windows.Forms.Timer tmrCheck;
        private System.Windows.Forms.TableLayoutPanel tlpDisplayManager;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.CheckBox chkHideScreen;
        private System.Windows.Forms.CheckBox chkStartMinimized;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblScheduledScripts;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
    }
}