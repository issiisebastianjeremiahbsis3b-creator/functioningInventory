using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace inventory
{
    public partial class SignInForm : Form
    {
        public SignInForm()
        {
            InitializeComponent();
            btnRegisterHere.Click += (s, e) => OpenRegister();
            lnkRegisterHere.Click += (s, e) => OpenRegister();
        }

        private void OpenRegister()
        {
            this.Hide();
            new RegisterForm().ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtSignInUsername.Text.Trim();
            string password = txtSignInPassword.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter your username and password.");
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(Db.ConnString))
                {
                    conn.Open();
                    using (OleDbCommand cmd = new OleDbCommand(
                        "SELECT FullName FROM Users WHERE Username = ? AND UserPassword = ?", conn))
                    {
                        cmd.Parameters.AddWithValue("?", username);
                        cmd.Parameters.AddWithValue("?", Db.Hash(password));
                        object result = cmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Invalid username or password.");
                            return;
                        }

                        string name = result.ToString();
                        if (name == "") name = username;

                        txtSignInUsername.Clear();
                        txtSignInPassword.Clear();
                        this.Hide();
                        new DashboardForm(name).ShowDialog();
                        this.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
