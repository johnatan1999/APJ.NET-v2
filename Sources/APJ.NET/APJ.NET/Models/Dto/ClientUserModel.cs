using APJ.NET.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models.Dto
{
    public class ClientUserModel
    {
        public string Id { get; set; }
        public string Login { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }

        public string Token { get; set; }

    }
}
