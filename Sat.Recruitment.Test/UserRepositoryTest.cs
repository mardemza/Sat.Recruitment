using Sat.Recruitment.Core.Domain;
using Sat.Recruitment.Infrastructure.Repositories.Users;
using System;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserRepositoryTest
    {
        private readonly UserRepository _userRepo;
        public UserRepositoryTest()
        {
            _userRepo = new UserRepository();
        }

        [Fact]
        public void GetAll()
        {
            var users = _userRepo.GetAll();

            Assert.Equal(3, users.Count);

            var user3 = users[1];
            Assert.Equal("Agustina", user3.Name);
            Assert.Equal("Agustina@gmail.com", user3.Email);
            Assert.Equal("+534645213542", user3.Phone);
            Assert.Equal("Garay y Otra Calle", user3.Address);
            Assert.Equal("SuperUser", user3.UserType);
            Assert.Equal(Convert.ToDecimal("112234"), user3.Money);
        }

        [Fact]
        public void Get()
        {
            var user = _userRepo.Get(0);

            Assert.Equal("Franco", user.Name);
            Assert.Equal("Franco.Perez@gmail.com", user.Email);
            Assert.Equal("+534645213542", user.Phone);
            Assert.Equal("Alvear y Colombres", user.Address);
            Assert.Equal("Premium", user.UserType);
            Assert.Equal(Convert.ToDecimal("112234"), user.Money);
        }

        [Fact]
        public void Add()
        {
            var userAdd = new User
            {
                Name = "Moises",
                Email = "moises.rivas@outlook.com",
                Phone = "+542664252463",
                Address = "San Luis",
                UserType = "Premium",
                Money = Convert.ToDecimal("44000")
            };

            _userRepo.Add(userAdd);

            var user = _userRepo.Get(3);

            Assert.Equal("Moises", user.Name);
            Assert.Equal("moises.rivas@outlook.com", user.Email);
            Assert.Equal("+542664252463", user.Phone);
            Assert.Equal("San Luis", user.Address);
            Assert.Equal("Premium", user.UserType);
            Assert.Equal(Convert.ToDecimal("44000"), user.Money);

            // -- Clear user add
            _userRepo.Delete(3);
        }

        [Fact]
        public void Update()
        {
            var userAdd = new User
            {
                Name = "Moises",
                Email = "moises.rivas@outlook.com",
                Phone = "+542664252463",
                Address = "San Luis",
                UserType = "Premium",
                Money = Convert.ToDecimal("44000")
            };

            _userRepo.Add(userAdd);

            var userUpdate = new User
            {
                Name = "Moises Adrian",
                Email = "moises.rivas@hotmail.com",
                Phone = "+542664225588",
                Address = "San Luis San Luis",
                UserType = "Normal",
                Money = Convert.ToDecimal("34000")
            };

            _userRepo.Update(userUpdate);

            var user = _userRepo.Get(3);

            Assert.Equal("Moises Adrian", user.Name);
            Assert.Equal("moises.rivas@hotmail.com", user.Email);
            Assert.Equal("+542664225588", user.Phone);
            Assert.Equal("San Luis San Luis", user.Address);
            Assert.Equal("Normal", user.UserType);
            Assert.Equal(Convert.ToDecimal("34000"), user.Money);

            // -- Clear user add
            _userRepo.Delete(3);
        }

        [Fact]
        public void Delete()
        {
            var userAdd = new User
            {
                Name = "Moises",
                Email = "moises.rivas@outlook.com",
                Phone = "+542664252463",
                Address = "San Luis",
                UserType = "Premium",
                Money = Convert.ToDecimal("44000")
            };

            _userRepo.Add(userAdd);

            // -- Clear user add
            Assert.True(_userRepo.Delete(3));
        }
    }
}
