using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Data.OleDb;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace ShhhNotes.Classes
{
   public static class Database
   {
        private static readonly string filePath = "users.json";

        public static List<User> LoadUsers()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<User>();
                }

                string json = File.ReadAllText(filePath);
                var users = JsonSerializer.Deserialize<List<User>>(json);
                return users ?? new List<User>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON Okuma hatası: " + ex.Message);
                return new List<User>();
            }
        }

        public static void SaveUsers(List<User> users)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(users, options);
                File.WriteAllText(filePath, json);
            }

            catch (Exception ex)
            {
                MessageBox.Show("JSON Yazma hatası: " + ex.Message);
            }
        }
        
        public static void UpdateUser(User updatedUser)
        {
            var users = LoadUsers();
            var existingUser = users.FirstOrDefault(u => u.Username == updatedUser.Username);

            if(existingUser != null)
            {
                existingUser.Notes = updatedUser.Notes;
                SaveUsers(users);
            }
        }
            
   }
}
