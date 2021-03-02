using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
