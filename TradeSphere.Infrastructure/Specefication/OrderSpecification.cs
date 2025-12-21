namespace TradeSphere.Infrastructure.Specefication
{
    public class OrderSpecification : BaseSpecefication<Order>
    {
        public OrderSpecification()
        {
            AddInculdes();
        }
        public OrderSpecification(int id) : base(r => r.Id == id)
        {
            AddInculdes();
        }
        public OrderSpecification(Expression<Func<Order, bool>> criteria) : base(criteria)
        {
            AddInculdes();
        }

        void AddInculdes()
        {
            Includes.Add(o => o.AppUser);
            Includes.Add(p => p.Payment);
            AddInclude("Payment.AppUser");
            Includes.Add(p => p.OrderItems);
            AddInclude("OrderItems.Product");

        }

    }
}
