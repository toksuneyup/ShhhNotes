using Jsoniledeneme;
using ShhhNotes.Classes;
using System;
using System.Collections;
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
    public partial class MainForm: Form
    {
        private User currentUser;
        
        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            MessageBox.Show($"Welcome, {currentUser.Username}");
            LoadNoteList();
        }

        private void LoadNoteList()
        {
            lstNotes.Items.Clear();
            foreach(var note in currentUser.Notes)
            {
                lstNotes.Items.Add(note.Title);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void lstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstNotes.SelectedIndex;
            if(index != -1)
            {
                var selectedNote = currentUser.Notes[index];
                txtTitle.Text = selectedNote.Title;
                txtContent.Text = selectedNote.Content;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string content = txtContent.Text.Trim();

            if(string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Title and content cannot be left blank.");
                return;
            }

            Note note = new Note
            {
                Title = txtTitle.Text,
                Content = txtContent.Text,
                CreatedAt = DateTime.Now
            };

            currentUser.Notes.Add(note);
            Database.UpdateUser(currentUser);
            LoadNoteList();

            txtTitle.Clear();
            txtContent.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lstNotes.SelectedIndex;
            if(index == -1)
            {
                MessageBox.Show("Please select the note to delete.");
                return;
            }

            currentUser.Notes.RemoveAt(index);

            var allUsers = Database.LoadUsers();
            var userInList = allUsers.FirstOrDefault(u => u.Username == currentUser.Username);
            if(userInList != null)
            {
                userInList.Notes = currentUser.Notes;
                Database.SaveUsers(allUsers);
            }

            LoadNoteList();
            ClearInputs();
            MessageBox.Show("Note deleted.");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = lstNotes.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the note to update.");
                return;
            }

            currentUser.Notes[index].Title = txtTitle.Text.Trim();
            currentUser.Notes[index].Content = txtContent.Text.Trim();

            var allUsers = Database.LoadUsers();
            var userInList = allUsers.FirstOrDefault(u => u.Username == currentUser.Username);
            if(userInList != null)
            {
                userInList.Notes = currentUser.Notes;
                Database.SaveUsers(allUsers);
            }

            LoadNoteList();
            MessageBox.Show("Note updated.");
        }

        private void ClearInputs()
        {
            txtTitle.Clear();
            txtContent.Clear();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have logged out.");
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FilterNotes(string query)
        {
            lstNotes.Items.Clear();

            if (string.IsNullOrWhiteSpace(query))
            {
                LoadNoteList();
                return;
            }

            var filteredNotes = currentUser.Notes
                .Where(n => n.Title.ToLower().Contains(query.ToLower())
                || n.Content.ToLower().Contains(query.ToLower()))
            .ToList();

            foreach (var note in filteredNotes)
            {
                lstNotes.Items.Add(note.Title);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();
            FilterNotes(query);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}