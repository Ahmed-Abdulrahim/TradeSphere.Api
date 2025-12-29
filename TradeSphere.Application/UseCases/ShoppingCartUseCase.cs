using TradeSphere.Application.DTOs.ShoppingCartDto;

namespace TradeSphere.Application.UseCases
{
    public class ShoppingCartUseCase(IMapper mapper, IShoppingCartRepository shoppingCartRepository)
    {
        public async Task<ShoppingCartDto> GetShoppingCartByUserIdAsync(string userName)
        {
            var shoppingCart = await shoppingCartRepository.GetShoppingCartByUser(userName);
            if (shoppingCart is null) return null;
            return mapper.Map<ShoppingCartDto>(shoppingCart);
        }

        public async Task<ShoppingCartDto> CreateShoppingCartAsync(ShoppingCartDto shoppingCartDto)
        {
            var shoppingCart = mapper.Map<ShoppingCart>(shoppingCartDto);
            var createdShoppingCart = await shoppingCartRepository.AddShoppingCart(shoppingCart);
            return mapper.Map<ShoppingCartDto>(createdShoppingCart);
        }
    }
}
