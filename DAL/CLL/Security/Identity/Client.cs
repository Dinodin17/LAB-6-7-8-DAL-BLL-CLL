using System;
using System.Collections.Generic;
using System.Text;

namespace CLL.Security.Identity
{
    public class Client
    : User
    {
        public Client(int userId, string name)
            : base(userId, name, nameof(Client))
        {
        }
    }
}
