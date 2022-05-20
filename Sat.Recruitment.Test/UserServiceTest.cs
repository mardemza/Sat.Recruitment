using AutoMapper;
using Moq;
using Sat.Recruitment.Application;
using Sat.Recruitment.Application.Users;
using Sat.Recruitment.Core.Domain;
using Sat.Recruitment.Infrastructure.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserServiceTest
    {
        private IMapper _mapper;
        private Mock<IUserRepository> _userRepo;
        private UserService _userService;

        public UserServiceTest()
        {
            // -- Mock Mapper
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationAutomapConfig());
            });
            _mapper = mockMapper.CreateMapper();

            // -- Mock IUserRepository
            var list = new List<User>
            {
                new User()
                {
                    Name = "Ricardo",
                    Email = "ricardo@hotmail.com",
                    Address = "Santa Fe",
                    Phone = "+5466554478",
                    UserType = "Normal",
                    Money = Convert.ToDecimal("326598")
                }
            };
            _userRepo = new Mock<IUserRepository>();
            _userRepo.Setup(x => x.GetAll()).Returns(list);

            // -- Init UserService
            _userService = new UserService(_mapper, _userRepo.Object);
        }

        [Fact]
        public void CreateUser()
        {
            var result = _userService.CreateUser(new UserDto("Mercedes", "mercedes@hotmail.com", "Rio Cuarto", "+5435125365", "SuperUser", "123453")).Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void GetAll()
        {
            var result = _userService.GetAll().Result;

            Assert.Single(result);            
        }
    }
}
