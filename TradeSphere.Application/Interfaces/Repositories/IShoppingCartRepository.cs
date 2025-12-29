namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartByUser(string userName);
        Task<ShoppingCart> GetById(int id);
        Task<ShoppingCart> AddShoppingCart(ShoppingCart shoppingCart);
        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart);
        Task<bool> DeleteShoppingCart(ShoppingCart shoppingCart);
        public Task<ShoppingCart> GetByIdTracked(int id);
    }
}
