using Sat.Recruitment.Core.Domain;
using Sat.Recruitment.Infrastructure.Exts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sat.Recruitment.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        // -- Path file to users
        private readonly string _path = $"{Directory.GetCurrentDirectory()}/Files/Users.txt";

        /// <summary>
        /// Get All to users
        /// </summary>
        /// <returns>List to Users</returns>
        public IList<User> GetAll()
        {
            // -- Read Lines
            var lines = ReadUsersFromFile();

            // -- Map To User
            var users = lines.Select((line,index) => new User
            {
                Id = index,
                Name = line[0].ToString(),
                Email = line[1].ToString(),
                Phone = line[2].ToString(),
                Address = line[3].ToString(),
                UserType = line[4].ToString(),
                Money = decimal.Parse(line[5].ToString()),
            }).ToList();

            return users;
        }

        /// <summary>
        /// Get User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public User Get(int id)
        {
            // -- Get User
            var user = GetAll().FirstOrDefault(x => x.Id == id);

            return user;
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Boolean</returns>
        public bool Add(User entity)
        {
            // -- Get users
            var users = GetAll();

            // -- Add user
            users.Add(entity);

            // -- Update users
            UpdateUsersFromFile(users);
            
            return true;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>        
        public bool Delete(int id)
        {
            // -- Get all users and remove users
            var users = GetAll().Where(x => x.Id != id).ToList();

            // -- Update users
            UpdateUsersFromFile(users);

            return true;
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Update(User entity)
        {
            // -- Get all users except entity
            var users = GetAll().Where(x => x.Id != entity.Id).ToList();

            // -- Add User
            users.Add(entity);

            // -- Update users
            UpdateUsersFromFile(users);

            return true;
        }

        /// <summary>
        /// Return lines of file users
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string[]> ReadUsersFromFile()
        {            
            var lines = File.ReadAllLines(_path).Select(x => x.Split(','));            
            return lines;
        }

        /// <summary>
        /// Update file with line to users
        /// </summary>
        /// <param name="users"></param>
        private void UpdateUsersFromFile(IList<User> users)
        {            
            File.WriteAllText(_path, users.Select(entity => $"{entity.Name},{entity.Email},{entity.Phone},{entity.Address},{entity.UserType},{entity.Money}").ToList().Join(Environment.NewLine));
        }
    }
}
