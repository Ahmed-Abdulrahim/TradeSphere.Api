namespace TradeSphere.Infrastructure.Specefication
{
    public class ShoppingCartSpecification : BaseSpecefication<ShoppingCart>
    {

        public ShoppingCartSpecification()
        {
            AddInculdes();
        }
        public ShoppingCartSpecification(int id) : base(r => r.Id == id)
        {
            AddInculdes();
        }
        public ShoppingCartSpecification(Expression<Func<ShoppingCart, bool>> criteria) : base(criteria)
        {
            AddInculdes();
        }

        void AddInculdes()
        {
            Includes.Add(o => o.AppUser);
            Includes.Add(o => o.CartItems);
            AddInclude("CartItems.Product");

        }


    }
}
