using AutoMapper;
using Sat.Recruitment.Application.Users;
using Sat.Recruitment.Core.Domain;

namespace Sat.Recruitment.Application
{
    public class ApplicationAutomapConfig: Profile
    {
        public ApplicationAutomapConfig()
        {            
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
