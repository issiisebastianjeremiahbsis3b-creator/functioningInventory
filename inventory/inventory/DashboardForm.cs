using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class DashboardForm : Form
    {
        public DashboardForm(string fullName)
        {
            InitializeComponent();
            lblWelcome.Text = "Welcome, " + fullName + "!";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
