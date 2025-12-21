using TradeSphere.Application.DTOs.OrderDto;

namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();

    }
}
