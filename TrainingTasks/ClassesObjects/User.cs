using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassesObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }    
        public string Email { get; set; }
        public string Profession { get; set; }
        public string Password { get; set; }

    }
}
