using System;
using System.Collections.Generic;

namespace aspnetwebapi.Models
{
    public  class MyRoles
    {
        public MyRoles()
        {
            MyUserRoles = new HashSet<MyUserRoles>();
        }

        public int Id { get; set; }
        public string Role { get; set; }

        public ICollection<MyUserRoles> MyUserRoles { get; set; }
    }
}
