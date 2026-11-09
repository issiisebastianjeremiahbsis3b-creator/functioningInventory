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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtRegFullName.Text.Trim();
            string username = txtRegUsername.Text.Trim();
            string password = txtRegPassword.Text;
            string confirm = txtRegConfirmPassword.Text;

            if (fullName == "" || username == "" || password == "" || confirm == "")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(Db.ConnString))
                {
                    conn.Open();

                    using (OleDbCommand check = new OleDbCommand(
                        "SELECT COUNT(*) FROM Users WHERE Username = ?", conn))
                    {
                        check.Parameters.AddWithValue("?", username);
                        if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Username already taken.");
                            return;
                        }
                    }

                    using (OleDbCommand cmd = new OleDbCommand(
                        "INSERT INTO Users (Username, UserPassword, FullName) VALUES (?, ?, ?)", conn))
                    {
                        cmd.Parameters.AddWithValue("?", username);
                        cmd.Parameters.AddWithValue("?", Db.Hash(password));
                        cmd.Parameters.AddWithValue("?", fullName);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Account created! You can now sign in.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
