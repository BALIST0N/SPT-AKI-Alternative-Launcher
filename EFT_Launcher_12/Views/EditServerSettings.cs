using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EFT_Launcher_12.Views
{
    public partial class EditServerSettings : Form
    {
        public EditServerSettings(Point location)
        {
            InitializeComponent();

            location.Y += 100;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;
            this.backendURLTextBox.Text = "https://";
            this.Select();
        }


        private void backendURLTextBox_TextChanged(object sender, EventArgs e)
        {
            bool httpStr = Regex.IsMatch(this.backendURLTextBox.Text, "https://", RegexOptions.IgnoreCase);
            string ip = Regex.Replace(this.backendURLTextBox.Text, "https://", "", RegexOptions.IgnoreCase);
            bool y = Regex.IsMatch(ip, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            if( httpStr == true && y ==true)
            {
                this.backendURLTextBox.ForeColor = Color.White;
            }
            else
            {
                this.backendURLTextBox.ForeColor = Color.Red;
            }

        }

    }

}
