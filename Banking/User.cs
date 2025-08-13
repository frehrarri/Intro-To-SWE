using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class User
    {
        private static int ID = 0;

        public User()
        {
            ID++;
            UserID = ID; //increment user id for every new user
        }

        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public long Phone { get; set; }
        public string? Password { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
