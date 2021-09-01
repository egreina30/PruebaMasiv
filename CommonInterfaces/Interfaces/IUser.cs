using CommonInterfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonInterfaces.Interfaces
{
    public interface IUser
    {
        bool CreateUser(User user);

        User GetUserXId(User user);

        bool UpdateBalanceUser(User user);
    }
}
