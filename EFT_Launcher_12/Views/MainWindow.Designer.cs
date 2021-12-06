namespace EFT_Launcher_12
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.profilesListBox = new System.Windows.Forms.ComboBox();
            this.gamePathTextBox = new System.Windows.Forms.TextBox();
            this.profileEditButton = new System.Windows.Forms.Button();
            this.serverOutputRichBox = new System.Windows.Forms.RichTextBox();
            this.backendUrlLabel = new System.Windows.Forms.Label();
            this.killServerButton = new System.Windows.Forms.Button();
            this.startServerChackBox = new System.Windows.Forms.CheckBox();
            this.GameLocationFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.backendUrlTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Tomato;
            this.label1.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game Location";
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.SystemColors.Control;
            this.startButton.Location = new System.Drawing.Point(412, 100);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(160, 49);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // profilesListBox
            // 
            this.profilesListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.profilesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profilesListBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profilesListBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profilesListBox.ForeColor = System.Drawing.SystemColors.Window;
            this.profilesListBox.FormattingEnabled = true;
            this.profilesListBox.Items.AddRange(new object[] {
            "Select a Profile !"});
            this.profilesListBox.Location = new System.Drawing.Point(412, 14);
            this.profilesListBox.Name = "profilesListBox";
            this.profilesListBox.Size = new System.Drawing.Size(160, 26);
            this.profilesListBox.TabIndex = 1;
            // 
            // gamePathTextBox
            // 
            this.gamePathTextBox.AccessibleDescription = "";
            this.gamePathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.gamePathTextBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gamePathTextBox.ForeColor = System.Drawing.Color.White;
            this.gamePathTextBox.Location = new System.Drawing.Point(115, 14);
            this.gamePathTextBox.Name = "gamePathTextBox";
            this.gamePathTextBox.Size = new System.Drawing.Size(199, 26);
            this.gamePathTextBox.TabIndex = 2;
            this.gamePathTextBox.Click += new System.EventHandler(this.gamePathTextBox_Click);
            this.gamePathTextBox.TextChanged += new System.EventHandler(this.gamePathTextBox_TextChanged);
            // 
            // profileEditButton
            // 
            this.profileEditButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.profileEditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profileEditButton.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileEditButton.ForeColor = System.Drawing.SystemColors.Control;
            this.profileEditButton.Location = new System.Drawing.Point(412, 46);
            this.profileEditButton.Name = "profileEditButton";
            this.profileEditButton.Size = new System.Drawing.Size(160, 26);
            this.profileEditButton.TabIndex = 6;
            this.profileEditButton.Text = "Edit Profile";
            this.profileEditButton.UseVisualStyleBackColor = false;
            this.profileEditButton.Click += new System.EventHandler(this.profileEditButton_Click);
            // 
            // serverOutputRichBox
            // 
            this.serverOutputRichBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.serverOutputRichBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serverOutputRichBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverOutputRichBox.ForeColor = System.Drawing.Color.Lime;
            this.serverOutputRichBox.Location = new System.Drawing.Point(0, 162);
            this.serverOutputRichBox.Name = "serverOutputRichBox";
            this.serverOutputRichBox.Size = new System.Drawing.Size(595, 210);
            this.serverOutputRichBox.TabIndex = 7;
            this.serverOutputRichBox.Text = "";
            // 
            // backendUrlLabel
            // 
            this.backendUrlLabel.AutoSize = true;
            this.backendUrlLabel.BackColor = System.Drawing.Color.SandyBrown;
            this.backendUrlLabel.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backendUrlLabel.ForeColor = System.Drawing.Color.Black;
            this.backendUrlLabel.Location = new System.Drawing.Point(12, 125);
            this.backendUrlLabel.Name = "backendUrlLabel";
            this.backendUrlLabel.Size = new System.Drawing.Size(100, 18);
            this.backendUrlLabel.TabIndex = 9;
            this.backendUrlLabel.Text = "backend URL : ";
            // 
            // killServerButton
            // 
            this.killServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.killServerButton.ForeColor = System.Drawing.SystemColors.Control;
            this.killServerButton.Location = new System.Drawing.Point(342, 100);
            this.killServerButton.Name = "killServerButton";
            this.killServerButton.Size = new System.Drawing.Size(64, 49);
            this.killServerButton.TabIndex = 10;
            this.killServerButton.Text = "CLOSE SERVER";
            this.killServerButton.UseVisualStyleBackColor = true;
            this.killServerButton.Visible = false;
            this.killServerButton.Click += new System.EventHandler(this.killServerButton_Click);
            // 
            // startServerChackBox
            // 
            this.startServerChackBox.AutoSize = true;
            this.startServerChackBox.BackColor = System.Drawing.Color.Transparent;
            this.startServerChackBox.ForeColor = System.Drawing.SystemColors.Control;
            this.startServerChackBox.Location = new System.Drawing.Point(412, 77);
            this.startServerChackBox.Name = "startServerChackBox";
            this.startServerChackBox.Size = new System.Drawing.Size(82, 17);
            this.startServerChackBox.TabIndex = 11;
            this.startServerChackBox.Text = "Start Server";
            this.startServerChackBox.UseVisualStyleBackColor = false;
            this.startServerChackBox.Visible = false;
            // 
            // GameLocationFolderBrowser
            // 
            this.GameLocationFolderBrowser.Description = "Select Patched EFT game folder";
            this.GameLocationFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // backendUrlTextBox
            // 
            this.backendUrlTextBox.AccessibleDescription = "";
            this.backendUrlTextBox.BackColor = System.Drawing.Color.SandyBrown;
            this.backendUrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.backendUrlTextBox.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backendUrlTextBox.ForeColor = System.Drawing.Color.Black;
            this.backendUrlTextBox.Location = new System.Drawing.Point(109, 124);
            this.backendUrlTextBox.Name = "backendUrlTextBox";
            this.backendUrlTextBox.Size = new System.Drawing.Size(140, 19);
            this.backendUrlTextBox.TabIndex = 12;
            this.backendUrlTextBox.TextChanged += new System.EventHandler(this.backendUrlTextBox_TextChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(584, 161);
            this.Controls.Add(this.backendUrlTextBox);
            this.Controls.Add(this.startServerChackBox);
            this.Controls.Add(this.killServerButton);
            this.Controls.Add(this.backendUrlLabel);
            this.Controls.Add(this.serverOutputRichBox);
            this.Controls.Add(this.profileEditButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gamePathTextBox);
            this.Controls.Add(this.profilesListBox);
            this.Controls.Add(this.startButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 400);
            this.MinimumSize = new System.Drawing.Size(600, 160);
            this.Name = "MainWindow";
            this.Text = "SPT-AKI Launcher (Alternative Version)";
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.ComboBox profilesListBox;
		private System.Windows.Forms.TextBox gamePathTextBox;
		private System.Windows.Forms.Button profileEditButton;
		private System.Windows.Forms.RichTextBox serverOutputRichBox;
        private System.Windows.Forms.Label backendUrlLabel;
        private System.Windows.Forms.Button killServerButton;
        private System.Windows.Forms.CheckBox startServerChackBox;
        private System.Windows.Forms.FolderBrowserDialog GameLocationFolderBrowser;
        private System.Windows.Forms.TextBox backendUrlTextBox;
    }
}