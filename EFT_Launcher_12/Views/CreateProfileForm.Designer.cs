
namespace EFT_Launcher_12.Views
{
    partial class CreateProfileForm
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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editionListBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.profileEditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.AccessibleDescription = "";
            this.usernameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.usernameTextBox.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameTextBox.ForeColor = System.Drawing.Color.White;
            this.usernameTextBox.Location = new System.Drawing.Point(103, 11);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(199, 27);
            this.usernameTextBox.TabIndex = 3;
            this.usernameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.usernameTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "User name :";
            // 
            // editionListBox
            // 
            this.editionListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.editionListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.editionListBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editionListBox.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editionListBox.ForeColor = System.Drawing.SystemColors.Window;
            this.editionListBox.FormattingEnabled = true;
            this.editionListBox.Items.AddRange(new object[] {
            "Standard",
            "Left Behind",
            "Prepare For Escape",
            "Edge Of Darkness"});
            this.editionListBox.Location = new System.Drawing.Point(103, 55);
            this.editionListBox.Name = "editionListBox";
            this.editionListBox.Size = new System.Drawing.Size(199, 27);
            this.editionListBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(34, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Edition :";
            // 
            // profileEditButton
            // 
            this.profileEditButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.profileEditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profileEditButton.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileEditButton.ForeColor = System.Drawing.SystemColors.Control;
            this.profileEditButton.Location = new System.Drawing.Point(1, 115);
            this.profileEditButton.Name = "profileEditButton";
            this.profileEditButton.Size = new System.Drawing.Size(313, 38);
            this.profileEditButton.TabIndex = 7;
            this.profileEditButton.Text = "Create !";
            this.profileEditButton.UseVisualStyleBackColor = false;
            this.profileEditButton.Click += new System.EventHandler(this.profileEditButton_Click);
            // 
            // CreateProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(314, 154);
            this.Controls.Add(this.profileEditButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editionListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usernameTextBox);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateProfileForm";
            this.Text = "Create new profile :";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateProfileForm_FormClosing);
            this.Load += new System.EventHandler(this.CreateProfileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox editionListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button profileEditButton;
    }
}