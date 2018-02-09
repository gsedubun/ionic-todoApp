using System;
using System.Collections.Generic;

namespace aspnetwebapi.Models
{
    public  class MyUserRoles
    {
        public int Id { get; set; }
        public int? MyRoleId { get; set; }
        public int? MyUserId { get; set; }

        public MyRoles MyRole { get; set; }
        public MyUsers MyUser { get; set; }
    }
}
