using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrldcHrIs.Core.Entities;
using static WrldcHrIs.Application.Common.Mappings.MappingProfile;

namespace WrldcHrIs.Application.CanteenOrders.Commands.EditOrder
{
    public class EditOrderCommand : IRequest<List<string>>, IMapFrom<CanteenOrder>
    {
        public int OrderId { get; set; }
        public int OrderQuantity { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CanteenOrder, EditOrderCommand>()
                .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.Id));
        }
    }
}
