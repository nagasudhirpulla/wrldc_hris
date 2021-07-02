using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WrldcHrIs.Application.Common;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Application.Users;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Application.CanteenOrders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppDbContext _context;

        public DeleteOrderCommandHandler(UserManager<ApplicationUser> userManager, ILogger<DeleteOrderCommandHandler> logger, ICurrentUserService currentUserService, IAppDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            string curUsrId = _currentUserService.UserId;
            ApplicationUser curUsr = await _userManager.FindByIdAsync(curUsrId);
            if (curUsr == null)
            {
                var errorMsg = "Logged User not found for order deletion";
                _logger.LogError(errorMsg);
                return false;
            }

            // check if order already created
            CanteenOrder existingOrder = await _context.CanteenOrders.Where(co => co.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (existingOrder == null)
            {
                _logger.LogInformation($"Order Id {request.OrderId} already not present for deletion");
                return true;
            }

            // check if user is authorized for deleting order
            IList<string> usrRoles = await _userManager.GetRolesAsync(curUsr);
            if (curUsrId != existingOrder.CustomerId
                && !usrRoles.Contains(SecurityConstants.AdminRoleString)
                && !usrRoles.Contains(SecurityConstants.CanteenMgrRoleString))
            {
                return false;
            }

            _context.CanteenOrders.Remove(existingOrder);
            _ = await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
