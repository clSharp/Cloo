namespace Clootils
{
    partial class ConfigForm
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
            this.deviceCheckList = new System.Windows.Forms.CheckedListBox();
            this.deviceLabel = new System.Windows.Forms.Label();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.optionsLabel = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.optionsTextBox = new System.Windows.Forms.TextBox();
            this.platformComboBox = new System.Windows.Forms.ComboBox();
            this.platformLabel = new System.Windows.Forms.Label();
            this.mainLayoutPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceCheckList
            // 
            this.deviceCheckList.FormattingEnabled = true;
            this.deviceCheckList.Location = new System.Drawing.Point(55, 30);
            this.deviceCheckList.Name = "deviceCheckList";
            this.deviceCheckList.Size = new System.Drawing.Size(338, 79);
            this.deviceCheckList.TabIndex = 0;
            // 
            // deviceLabel
            // 
            this.deviceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.deviceLabel.AutoSize = true;
            this.deviceLabel.Location = new System.Drawing.Point(3, 63);
            this.deviceLabel.Name = "deviceLabel";
            this.deviceLabel.Size = new System.Drawing.Size(46, 13);
            this.deviceLabel.TabIndex = 1;
            this.deviceLabel.Text = "Devices";
            this.deviceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.AutoSize = true;
            this.mainLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainLayoutPanel.ColumnCount = 2;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainLayoutPanel.Controls.Add(this.optionsLabel, 0, 2);
            this.mainLayoutPanel.Controls.Add(this.deviceLabel, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.buttonPanel, 1, 3);
            this.mainLayoutPanel.Controls.Add(this.optionsTextBox, 1, 2);
            this.mainLayoutPanel.Controls.Add(this.deviceCheckList, 1, 1);
            this.mainLayoutPanel.Controls.Add(this.platformComboBox, 1, 0);
            this.mainLayoutPanel.Controls.Add(this.platformLabel, 0, 0);
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 4;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayoutPanel.Size = new System.Drawing.Size(396, 173);
            this.mainLayoutPanel.TabIndex = 2;
            // 
            // optionsLabel
            // 
            this.optionsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.optionsLabel.AutoSize = true;
            this.optionsLabel.Location = new System.Drawing.Point(3, 118);
            this.optionsLabel.Name = "optionsLabel";
            this.optionsLabel.Size = new System.Drawing.Size(43, 13);
            this.optionsLabel.TabIndex = 2;
            this.optionsLabel.Text = "Options";
            this.optionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonPanel.AutoSize = true;
            this.buttonPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPanel.Controls.Add(this.okButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Location = new System.Drawing.Point(231, 141);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(162, 29);
            this.buttonPanel.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // optionsTextBox
            // 
            this.optionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsTextBox.Location = new System.Drawing.Point(55, 115);
            this.optionsTextBox.Name = "optionsTextBox";
            this.optionsTextBox.Size = new System.Drawing.Size(338, 20);
            this.optionsTextBox.TabIndex = 3;
            // 
            // platformComboBox
            // 
            this.platformComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.platformComboBox.FormattingEnabled = true;
            this.platformComboBox.Location = new System.Drawing.Point(55, 3);
            this.platformComboBox.Name = "platformComboBox";
            this.platformComboBox.Size = new System.Drawing.Size(338, 21);
            this.platformComboBox.TabIndex = 4;
            // 
            // platformLabel
            // 
            this.platformLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.platformLabel.AutoSize = true;
            this.platformLabel.Location = new System.Drawing.Point(3, 7);
            this.platformLabel.Name = "platformLabel";
            this.platformLabel.Size = new System.Drawing.Size(45, 13);
            this.platformLabel.TabIndex = 5;
            this.platformLabel.Text = "Platform";
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(395, 173);
            this.Controls.Add(this.mainLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Build Configuration";
            this.mainLayoutPanel.ResumeLayout(false);
            this.mainLayoutPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox deviceCheckList;
        private System.Windows.Forms.Label deviceLabel;
        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.Label optionsLabel;
        private System.Windows.Forms.TextBox optionsTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.ComboBox platformComboBox;
        private System.Windows.Forms.Label platformLabel;
        private System.Windows.Forms.Button cancelButton;

    }
}