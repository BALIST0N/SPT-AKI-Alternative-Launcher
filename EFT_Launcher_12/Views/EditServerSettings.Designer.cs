namespace EFT_Launcher_12.Views
{
    partial class EditServerSettings
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
            this.backenUrlLabel = new System.Windows.Forms.Label();
            this.backendURLTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.PortNumericBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumericBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // backenUrlLabel
            // 
            this.backenUrlLabel.AutoSize = true;
            this.backenUrlLabel.BackColor = System.Drawing.Color.Transparent;
            this.backenUrlLabel.Font = new System.Drawing.Font("Candara", 11.25F);
            this.backenUrlLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.backenUrlLabel.Location = new System.Drawing.Point(12, 9);
            this.backenUrlLabel.Name = "backenUrlLabel";
            this.backenUrlLabel.Size = new System.Drawing.Size(101, 18);
            this.backenUrlLabel.TabIndex = 1;
            this.backenUrlLabel.Text = "Backend URL : ";
            // 
            // backendURLTextBox
            // 
            this.backendURLTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.backendURLTextBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backendURLTextBox.ForeColor = System.Drawing.Color.White;
            this.backendURLTextBox.Location = new System.Drawing.Point(119, 6);
            this.backendURLTextBox.Name = "backendURLTextBox";
            this.backendURLTextBox.Size = new System.Drawing.Size(163, 26);
            this.backendURLTextBox.TabIndex = 3;
            this.backendURLTextBox.TextChanged += new System.EventHandler(this.backendURLTextBox_TextChanged);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.BackColor = System.Drawing.Color.Transparent;
            this.PortLabel.Font = new System.Drawing.Font("Candara", 11.25F);
            this.PortLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.PortLabel.Location = new System.Drawing.Point(295, 9);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(41, 18);
            this.PortLabel.TabIndex = 5;
            this.PortLabel.Text = "Port :";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.SaveSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveSettingsButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveSettingsButton.ForeColor = System.Drawing.SystemColors.Control;
            this.SaveSettingsButton.Location = new System.Drawing.Point(3, 230);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(479, 38);
            this.SaveSettingsButton.TabIndex = 8;
            this.SaveSettingsButton.Text = "Save Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = false;
            // 
            // PortNumericBox
            // 
            this.PortNumericBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.PortNumericBox.Font = new System.Drawing.Font("Candara", 11.25F);
            this.PortNumericBox.ForeColor = System.Drawing.SystemColors.Control;
            this.PortNumericBox.Location = new System.Drawing.Point(342, 7);
            this.PortNumericBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PortNumericBox.Name = "PortNumericBox";
            this.PortNumericBox.Size = new System.Drawing.Size(76, 26);
            this.PortNumericBox.TabIndex = 9;
            // 
            // EditServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(484, 271);
            this.Controls.Add(this.PortNumericBox);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.backendURLTextBox);
            this.Controls.Add(this.backenUrlLabel);
            this.Controls.Add(this.label1);
            this.Name = "EditServerSettings";
            this.Text = "Edit Server Settings";
            ((System.ComponentModel.ISupportInitialize)(this.PortNumericBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label backenUrlLabel;
        private System.Windows.Forms.TextBox backendURLTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.NumericUpDown PortNumericBox;
    }
}