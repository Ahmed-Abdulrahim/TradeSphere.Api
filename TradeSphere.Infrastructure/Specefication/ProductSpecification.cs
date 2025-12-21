namespace TradeSphere.Infrastructure.Specefication
{
    public class ProductSpecification : BaseSpecefication<Product>
    {
        public ProductSpecification()
        {
            AddInculdes();
        }
        public ProductSpecification(int id) : base(r => r.Id == id)
        {
            AddInculdes();
        }
        public ProductSpecification(Expression<Func<Product, bool>> criteria) : base(criteria)
        {
            AddInculdes();
        }

        void AddInculdes()
        {
            Includes.Add(p => p.FeedBacks);
            Includes.Add(p => p.Category);
            Includes.Add(p => p.OrderItems);
            Includes.Add(p => p.OrderItems);


        }
    }
}
