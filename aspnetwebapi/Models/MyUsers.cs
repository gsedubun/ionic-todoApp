using System;
using System.Collections.Generic;

namespace aspnetwebapi.Models
{
    public  class MyUsers
    {
        public MyUsers()
        {
            MyUserRoles = new HashSet<MyUserRoles>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public ICollection<MyUserRoles> MyUserRoles { get; set; }
    }
}
