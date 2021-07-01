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

namespace WrldcHrIs.Application.CanteenOrders.Commands.EditOrder
{
    public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, List<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EditOrderCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppDbContext _context;

        public EditOrderCommandHandler(UserManager<ApplicationUser> userManager, ILogger<EditOrderCommandHandler> logger, ICurrentUserService currentUserService, IAppDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<List<string>> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            string curUsrId = _currentUserService.UserId;
            ApplicationUser curUsr = await _userManager.FindByIdAsync(curUsrId);
            if (curUsr == null)
            {
                var errorMsg = "Logged In user not found for order creation";
                _logger.LogError(errorMsg);
                return new List<string>() { errorMsg };
            }


            // fetch the order for editing
            var canteenOrder = await _context.CanteenOrders.Where(co => co.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken);

            if (canteenOrder == null)
            {
                string errorMsg = $"Order Id {request.OrderId} not present for editing";
                return new List<string>() { errorMsg };
            }

            // check if user is authorized for editing
            IList<string> usrRoles = await _userManager.GetRolesAsync(curUsr);
            if (curUsrId != canteenOrder.CustomerId
                && !usrRoles.Contains(SecurityConstants.AdminRoleString)
                && !usrRoles.Contains(SecurityConstants.CanteenMgrRoleString))
            {
                return new List<string>() { "This user is not authorized for editing this order since this is not his order and he is not canteen manager or admin" };
            }

            canteenOrder.OrderQuantity = request.OrderQuantity;
            _context.Attach(canteenOrder).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CanteenOrders.Any(e => e.Id == request.OrderId))
                {
                    return new List<string>() { $"Order Id {request.OrderId} not present for editing" };
                }
                else
                {
                    throw;
                }
            }

            return new List<string>();
        }
    }
}
