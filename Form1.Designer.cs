namespace YoutubeTool
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChooseSaveFolderBtn = new System.Windows.Forms.Button();
            this.CheckRootLinkBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveFolderLinkTBox = new System.Windows.Forms.TextBox();
            this.LogDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.RootLinkTBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DownloadThreadUD = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.RunPauseBtn = new System.Windows.Forms.Button();
            this.ThumnailsBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tick = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Root = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThumNail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tíchChọnTấtCảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bỏTíchTấtCảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đảoNgượcVùngChọnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xóaToànBộDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SoLuotCuonUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogDataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadThreadUD)).BeginInit();
            this.tickMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SoLuotCuonUD)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ChooseSaveFolderBtn);
            this.groupBox1.Controls.Add(this.CheckRootLinkBtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.SaveFolderLinkTBox);
            this.groupBox1.Controls.Add(this.LogDataGridView);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RootLinkTBox);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 645);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ChooseSaveFolderBtn
            // 
            this.ChooseSaveFolderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseSaveFolderBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChooseSaveFolderBtn.Location = new System.Drawing.Point(711, 43);
            this.ChooseSaveFolderBtn.Name = "ChooseSaveFolderBtn";
            this.ChooseSaveFolderBtn.Size = new System.Drawing.Size(163, 23);
            this.ChooseSaveFolderBtn.TabIndex = 7;
            this.ChooseSaveFolderBtn.Text = "CHOOSE";
            this.ChooseSaveFolderBtn.UseVisualStyleBackColor = true;
            this.ChooseSaveFolderBtn.Click += new System.EventHandler(this.ChooseSaveFolderBtn_Click);
            // 
            // CheckRootLinkBtn
            // 
            this.CheckRootLinkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckRootLinkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckRootLinkBtn.Location = new System.Drawing.Point(711, 19);
            this.CheckRootLinkBtn.Name = "CheckRootLinkBtn";
            this.CheckRootLinkBtn.Size = new System.Drawing.Size(163, 23);
            this.CheckRootLinkBtn.TabIndex = 6;
            this.CheckRootLinkBtn.Text = "CHECK";
            this.CheckRootLinkBtn.UseVisualStyleBackColor = true;
            this.CheckRootLinkBtn.Click += new System.EventHandler(this.CheckRootLinkBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SAVE FOLDER";
            // 
            // SaveFolderLinkTBox
            // 
            this.SaveFolderLinkTBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFolderLinkTBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SaveFolderLinkTBox.Enabled = false;
            this.SaveFolderLinkTBox.Location = new System.Drawing.Point(90, 45);
            this.SaveFolderLinkTBox.Name = "SaveFolderLinkTBox";
            this.SaveFolderLinkTBox.Size = new System.Drawing.Size(618, 20);
            this.SaveFolderLinkTBox.TabIndex = 4;
            // 
            // LogDataGridView
            // 
            this.LogDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.Tick,
            this.Root,
            this.Url,
            this.ThumNail,
            this.Status});
            this.LogDataGridView.Location = new System.Drawing.Point(6, 71);
            this.LogDataGridView.Name = "LogDataGridView";
            this.LogDataGridView.Size = new System.Drawing.Size(868, 574);
            this.LogDataGridView.TabIndex = 3;
            this.LogDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.LogDataGridView_CellMouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ROOT LINK";
            // 
            // RootLinkTBox
            // 
            this.RootLinkTBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RootLinkTBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RootLinkTBox.Location = new System.Drawing.Point(90, 19);
            this.RootLinkTBox.Multiline = true;
            this.RootLinkTBox.Name = "RootLinkTBox";
            this.RootLinkTBox.Size = new System.Drawing.Size(618, 20);
            this.RootLinkTBox.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1239, 683);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1231, 657);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DOWNLOAD";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SoLuotCuonUD);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.DownloadThreadUD);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listBoxLog);
            this.groupBox2.Controls.Add(this.RunPauseBtn);
            this.groupBox2.Controls.Add(this.ThumnailsBtn);
            this.groupBox2.Location = new System.Drawing.Point(892, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 645);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // DownloadThreadUD
            // 
            this.DownloadThreadUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DownloadThreadUD.Location = new System.Drawing.Point(109, 574);
            this.DownloadThreadUD.Name = "DownloadThreadUD";
            this.DownloadThreadUD.Size = new System.Drawing.Size(56, 20);
            this.DownloadThreadUD.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 576);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Số luồng download";
            // 
            // listBoxLog
            // 
            this.listBoxLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.listBoxLog.ForeColor = System.Drawing.SystemColors.Info;
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.Location = new System.Drawing.Point(4, 5);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(326, 524);
            this.listBoxLog.TabIndex = 3;
            // 
            // RunPauseBtn
            // 
            this.RunPauseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RunPauseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RunPauseBtn.Location = new System.Drawing.Point(4, 598);
            this.RunPauseBtn.Name = "RunPauseBtn";
            this.RunPauseBtn.Size = new System.Drawing.Size(162, 41);
            this.RunPauseBtn.TabIndex = 1;
            this.RunPauseBtn.Text = "RUN";
            this.RunPauseBtn.UseVisualStyleBackColor = true;
            this.RunPauseBtn.Click += new System.EventHandler(this.RunPauseBtn_Click);
            // 
            // ThumnailsBtn
            // 
            this.ThumnailsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ThumnailsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ThumnailsBtn.Location = new System.Drawing.Point(168, 598);
            this.ThumnailsBtn.Name = "ThumnailsBtn";
            this.ThumnailsBtn.Size = new System.Drawing.Size(162, 41);
            this.ThumnailsBtn.TabIndex = 0;
            this.ThumnailsBtn.Text = "RUN THUMNAIL";
            this.ThumnailsBtn.UseVisualStyleBackColor = true;
            this.ThumnailsBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1329, 801);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            // 
            // Tick
            // 
            this.Tick.HeaderText = "Tick";
            this.Tick.Name = "Tick";
            this.Tick.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Tick.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Tick.Width = 60;
            // 
            // Root
            // 
            this.Root.HeaderText = "Nguon";
            this.Root.Name = "Root";
            this.Root.Width = 60;
            // 
            // Url
            // 
            this.Url.HeaderText = "Url";
            this.Url.Name = "Url";
            this.Url.Width = 300;
            // 
            // ThumNail
            // 
            this.ThumNail.HeaderText = "ThumNail";
            this.ThumNail.Name = "ThumNail";
            this.ThumNail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ThumNail.Width = 300;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 200;
            // 
            // tickMenu
            // 
            this.tickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tíchChọnTấtCảToolStripMenuItem,
            this.bỏTíchTấtCảToolStripMenuItem,
            this.đảoNgượcVùngChọnToolStripMenuItem,
            this.xóaToànBộDataToolStripMenuItem});
            this.tickMenu.Name = "tickMenu";
            this.tickMenu.Size = new System.Drawing.Size(193, 92);
            // 
            // tíchChọnTấtCảToolStripMenuItem
            // 
            this.tíchChọnTấtCảToolStripMenuItem.Name = "tíchChọnTấtCảToolStripMenuItem";
            this.tíchChọnTấtCảToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.tíchChọnTấtCảToolStripMenuItem.Text = "Tích chọn tất cả";
            this.tíchChọnTấtCảToolStripMenuItem.Click += new System.EventHandler(this.tíchChọnTấtCảToolStripMenuItem_Click);
            // 
            // bỏTíchTấtCảToolStripMenuItem
            // 
            this.bỏTíchTấtCảToolStripMenuItem.Name = "bỏTíchTấtCảToolStripMenuItem";
            this.bỏTíchTấtCảToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.bỏTíchTấtCảToolStripMenuItem.Text = "Bỏ tích tất cả";
            this.bỏTíchTấtCảToolStripMenuItem.Click += new System.EventHandler(this.bỏTíchTấtCảToolStripMenuItem_Click);
            // 
            // đảoNgượcVùngChọnToolStripMenuItem
            // 
            this.đảoNgượcVùngChọnToolStripMenuItem.Name = "đảoNgượcVùngChọnToolStripMenuItem";
            this.đảoNgượcVùngChọnToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.đảoNgượcVùngChọnToolStripMenuItem.Text = "Đảo ngược vùng chọn";
            this.đảoNgượcVùngChọnToolStripMenuItem.Click += new System.EventHandler(this.đảoNgượcVùngChọnToolStripMenuItem_Click);
            // 
            // xóaToànBộDataToolStripMenuItem
            // 
            this.xóaToànBộDataToolStripMenuItem.Name = "xóaToànBộDataToolStripMenuItem";
            this.xóaToànBộDataToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.xóaToànBộDataToolStripMenuItem.Text = "Xóa toàn bộ data";
            this.xóaToànBộDataToolStripMenuItem.Click += new System.EventHandler(this.xóaToànBộDataToolStripMenuItem_Click);
            // 
            // SoLuotCuonUD
            // 
            this.SoLuotCuonUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SoLuotCuonUD.Location = new System.Drawing.Point(109, 550);
            this.SoLuotCuonUD.Name = "SoLuotCuonUD";
            this.SoLuotCuonUD.Size = new System.Drawing.Size(56, 20);
            this.SoLuotCuonUD.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 552);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Số lượt cuộn ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 707);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Youtube Tool ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogDataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadThreadUD)).EndInit();
            this.tickMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SoLuotCuonUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox RootLinkTBox;
        private System.Windows.Forms.DataGridView LogDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ChooseSaveFolderBtn;
        private System.Windows.Forms.Button CheckRootLinkBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SaveFolderLinkTBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button RunPauseBtn;
        private System.Windows.Forms.Button ThumnailsBtn;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.NumericUpDown DownloadThreadUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Tick;
        private System.Windows.Forms.DataGridViewTextBoxColumn Root;
        private System.Windows.Forms.DataGridViewTextBoxColumn Url;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThumNail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.ContextMenuStrip tickMenu;
        private System.Windows.Forms.ToolStripMenuItem tíchChọnTấtCảToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bỏTíchTấtCảToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đảoNgượcVùngChọnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xóaToànBộDataToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown SoLuotCuonUD;
        private System.Windows.Forms.Label label4;
    }
}

