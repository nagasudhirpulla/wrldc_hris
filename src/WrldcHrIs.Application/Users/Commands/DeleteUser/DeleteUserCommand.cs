using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrldcHrIs.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<List<string>>
    {
        public string Id { get; set; }
    }
}
