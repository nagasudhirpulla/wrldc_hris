using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WrldcHrIs.Application.Common;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Application.Users;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Application.CanteenOrders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, List<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppDbContext _context;

        public CreateOrderCommandHandler(UserManager<ApplicationUser> userManager, ILogger<CreateOrderCommandHandler> logger, ICurrentUserService currentUserService, IAppDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<List<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            string curUsrId = _currentUserService.UserId;
            ApplicationUser curUsr = await _userManager.FindByIdAsync(curUsrId);
            if (curUsr == null)
            {
                var errorMsg = "Logged User not found for order creation";
                _logger.LogError(errorMsg);
                return new List<string>() { errorMsg };
            }

            // check if user is authorized for adding order
            IList<string> usrRoles = await _userManager.GetRolesAsync(curUsr);
            if (curUsrId != request.CustomerId
                && !usrRoles.Contains(SecurityConstants.AdminRoleString)
                && !usrRoles.Contains(SecurityConstants.CanteenMgrRoleString))
            {
                return new List<string>() { "This user is not authorized for creating this order since this is not his order and he is not canteen manager or admin" };
            }

            // check if order already created
            var isOrderAlreadyPresent = await _context.CanteenOrders.AnyAsync(co => co.FoodItemName == request.FoodItemName
                                                                            && co.CustomerId == request.CustomerId
                                                                            && co.OrderDate == request.OrderDate, cancellationToken: cancellationToken);

            if (isOrderAlreadyPresent)
            {
                var errorMsg = $"Order for {request.FoodItemName} on {request.OrderDate} already present";
                return new List<string>() { errorMsg + ", please update your existing order for this date" };
            }

            // create new order
            CanteenOrder order = new()
            {
                OrderDate = request.OrderDate,
                OrderQuantity = request.OrderQuantity,
                FoodItemName = request.FoodItemName,
                FoodItemDescription = request.FoodItemDescription,
                FoodItemUnitPrice = request.FoodItemUnitPrice,
                CustomerId = request.CustomerId
            };

            _context.CanteenOrders.Add(order);
            _ = await _context.SaveChangesAsync(cancellationToken);

            return new List<string>();
        }
    }
}
