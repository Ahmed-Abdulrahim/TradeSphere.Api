namespace TradeSphere.Infrastructure.Repositories.OrderRepository
{
    public class OrderRepository(IUnitOfWork unit) : IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await unit.Repository<Order>().GetAllWithSpec(new OrderSpecification());
            return orders;
        }
        public async Task<Order> GetById(int id)
        {
            var order = await unit.Repository<Order>().GetByIdSpec(new OrderSpecification(id));
            return order;
        }
        public async Task<Order> AddOrder(Order order)
        {
            await unit.Repository<Order>().AddAsync(order);
            await unit.CommitAsync();
            return order;
        }

        public Task<Order> DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetByUserId(int userId)
        {
            var order = await unit.Repository<Order>().GetAllWithSpec(new OrderSpecification(o => o.AppUserId == userId));
            return order;
        }

        public Task<Order> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
