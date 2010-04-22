namespace Clootils
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildProgramMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontSettngsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorFontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runTestsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.editorTextBox = new System.Windows.Forms.TextBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.tableLayoutPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.menuStrip, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.statusStrip, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.splitContainer, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(792, 566);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.buildMenuItem,
            this.optionsMenuItem,
            this.openCLMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenuItem,
            this.saveFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openFileMenuItem.Text = "Open...";
            this.openFileMenuItem.Click += new System.EventHandler(this.openFileMenuItem_Click);
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFileMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveFileMenuItem.Text = "Save...";
            this.saveFileMenuItem.Click += new System.EventHandler(this.saveFileMenuItem_Click);
            // 
            // buildMenuItem
            // 
            this.buildMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildProgramMenuItem,
            this.buildConfigMenuItem});
            this.buildMenuItem.Name = "buildMenuItem";
            this.buildMenuItem.Size = new System.Drawing.Size(46, 20);
            this.buildMenuItem.Text = "Build";
            // 
            // buildProgramMenuItem
            // 
            this.buildProgramMenuItem.Name = "buildProgramMenuItem";
            this.buildProgramMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.buildProgramMenuItem.Size = new System.Drawing.Size(157, 22);
            this.buildProgramMenuItem.Text = "Program";
            this.buildProgramMenuItem.Click += new System.EventHandler(this.buildProgramMenuItem_Click);
            // 
            // buildConfigMenuItem
            // 
            this.buildConfigMenuItem.Name = "buildConfigMenuItem";
            this.buildConfigMenuItem.Size = new System.Drawing.Size(157, 22);
            this.buildConfigMenuItem.Text = "Configuration...";
            this.buildConfigMenuItem.Click += new System.EventHandler(this.buildDeviceMenuItem_Click);
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontSettngsMenuItem});
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsMenuItem.Text = "Options";
            // 
            // fontSettngsMenuItem
            // 
            this.fontSettngsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorFontMenuItem,
            this.logFontMenuItem});
            this.fontSettngsMenuItem.Name = "fontSettngsMenuItem";
            this.fontSettngsMenuItem.Size = new System.Drawing.Size(103, 22);
            this.fontSettngsMenuItem.Text = "Fonts";
            // 
            // editorFontMenuItem
            // 
            this.editorFontMenuItem.Name = "editorFontMenuItem";
            this.editorFontMenuItem.Size = new System.Drawing.Size(114, 22);
            this.editorFontMenuItem.Text = "Editor...";
            this.editorFontMenuItem.Click += new System.EventHandler(this.editorFontMenuItem_Click);
            // 
            // logFontMenuItem
            // 
            this.logFontMenuItem.Name = "logFontMenuItem";
            this.logFontMenuItem.Size = new System.Drawing.Size(114, 22);
            this.logFontMenuItem.Text = "Log...";
            this.logFontMenuItem.Click += new System.EventHandler(this.logFontMenuItem_Click);
            // 
            // openCLMenuItem
            // 
            this.openCLMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInfoMenuItem,
            this.runTestsMenuItem});
            this.openCLMenuItem.Name = "openCLMenuItem";
            this.openCLMenuItem.Size = new System.Drawing.Size(62, 20);
            this.openCLMenuItem.Text = "OpenCL";
            // 
            // showInfoMenuItem
            // 
            this.showInfoMenuItem.Name = "showInfoMenuItem";
            this.showInfoMenuItem.Size = new System.Drawing.Size(127, 22);
            this.showInfoMenuItem.Text = "Show Info";
            this.showInfoMenuItem.Click += new System.EventHandler(this.showInfoToolStripMenuItem_Click);
            // 
            // runTestsMenuItem
            // 
            this.runTestsMenuItem.Name = "runTestsMenuItem";
            this.runTestsMenuItem.Size = new System.Drawing.Size(127, 22);
            this.runTestsMenuItem.Text = "Run Tests";
            this.runTestsMenuItem.Click += new System.EventHandler(this.runTestsToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 544);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 27);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.editorTextBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.logTextBox);
            this.splitContainer.Size = new System.Drawing.Size(786, 514);
            this.splitContainer.SplitterDistance = 380;
            this.splitContainer.TabIndex = 2;
            // 
            // editorTextBox
            // 
            this.editorTextBox.AcceptsTab = true;
            this.editorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorTextBox.Location = new System.Drawing.Point(0, 0);
            this.editorTextBox.Multiline = true;
            this.editorTextBox.Name = "editorTextBox";
            this.editorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.editorTextBox.Size = new System.Drawing.Size(786, 380);
            this.editorTextBox.TabIndex = 0;
            this.editorTextBox.WordWrap = false;
            // 
            // logTextBox
            // 
            this.logTextBox.AcceptsTab = true;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(786, 130);
            this.logTextBox.TabIndex = 1;
            this.logTextBox.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clootils";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildProgramMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontSettngsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorFontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logFontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildConfigMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCLMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInfoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runTestsMenuItem;
        private System.Windows.Forms.TextBox editorTextBox;
        private System.Windows.Forms.TextBox logTextBox;

    }
}