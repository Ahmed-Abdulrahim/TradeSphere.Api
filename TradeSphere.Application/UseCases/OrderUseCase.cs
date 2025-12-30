namespace TradeSphere.Application.UseCases
{
    public class OrderUseCase(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
    {
        public async Task<List<OrderInfoDto>> GetAllOrder()
        {
            var getOrders = await orderRepository.GetAll();
            var orders = mapper.Map<List<OrderInfoDto>>(getOrders);
            return orders;
        }

        public async Task<OrderInfoDto> GetById(int id)
        {
            var order = await orderRepository.GetById(id);
            if (order is null) return null;
            var result = mapper.Map<OrderInfoDto>(order);
            return result;
        }

        public async Task<List<OrderInfoDto>> GetByUserId(int UserId)
        {
            var order = await orderRepository.GetByUserId(UserId);
            if (order is null) return null;
            var result = mapper.Map<List<OrderInfoDto>>(order);
            return result;
        }

        public async Task<OrderInfoDto> Checkout(CreateOrderDto dto)
        {

            if (dto == null || !dto.OrderItems.Any())
                throw new ArgumentException("Invalid order");

            var order = new Order
            {
                AppUserId = dto.AppUserId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;

            foreach (var item in dto.OrderItems)
            {
                var product = await productRepository.GetById(item.ProductId);

                if (product == null)
                    throw new Exception("Product not found");

                if (product.Quantity < item.Quantity)
                    throw new Exception("Insufficient stock");

                product.Quantity -= item.Quantity;
                await productRepository.UpdateProduct(product);
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                total += orderItem.Quantity * orderItem.UnitPrice;
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = total;

            order.Payment = new Payment
            {
                Amount = total,
                Status = PaymentStatus.Pending,
                AppUserId = dto.AppUserId,
                PaymentDate = DateTime.UtcNow,


            };

            await orderRepository.AddOrder(order);

            var savedOrder = await orderRepository.GetById(order.Id);
            return mapper.Map<OrderInfoDto>(savedOrder);
        }

        public async Task<OrderInfoDto> CancelOrder(int orderId)
        {
            var order = await orderRepository.GetByIdTracked(orderId);
            if (order == null)
                throw new Exception("Order not found");
            if (order.Status == OrderStatus.Cancelled || order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
                throw new Exception("Order is already cancelled");
            order.Status = OrderStatus.Cancelled;
            foreach (var item in order.OrderItems)
            {
                var product = await productRepository.GetByIdTracked(item.ProductId);
                if (product != null)
                {
                    product.Quantity += item.Quantity;
                }
            }
            await orderRepository.UpdateOrder(order);
            return mapper.Map<OrderInfoDto>(order);
        }


        public async Task<OrderInfoDto> UpdateOrderStatus(UpdateOrderStatusDto orderstatus)
        {
            var order = await orderRepository.GetByIdTracked(orderstatus.OrderId);
            if (order == null)
                throw new Exception("Order not found");
            var stausExists = Enum.TryParse<OrderStatus>(orderstatus.Status, out var status);
            order.Status = status;
            await orderRepository.UpdateOrder(order);
            return mapper.Map<OrderInfoDto>(order);
        }


        public async Task<string> GetStatus(int orderId)
        {
            var order = await orderRepository.GetById(orderId);
            if (order == null)
                throw new Exception("Order not found");
            return order.Status.ToString();
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            var order = await orderRepository.GetByIdTracked(orderId);
            if (order == null)
                throw new Exception("Order not found");
            var orderDeleted = await orderRepository.DeleteOrder(order);
            return orderDeleted;
        }
    }
}

