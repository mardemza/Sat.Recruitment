using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.Users;
using System;
using System.Collections.Generic;
using Xunit;

namespace Sat.Recruitment.Test
{    
    public class UsersControllerTest
    {
        [Fact]
        public void CreateUserIsSuccess()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.CreateUser(It.IsAny<UserDto>())).ReturnsAsync(new ResultDto
            {
                IsSuccess = true,
                Errors = "User Created"
            });
            var userController = new UsersController(userService.Object);

            var result = userController.CreateUser(
                new UserDto("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124")).Result;


            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        } 

        [Fact]
        public void CreateUserIsDuplicated()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.CreateUser(It.IsAny<UserDto>())).ReturnsAsync(new ResultDto
            {
                IsSuccess = false,
                Errors = "The user is duplicated"
            });
            var userController = new UsersController(userService.Object);

            var result = userController.CreateUser(
                new UserDto("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124")).Result;


            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void GetAllUsers()
        {
            var list = new List<UserDto>();
            list.Add(new UserDto("Ricardo", "ricardo@hotmail.com", "Santa Fe", "+5466554478", "Normal", "326598"));
            list.Add(new UserDto("Mercedes", "mercedes@hotmail.com", "Rio Cuarto", "+5435125365", "SuperUser", "123453"));
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetAll()).ReturnsAsync(list);
            var userController = new UsersController(userService.Object);

            var result = userController.GetAllUser().Result;


            Assert.Equal(2, result.Count);

            var user = result[0];
            Assert.Equal("Ricardo", user.Name);
            Assert.Equal("ricardo@hotmail.com", user.Email);
            Assert.Equal("Santa Fe", user.Address);
        }
    }
}
