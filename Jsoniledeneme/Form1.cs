using ShhhNotes;
using ShhhNotes.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jsoniledeneme
{
    public partial class LoginForm: Form
    {
       

        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string rawPassword = txtPassword.Text;

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(rawPassword))
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }
            
            var users = Database.LoadUsers();

            string hashedPassword = PasswordHelper.HashPassword(rawPassword);

            var user = users.FirstOrDefault(u =>
                u.Username == username &&
                u.PasswordHash == hashedPassword
            );

            if(user != null)
            {
                MessageBox.Show("Login successful!");
                MainForm mainForm = new MainForm(user);
                mainForm.Show();
                this.Hide();
            }

            else if(user == null || user.PasswordHash != hashedPassword)
            {
                MessageBox.Show("Username or password is incorrect.");
            }

            else
            {
                MessageBox.Show("User not found.");
            }
        }

        private void lblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
