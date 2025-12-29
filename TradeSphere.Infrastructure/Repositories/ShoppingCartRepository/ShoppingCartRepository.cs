namespace TradeSphere.Infrastructure.Repositories.ShoppingCartRepository
{
    public class ShoppingCartRepository(IUnitOfWork unit) : IShoppingCartRepository
    {
        public async Task<ShoppingCart> AddShoppingCart(ShoppingCart shoppingCart)
        {
            await unit.Repository<ShoppingCart>().AddAsync(shoppingCart);
            return await unit.CommitAsync() > 0 ? shoppingCart : null!;
        }

        public async Task<bool> DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            unit.Repository<ShoppingCart>().Delete(shoppingCart);
            return await unit.CommitAsync() > 0 ? true : false;
        }

        public async Task<ShoppingCart> GetById(int id)
        {
            var shoppingCart = await unit.Repository<ShoppingCart>().GetByIdAsync(id);
            return shoppingCart;
        }

        public async Task<ShoppingCart> GetByIdTracked(int id)
        {
            var shoppingCart = await unit.Repository<ShoppingCart>().GetByIdSpecTracked(new ShoppingCartSpecification(id));
            return shoppingCart;
        }

        public Task<ShoppingCart> GetShoppingCartByUser(int userId)
        {
            var shoppingCart = unit.Repository<ShoppingCart>().GetByIdSpecTracked(new ShoppingCartSpecification(s => s.AppUserId == userId));
            return shoppingCart;
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            unit.Repository<ShoppingCart>().Update(shoppingCart);
            return await unit.CommitAsync() > 0 ? shoppingCart : null!;
        }
    }
}
