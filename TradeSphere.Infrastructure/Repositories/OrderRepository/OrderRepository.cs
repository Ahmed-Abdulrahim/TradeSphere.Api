namespace TradeSphere.Infrastructure.Repositories.OrderRepository
{
    public class OrderRepository(IUnitOfWork unit) : IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await unit.Repository<Order>().GetAllWithSpec(new OrderSpecification());
            return orders;
        }
    }
}
