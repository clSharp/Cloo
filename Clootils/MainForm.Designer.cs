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
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.compilerTab = new System.Windows.Forms.TabPage();
            this.compilerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.compilerMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildProgramMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontSettngsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorFontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilerStatusStrip = new System.Windows.Forms.StatusStrip();
            this.compilerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.editorTextBox = new System.Windows.Forms.TextBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.infoTab = new System.Windows.Forms.TabPage();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.infoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.copyFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.copyButton = new System.Windows.Forms.Button();
            this.infoFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.infoButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.tabControl.SuspendLayout();
            this.compilerTab.SuspendLayout();
            this.compilerTableLayoutPanel.SuspendLayout();
            this.compilerMenuStrip.SuspendLayout();
            this.compilerSplitContainer.Panel1.SuspendLayout();
            this.compilerSplitContainer.Panel2.SuspendLayout();
            this.compilerSplitContainer.SuspendLayout();
            this.infoTab.SuspendLayout();
            this.infoTableLayoutPanel.SuspendLayout();
            this.copyFlowLayoutPanel.SuspendLayout();
            this.infoFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add( this.compilerTab );
            this.tabControl.Controls.Add( this.infoTab );
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point( 0, 0 );
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size( 792, 566 );
            this.tabControl.TabIndex = 0;
            // 
            // compilerTab
            // 
            this.compilerTab.Controls.Add( this.compilerTableLayoutPanel );
            this.compilerTab.Location = new System.Drawing.Point( 4, 22 );
            this.compilerTab.Name = "compilerTab";
            this.compilerTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.compilerTab.Size = new System.Drawing.Size( 784, 540 );
            this.compilerTab.TabIndex = 0;
            this.compilerTab.Text = "Compiler";
            this.compilerTab.UseVisualStyleBackColor = true;
            // 
            // compilerTableLayoutPanel
            // 
            this.compilerTableLayoutPanel.AutoSize = true;
            this.compilerTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.compilerTableLayoutPanel.ColumnCount = 1;
            this.compilerTableLayoutPanel.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.compilerTableLayoutPanel.Controls.Add( this.compilerMenuStrip, 0, 0 );
            this.compilerTableLayoutPanel.Controls.Add( this.compilerStatusStrip, 0, 2 );
            this.compilerTableLayoutPanel.Controls.Add( this.compilerSplitContainer, 0, 1 );
            this.compilerTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compilerTableLayoutPanel.Location = new System.Drawing.Point( 3, 3 );
            this.compilerTableLayoutPanel.Name = "compilerTableLayoutPanel";
            this.compilerTableLayoutPanel.RowCount = 3;
            this.compilerTableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.compilerTableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.compilerTableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.compilerTableLayoutPanel.Size = new System.Drawing.Size( 778, 534 );
            this.compilerTableLayoutPanel.TabIndex = 0;
            // 
            // compilerMenuStrip
            // 
            this.compilerMenuStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.buildMenuItem,
            this.optionsToolStripMenuItem} );
            this.compilerMenuStrip.Location = new System.Drawing.Point( 0, 0 );
            this.compilerMenuStrip.Name = "compilerMenuStrip";
            this.compilerMenuStrip.Size = new System.Drawing.Size( 778, 24 );
            this.compilerMenuStrip.TabIndex = 0;
            this.compilerMenuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenuItem,
            this.saveFileMenuItem} );
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size( 35, 20 );
            this.fileMenuItem.Text = "File";
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O ) ) );
            this.openFileMenuItem.Size = new System.Drawing.Size( 163, 22 );
            this.openFileMenuItem.Text = "Open...";
            this.openFileMenuItem.Click += new System.EventHandler( this.openFileMenuItem_Click );
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.ShortcutKeys = ( ( System.Windows.Forms.Keys )( ( System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S ) ) );
            this.saveFileMenuItem.Size = new System.Drawing.Size( 163, 22 );
            this.saveFileMenuItem.Text = "Save...";
            this.saveFileMenuItem.Click += new System.EventHandler( this.saveFileMenuItem_Click );
            // 
            // buildMenuItem
            // 
            this.buildMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.buildProgramMenuItem,
            this.buildConfigMenuItem} );
            this.buildMenuItem.Name = "buildMenuItem";
            this.buildMenuItem.Size = new System.Drawing.Size( 41, 20 );
            this.buildMenuItem.Text = "Build";
            // 
            // buildProgramMenuItem
            // 
            this.buildProgramMenuItem.Name = "buildProgramMenuItem";
            this.buildProgramMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.buildProgramMenuItem.Size = new System.Drawing.Size( 162, 22 );
            this.buildProgramMenuItem.Text = "Program";
            this.buildProgramMenuItem.Click += new System.EventHandler( this.buildProgramMenuItem_Click );
            // 
            // buildConfigMenuItem
            // 
            this.buildConfigMenuItem.Name = "buildConfigMenuItem";
            this.buildConfigMenuItem.Size = new System.Drawing.Size( 162, 22 );
            this.buildConfigMenuItem.Text = "Configuration...";
            this.buildConfigMenuItem.Click += new System.EventHandler( this.buildDeviceMenuItem_Click );
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fontSettngsMenuItem} );
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size( 56, 20 );
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // fontSettngsMenuItem
            // 
            this.fontSettngsMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.editorFontMenuItem,
            this.logFontMenuItem} );
            this.fontSettngsMenuItem.Name = "fontSettngsMenuItem";
            this.fontSettngsMenuItem.Size = new System.Drawing.Size( 152, 22 );
            this.fontSettngsMenuItem.Text = "Fonts";
            // 
            // editorFontMenuItem
            // 
            this.editorFontMenuItem.Name = "editorFontMenuItem";
            this.editorFontMenuItem.Size = new System.Drawing.Size( 125, 22 );
            this.editorFontMenuItem.Text = "Editor...";
            this.editorFontMenuItem.Click += new System.EventHandler( this.editorFontMenuItem_Click );
            // 
            // logFontMenuItem
            // 
            this.logFontMenuItem.Name = "logFontMenuItem";
            this.logFontMenuItem.Size = new System.Drawing.Size( 125, 22 );
            this.logFontMenuItem.Text = "Log...";
            this.logFontMenuItem.Click += new System.EventHandler( this.logFontMenuItem_Click );
            // 
            // compilerStatusStrip
            // 
            this.compilerStatusStrip.Location = new System.Drawing.Point( 0, 512 );
            this.compilerStatusStrip.Name = "compilerStatusStrip";
            this.compilerStatusStrip.Size = new System.Drawing.Size( 778, 22 );
            this.compilerStatusStrip.TabIndex = 1;
            this.compilerStatusStrip.Text = "statusStrip1";
            // 
            // compilerSplitContainer
            // 
            this.compilerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compilerSplitContainer.Location = new System.Drawing.Point( 3, 27 );
            this.compilerSplitContainer.Name = "compilerSplitContainer";
            this.compilerSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // compilerSplitContainer.Panel1
            // 
            this.compilerSplitContainer.Panel1.Controls.Add( this.editorTextBox );
            // 
            // compilerSplitContainer.Panel2
            // 
            this.compilerSplitContainer.Panel2.Controls.Add( this.logTextBox );
            this.compilerSplitContainer.Size = new System.Drawing.Size( 772, 482 );
            this.compilerSplitContainer.SplitterDistance = 364;
            this.compilerSplitContainer.TabIndex = 2;
            // 
            // editorTextBox
            // 
            this.editorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorTextBox.Location = new System.Drawing.Point( 0, 0 );
            this.editorTextBox.Multiline = true;
            this.editorTextBox.Name = "editorTextBox";
            this.editorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.editorTextBox.Size = new System.Drawing.Size( 772, 364 );
            this.editorTextBox.TabIndex = 0;
            this.editorTextBox.WordWrap = false;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point( 0, 0 );
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size( 772, 114 );
            this.logTextBox.TabIndex = 1;
            this.logTextBox.WordWrap = false;
            // 
            // infoTab
            // 
            this.infoTab.Controls.Add( this.infoTextBox );
            this.infoTab.Controls.Add( this.infoTableLayoutPanel );
            this.infoTab.Location = new System.Drawing.Point( 4, 22 );
            this.infoTab.Name = "infoTab";
            this.infoTab.Padding = new System.Windows.Forms.Padding( 3 );
            this.infoTab.Size = new System.Drawing.Size( 784, 540 );
            this.infoTab.TabIndex = 1;
            this.infoTab.Text = "InfoView";
            this.infoTab.UseVisualStyleBackColor = true;
            // 
            // infoTextBox
            // 
            this.infoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoTextBox.Location = new System.Drawing.Point( 3, 3 );
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.ReadOnly = true;
            this.infoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.infoTextBox.Size = new System.Drawing.Size( 778, 499 );
            this.infoTextBox.TabIndex = 5;
            this.infoTextBox.WordWrap = false;
            // 
            // infoTableLayoutPanel
            // 
            this.infoTableLayoutPanel.AutoSize = true;
            this.infoTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.infoTableLayoutPanel.ColumnCount = 2;
            this.infoTableLayoutPanel.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.infoTableLayoutPanel.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.infoTableLayoutPanel.Controls.Add( this.copyFlowLayoutPanel, 1, 0 );
            this.infoTableLayoutPanel.Controls.Add( this.infoFlowLayoutPanel, 0, 0 );
            this.infoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoTableLayoutPanel.Location = new System.Drawing.Point( 3, 502 );
            this.infoTableLayoutPanel.Name = "infoTableLayoutPanel";
            this.infoTableLayoutPanel.RowCount = 1;
            this.infoTableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.infoTableLayoutPanel.Size = new System.Drawing.Size( 778, 35 );
            this.infoTableLayoutPanel.TabIndex = 4;
            // 
            // copyFlowLayoutPanel
            // 
            this.copyFlowLayoutPanel.AutoSize = true;
            this.copyFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.copyFlowLayoutPanel.Controls.Add( this.copyButton );
            this.copyFlowLayoutPanel.Location = new System.Drawing.Point( 665, 3 );
            this.copyFlowLayoutPanel.Name = "copyFlowLayoutPanel";
            this.copyFlowLayoutPanel.Size = new System.Drawing.Size( 110, 29 );
            this.copyFlowLayoutPanel.TabIndex = 5;
            // 
            // copyButton
            // 
            this.copyButton.AutoSize = true;
            this.copyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.copyButton.Location = new System.Drawing.Point( 3, 3 );
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size( 104, 23 );
            this.copyButton.TabIndex = 2;
            this.copyButton.Text = "Copy To Clipboard";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler( this.copyButton_Click );
            // 
            // infoFlowLayoutPanel
            // 
            this.infoFlowLayoutPanel.AutoSize = true;
            this.infoFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.infoFlowLayoutPanel.Controls.Add( this.infoButton );
            this.infoFlowLayoutPanel.Controls.Add( this.testButton );
            this.infoFlowLayoutPanel.Location = new System.Drawing.Point( 3, 3 );
            this.infoFlowLayoutPanel.Name = "infoFlowLayoutPanel";
            this.infoFlowLayoutPanel.Size = new System.Drawing.Size( 186, 29 );
            this.infoFlowLayoutPanel.TabIndex = 0;
            // 
            // infoButton
            // 
            this.infoButton.AutoSize = true;
            this.infoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.infoButton.Location = new System.Drawing.Point( 3, 3 );
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size( 108, 23 );
            this.infoButton.TabIndex = 1;
            this.infoButton.Text = "Query OpenCL Info";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler( this.infoButton_Click );
            // 
            // testButton
            // 
            this.testButton.AutoSize = true;
            this.testButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.testButton.Location = new System.Drawing.Point( 117, 3 );
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size( 66, 23 );
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Run Tests";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler( this.testButton_Click );
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 792, 566 );
            this.Controls.Add( this.tabControl );
            this.MainMenuStrip = this.compilerMenuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clootils";
            this.tabControl.ResumeLayout( false );
            this.compilerTab.ResumeLayout( false );
            this.compilerTab.PerformLayout();
            this.compilerTableLayoutPanel.ResumeLayout( false );
            this.compilerTableLayoutPanel.PerformLayout();
            this.compilerMenuStrip.ResumeLayout( false );
            this.compilerMenuStrip.PerformLayout();
            this.compilerSplitContainer.Panel1.ResumeLayout( false );
            this.compilerSplitContainer.Panel1.PerformLayout();
            this.compilerSplitContainer.Panel2.ResumeLayout( false );
            this.compilerSplitContainer.Panel2.PerformLayout();
            this.compilerSplitContainer.ResumeLayout( false );
            this.infoTab.ResumeLayout( false );
            this.infoTab.PerformLayout();
            this.infoTableLayoutPanel.ResumeLayout( false );
            this.infoTableLayoutPanel.PerformLayout();
            this.copyFlowLayoutPanel.ResumeLayout( false );
            this.copyFlowLayoutPanel.PerformLayout();
            this.infoFlowLayoutPanel.ResumeLayout( false );
            this.infoFlowLayoutPanel.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage compilerTab;
        private System.Windows.Forms.TableLayoutPanel compilerTableLayoutPanel;
        private System.Windows.Forms.TabPage infoTab;
        private System.Windows.Forms.MenuStrip compilerMenuStrip;
        private System.Windows.Forms.StatusStrip compilerStatusStrip;
        private System.Windows.Forms.SplitContainer compilerSplitContainer;
        private System.Windows.Forms.TextBox editorTextBox;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildProgramMenuItem;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.TableLayoutPanel infoTableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel infoFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel copyFlowLayoutPanel;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontSettngsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorFontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logFontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildConfigMenuItem;

    }
}