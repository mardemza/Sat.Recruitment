using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Users
{
    public interface IUserService
    {
        Task<ResultDto> CreateUser(UserDto input);
        Task<IList<UserDto>> GetAll();
    }
}