using AutoMapper;
using Sat.Recruitment.Infrastructure.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Users
{
    public class UserService : IUserService
    {        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public UserService(IMapper mapper,IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns>ResultDto</returns>
        public async Task<ResultDto> CreateUser(UserDto input)
        {
            // -- Convert to method async
            return await Task.Run(() =>
            {
                // -- Validate errors
                var errors = input.ValidateErrors();

                // -- Check errors
                if (!string.IsNullOrEmpty(errors))
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Errors = errors
                    };

                // -- Map UserDto to User
                var newUser = input.GetUser(_mapper);

                try
                {
                    // -- Check if user exist
                    var isDuplicated = _userRepo.GetAll().Any(user => (user.Email == newUser.Email || user.Phone == newUser.Phone) || (user.Name == newUser.Name && user.Address == newUser.Address));

                    // -- If exist return exception
                    if (isDuplicated)
                        throw new Exception("User is duplicated");

                    // -- Add user
                    _userRepo.Add(newUser);
                }
                catch
                {
                    // -- Generate error
                    Debug.WriteLine("The user is duplicated");
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }

                Debug.WriteLine("User Created");
                return new ResultDto()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            });
        }

        /// <summary>
        /// Return all users map
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserDto>> GetAll()
        {
            return await Task.Run(() =>
            {
                var users = _userRepo.GetAll().Select(x => _mapper.Map<UserDto>(x)).ToList();
                return users;
            });
        }
    }
}
