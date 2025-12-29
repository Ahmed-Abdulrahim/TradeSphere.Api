using TradeSphere.Application.DTOs.ShoppingCartDto;

namespace TradeSphere.Application.UseCases
{
    public class ShoppingCartUseCase(IMapper mapper, IShoppingCartRepository shoppingCartRepository)
    {
        public async Task<ShoppingCartDto> GetShoppingCartByUserIdAsync(int userId)
        {
            var shoppingCart = await shoppingCartRepository.GetShoppingCartByUser(userId);
            if (shoppingCart is null) return null;
            return mapper.Map<ShoppingCartDto>(shoppingCart);
        }

        public async Task<ShoppingCartDto> AddToCartAsync(int userId, AddToCartDto dto)
        {
           
            var cart = await shoppingCartRepository.GetShoppingCartByUser(userId);

            
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    AppUserId = userId,
                    CartItems = new List<CartItem>()
                };

                await shoppingCartRepository.AddShoppingCart(cart);
            }

          
            var existingItem = cart.CartItems
                .FirstOrDefault(c => c.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            await shoppingCartRepository.UpdateShoppingCart(cart);

            return mapper.Map<ShoppingCartDto>(cart);
        }





    }
}
