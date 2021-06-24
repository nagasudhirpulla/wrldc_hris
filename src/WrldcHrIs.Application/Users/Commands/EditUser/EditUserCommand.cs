using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Text;
using WrldcHrIs.Application.Users.Queries.GetAppUsers;
using WrldcHrIs.Core.Entities;
using static WrldcHrIs.Application.Common.Mappings.MappingProfile;

namespace WrldcHrIs.Application.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<List<string>>, IMapFrom<UserDTO>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserRole { get; set; }
        public string OfficeId { get; set; }
        public int DepartmentId { get; set; }
        public string Designation { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, EditUserCommand>()
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.UserName));
        }
    }
}
