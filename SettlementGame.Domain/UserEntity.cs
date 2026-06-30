using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }
    }
}
