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

namespace ShhhNotes
{
    public partial class RegisterForm: Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text.Trim();
            string rawPassword = txtNewPassword.Text;

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(rawPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            var users = Database.LoadUsers();

            if(users.Any(u => u.Username == txtNewUsername.Text))
            {
                MessageBox.Show("This username already exits.");
                return;
            }

            string hashedPassword = PasswordHelper.HashPassword(rawPassword);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            users.Add(newUser);
            Database.SaveUsers(users);

            MessageBox.Show("Registration is successful! You can log in.");
            this.Close();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
