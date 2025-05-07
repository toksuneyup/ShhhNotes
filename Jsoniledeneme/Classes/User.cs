using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShhhNotes.Classes
{
   public class User
   {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
   }
}
