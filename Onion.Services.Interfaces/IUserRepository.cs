using Onion.Domain.Core.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Services.Interfaces
{
    public interface IUserRepository
    {
        User SetUp(string firstname, string lastname, string email, string password, long card);
        User SetUp(string firstname, string lastname, string email, string password);
        User SetIn(string email, string password);
        void AddUser(User user);
        User GetUser(int? id);
        bool IsUsed(string email);
        User GetByEmail(string email);
        IEnumerable<User> GetUsers();
        void ChangeUsersInfo(User user, string name, string lastname);
        void ChangeUsersPassword(User user, string password);
        void ChangeUsersInfo(User user, decimal money);
        void ChangeUsersInfo(User user, User newUser);
        void ChangeUsersEmail(User user, string email);
        Guest Exit();
    }
}
