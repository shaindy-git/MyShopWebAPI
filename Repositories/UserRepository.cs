using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using System.Text.Json;

namespace Repositories
{
    class UserRepository
    {

        public List<User> GetUsers()
        {
            List<User> users = System.IO.File.Exists("users.txt") ? System.IO.File.ReadLines("users.txt").Select(line => JsonSerializer.Deserialize<User>(line)).ToList() : new List<User>();
            return users;
        }
        public User Register(User user)
        {
            List<User> users = GetUsers();
            user.UserId = users.Any() ? users.Max(u => u.UserId) + 1 : 1;
            System.IO.File.AppendAllText("users.txt", JsonSerializer.Serialize(user) + Environment.NewLine);
            return user;

        }
        public User Login(string userName)
        {
            List<User> users = GetUsers();
            User userLog = users.FirstOrDefault(u => u.user_name == userName);
            return userLog;
        }


        public User UpDate(User user, int id)
        {

            List<User> users = GetUsers();
            User userToUp = users.FirstOrDefault(u => u.UserId == id);
            if (userToUp == null)
            {
                return null;
            }
            userToUp.first_name = user.first_name != null ? user.first_name : userToUp.first_name;
            userToUp.last_name = user.last_name != null ? user.last_name : userToUp.last_name;
            userToUp.password = user.password != null ? user.password : userToUp.password;
            userToUp.user_name = user.user_name != null ? user.user_name : userToUp.user_name;
            File.WriteAllLines("users.txt", users.Select(u => JsonSerializer.Serialize(u)));
            return userToUp;
        }
    }
}
