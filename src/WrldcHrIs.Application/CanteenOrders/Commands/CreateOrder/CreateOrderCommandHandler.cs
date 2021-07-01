using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WrldcHrIs.Application.Common;
using WrldcHrIs.Application.Common.Interfaces;
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
            string userId = _currentUserService.UserId;
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var errorMsg = "Logged User not found for order creation";
                _logger.LogError(errorMsg);
                return new List<string>() { errorMsg };
            }

            // check if order already created
            var isOrderAlreadyPresent = await _context.CanteenOrders.AnyAsync(co => co.FoodItemName == request.FoodItemName
                                                                            && co.CustomerId == userId
                                                                            && co.OrderDate == request.OrderDate);

            if (isOrderAlreadyPresent)
            {
                var errorMsg = $"Order for {request.FoodItemName} on {request.OrderDate} already present";
                return new List<string>() { errorMsg + ", please update your existing order for this date" };
            }

            // create new order
            CanteenOrder order = new()
            {
                OrderDate = request.OrderDate,
                FoodItemName = request.FoodItemName,
                CustomerId = userId,
                OrderQuantity = request.OrderQuantity
            };

            _context.CanteenOrders.Add(order);
            _ = await _context.SaveChangesAsync(cancellationToken);

            return new List<string>();
        }
    }
}
