using TradeSphere.Application.DTOs.OrderDto;
using TradeSphere.Application.DTOs.OrderDto.CreateOrderDto;

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

        public async Task<OrderInfoDto> GetById(int id)
        {
            var order = await orderRepository.GetById(id);
            if (order is null) return null;
            var result = mapper.Map<OrderInfoDto>(order);
            return result;
        }

        public async Task<List<OrderInfoDto>> GetByUserId(int UserId)
        {
            var order = await orderRepository.GetByUserId(UserId);
            if (order is null) return null;
            var result = mapper.Map<List<OrderInfoDto>>(order);
            return result;
        }

        public async Task<OrderInfoDto> AddOrder(CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null || createOrderDto.OrderItems == null || !createOrderDto.OrderItems.Any())
            {
                throw new ArgumentException("Order must contain at least one item");
            }

            var order = mapper.Map<Order>(createOrderDto);

            order.OrderItems = mapper.Map<List<OrderItem>>(createOrderDto.OrderItems);

            decimal totalAmount = 0;
            foreach (var item in order.OrderItems)
            {
                totalAmount += item.Quantity * item.UnitPrice;
            }
            order.TotalAmount = totalAmount;

            if (createOrderDto.Payment != null)
            {
                order.Payment = mapper.Map<Payment>(createOrderDto.Payment);
                order.Payment.Amount = totalAmount;
                order.Payment.AppUserId = createOrderDto.AppUserId;
            }

            var savedOrder = await orderRepository.AddOrder(order);

            var result = await orderRepository.GetById(savedOrder.Id);
            return mapper.Map<OrderInfoDto>(result);
        }
    }
}

