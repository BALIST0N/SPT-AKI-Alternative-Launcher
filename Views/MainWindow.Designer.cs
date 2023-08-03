namespace SPTAKI_Alt_Launcher
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
            label1 = new Label();
            startButton = new Button();
            profilesListBox = new ComboBox();
            gamePathTextBox = new TextBox();
            profileEditButton = new Button();
            serverOutputRichBox = new RichTextBox();
            backendUrlLabel = new Label();
            killServerButton = new Button();
            startServerChackBox = new CheckBox();
            GameLocationFolderBrowser = new FolderBrowserDialog();
            backendUrlTextBox = new TextBox();
            BackgroundColorSelector = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Tomato;
            label1.Font = new Font("Candara", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(14, 21);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 18);
            label1.TabIndex = 3;
            label1.Text = "Game Location";
            // 
            // startButton
            // 
            startButton.BackColor = Color.FromArgb(32, 32, 32);
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Font = new Font("Trebuchet MS", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            startButton.ForeColor = SystemColors.Control;
            startButton.Location = new Point(481, 115);
            startButton.Margin = new Padding(4, 3, 4, 3);
            startButton.Name = "startButton";
            startButton.Size = new Size(187, 57);
            startButton.TabIndex = 0;
            startButton.Text = "Start Game";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            // 
            // profilesListBox
            // 
            profilesListBox.BackColor = Color.FromArgb(32, 32, 32);
            profilesListBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profilesListBox.FlatStyle = FlatStyle.Flat;
            profilesListBox.Font = new Font("Candara", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            profilesListBox.ForeColor = SystemColors.Window;
            profilesListBox.FormattingEnabled = true;
            profilesListBox.Items.AddRange(new object[] { "Select a Profile !" });
            profilesListBox.Location = new Point(481, 16);
            profilesListBox.Margin = new Padding(4, 3, 4, 3);
            profilesListBox.Name = "profilesListBox";
            profilesListBox.Size = new Size(186, 26);
            profilesListBox.TabIndex = 1;
            // 
            // gamePathTextBox
            // 
            gamePathTextBox.AccessibleDescription = "";
            gamePathTextBox.BackColor = Color.FromArgb(32, 32, 32);
            gamePathTextBox.Font = new Font("Candara", 11.25F, FontStyle.Italic, GraphicsUnit.Point);
            gamePathTextBox.ForeColor = Color.Red;
            gamePathTextBox.Location = new Point(134, 16);
            gamePathTextBox.Margin = new Padding(4, 3, 4, 3);
            gamePathTextBox.Name = "gamePathTextBox";
            gamePathTextBox.Size = new Size(231, 26);
            gamePathTextBox.TabIndex = 2;
            gamePathTextBox.Click += gamePathTextBox_Click;
            gamePathTextBox.TextChanged += gamePathTextBox_TextChanged;
            // 
            // profileEditButton
            // 
            profileEditButton.BackColor = Color.FromArgb(32, 32, 32);
            profileEditButton.FlatStyle = FlatStyle.Flat;
            profileEditButton.Font = new Font("Candara", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            profileEditButton.ForeColor = SystemColors.Control;
            profileEditButton.Location = new Point(481, 53);
            profileEditButton.Margin = new Padding(4, 3, 4, 3);
            profileEditButton.Name = "profileEditButton";
            profileEditButton.Size = new Size(187, 30);
            profileEditButton.TabIndex = 6;
            profileEditButton.Text = "Edit Profile";
            profileEditButton.UseVisualStyleBackColor = false;
            profileEditButton.Click += profileEditButton_Click;
            // 
            // serverOutputRichBox
            // 
            serverOutputRichBox.BackColor = SystemColors.ActiveCaptionText;
            serverOutputRichBox.BorderStyle = BorderStyle.FixedSingle;
            serverOutputRichBox.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            serverOutputRichBox.ForeColor = Color.Lime;
            serverOutputRichBox.Location = new Point(0, 185);
            serverOutputRichBox.Margin = new Padding(4, 3, 4, 3);
            serverOutputRichBox.Name = "serverOutputRichBox";
            serverOutputRichBox.Size = new Size(682, 297);
            serverOutputRichBox.TabIndex = 7;
            serverOutputRichBox.Text = "";
            // 
            // backendUrlLabel
            // 
            backendUrlLabel.AutoSize = true;
            backendUrlLabel.BackColor = Color.SandyBrown;
            backendUrlLabel.Font = new Font("Candara", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            backendUrlLabel.ForeColor = Color.Black;
            backendUrlLabel.Location = new Point(14, 144);
            backendUrlLabel.Margin = new Padding(4, 0, 4, 0);
            backendUrlLabel.Name = "backendUrlLabel";
            backendUrlLabel.Size = new Size(100, 18);
            backendUrlLabel.TabIndex = 9;
            backendUrlLabel.Text = "backend URL : ";
            // 
            // killServerButton
            // 
            killServerButton.FlatStyle = FlatStyle.Flat;
            killServerButton.ForeColor = SystemColors.Control;
            killServerButton.Location = new Point(399, 115);
            killServerButton.Margin = new Padding(4, 3, 4, 3);
            killServerButton.Name = "killServerButton";
            killServerButton.Size = new Size(75, 57);
            killServerButton.TabIndex = 10;
            killServerButton.Text = "CLOSE SERVER";
            killServerButton.UseVisualStyleBackColor = true;
            killServerButton.Visible = false;
            killServerButton.Click += killServerButton_Click;
            // 
            // startServerChackBox
            // 
            startServerChackBox.AutoSize = true;
            startServerChackBox.BackColor = Color.Transparent;
            startServerChackBox.ForeColor = SystemColors.Control;
            startServerChackBox.Location = new Point(481, 89);
            startServerChackBox.Margin = new Padding(4, 3, 4, 3);
            startServerChackBox.Name = "startServerChackBox";
            startServerChackBox.Size = new Size(85, 19);
            startServerChackBox.TabIndex = 11;
            startServerChackBox.Text = "Start Server";
            startServerChackBox.UseVisualStyleBackColor = false;
            startServerChackBox.Visible = false;
            // 
            // GameLocationFolderBrowser
            // 
            GameLocationFolderBrowser.Description = "Select Patched EFT game folder";
            GameLocationFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            // 
            // backendUrlTextBox
            // 
            backendUrlTextBox.AccessibleDescription = "";
            backendUrlTextBox.BackColor = Color.SandyBrown;
            backendUrlTextBox.BorderStyle = BorderStyle.None;
            backendUrlTextBox.Font = new Font("Candara", 11.25F, FontStyle.Italic, GraphicsUnit.Point);
            backendUrlTextBox.ForeColor = Color.Black;
            backendUrlTextBox.Location = new Point(127, 143);
            backendUrlTextBox.Margin = new Padding(4, 3, 4, 3);
            backendUrlTextBox.Name = "backendUrlTextBox";
            backendUrlTextBox.Size = new Size(163, 19);
            backendUrlTextBox.TabIndex = 12;
            // 
            // BackgroundColorSelector
            // 
            BackgroundColorSelector.AutoSize = true;
            BackgroundColorSelector.BackColor = Color.Transparent;
            BackgroundColorSelector.Location = new Point(652, 91);
            BackgroundColorSelector.Name = "BackgroundColorSelector";
            BackgroundColorSelector.Size = new Size(15, 14);
            BackgroundColorSelector.TabIndex = 13;
            BackgroundColorSelector.TextAlign = ContentAlignment.MiddleCenter;
            BackgroundColorSelector.UseVisualStyleBackColor = false;
            BackgroundColorSelector.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(681, 186);
            Controls.Add(BackgroundColorSelector);
            Controls.Add(backendUrlTextBox);
            Controls.Add(startServerChackBox);
            Controls.Add(killServerButton);
            Controls.Add(backendUrlLabel);
            Controls.Add(serverOutputRichBox);
            Controls.Add(profileEditButton);
            Controls.Add(label1);
            Controls.Add(gamePathTextBox);
            Controls.Add(profilesListBox);
            Controls.Add(startButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new Size(697, 600);
            MinimumSize = new Size(697, 179);
            Name = "MainWindow";
            Text = "SPT-AKI Launcher (Alternative Version)";
            Shown += MainWindow_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button startButton;
        private ComboBox profilesListBox;
        private TextBox gamePathTextBox;
        private Button profileEditButton;
        private RichTextBox serverOutputRichBox;
        private Label backendUrlLabel;
        private Button killServerButton;
        private CheckBox startServerChackBox;
        private FolderBrowserDialog GameLocationFolderBrowser;
        private TextBox backendUrlTextBox;
        private CheckBox BackgroundColorSelector;
    }
}