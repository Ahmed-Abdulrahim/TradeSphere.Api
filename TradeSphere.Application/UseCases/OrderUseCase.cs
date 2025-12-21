using TradeSphere.Application.DTOs.OrderDto;

namespace TradeSphere.Application.UseCases
{
    public class OrderUseCase(IOrderRepository orderRepository, IMapper mapper)
    {
        public async Task<List<OrderInfoDto>> GetAllOrder()
        {
            var getOrders = await orderRepository.GetAll();
            var orders = mapper.Map<List<OrderInfoDto>>(getOrders);
            return orders;
        }
    }
}
