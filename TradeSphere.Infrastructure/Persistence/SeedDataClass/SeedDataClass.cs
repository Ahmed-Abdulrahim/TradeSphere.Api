namespace TradeSphere.Infrastructure.Persistence.SeedDataClass
{
    public static class SeedDataClass
    {
        public static async Task SeedDataAsync(this TradeSphereDbContext context, string contentRootPath)
        {
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            var passwordHasher = new PasswordHasher<AppUser>();

            var seedFilesPath = Path.Combine(contentRootPath, "Persistence", "SeedFiles");
            if (!Directory.Exists(seedFilesPath))
            {
                seedFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "TradeSphere.Infrastructure", "Persistence", "SeedFiles");
            }
            if (!Directory.Exists(seedFilesPath))
            {
                throw new DirectoryNotFoundException($"Seed files not found at: {seedFilesPath}");
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                //Read Roles From JSON
                if (!context.Roles.Any())
                {
                    var rolesData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "AppRoles.json"));
                    var roles = JsonSerializer.Deserialize<List<AppRole>>(rolesData, option)!;
                    context.Roles.AddRange(roles);
                    await context.SaveChangesAsync();
                }

                //Add Users
                if (!context.Users.Any())
                {
                    var userData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "AppUsers.json"));
                    var users = JsonSerializer.Deserialize<List<AppUser>>(userData, option)!;
                    foreach (var user in users)
                    {
                        user.Id = 0;
                        user.NormalizedUserName = user.UserName.ToUpper();
                        user.NormalizedEmail = user.Email.ToUpper();
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        user.ConcurrencyStamp = Guid.NewGuid().ToString();
                        user.PasswordHash = passwordHasher.HashPassword(user, "P@ssw0rd");
                    }
                    context.Users.AddRange(users);
                    await context.SaveChangesAsync();
                }

                //AddUserROle
                if (!context.UserRoles.Any())
                {
                    var userRolesData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "UserRoles.json"));
                    var userRolesMappings = JsonSerializer.Deserialize<List<UserRoleMapping>>(userRolesData, option)!;

                    var userRoles = new List<IdentityUserRole<int>>();

                    foreach (var mapping in userRolesMappings)
                    {
                        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == mapping.UserEmail);
                        var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == mapping.RoleName);

                        if (user != null && role != null)
                        {
                            userRoles.Add(new IdentityUserRole<int>
                            {
                                UserId = user.Id,
                                RoleId = role.Id
                            });
                        }
                    }

                    context.UserRoles.AddRange(userRoles);
                    await context.SaveChangesAsync();
                }


                //Add Category
                if (!context.Categories.Any())
                {
                    var categoryData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "Categories.json"));
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData, option)!;
                    context.Categories.AddRange(categories);
                    await context.SaveChangesAsync();
                }

                //Add Product
                if (!context.Products.Any())
                {
                    var productData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "Products.json"));
                    var products = JsonSerializer.Deserialize<List<Product>>(productData, option)!;
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }

                //Add ShoppingCarts
                if (!context.ShoppingCarts.Any())
                {
                    var shoppingCartData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "ShoppingCarts.json"));
                    var shoppingCarts = JsonSerializer.Deserialize<List<ShoppingCart>>(shoppingCartData, option)!;
                    context.ShoppingCarts.AddRange(shoppingCarts);
                    await context.SaveChangesAsync();
                }

                //Add CartItems
                if (!context.CartItems.Any())
                {
                    var cartItemData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "CartItems.json"));
                    var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartItemData, option)!;
                    context.CartItems.AddRange(cartItems);
                    await context.SaveChangesAsync();
                }

                //Add Orders
                if (!context.Orders.Any())
                {
                    var ordersData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "Orders.json"));
                    var orders = JsonSerializer.Deserialize<List<Order>>(ordersData, option)!;
                    context.Orders.AddRange(orders);
                    await context.SaveChangesAsync();
                }

                //Add OrderItems
                if (!context.OrderItems.Any())
                {
                    var orderItemsData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "OrderItems.json"));
                    var orderItems = JsonSerializer.Deserialize<List<OrderItem>>(orderItemsData, option)!;
                    context.OrderItems.AddRange(orderItems);
                    await context.SaveChangesAsync();

                    //Add FeedBacks
                    if (!context.FeedBacks.Any())
                    {
                        var feedBacksData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "FeedBacks.json"));
                        var feedBacks = JsonSerializer.Deserialize<List<FeedBack>>(feedBacksData, option)!;
                        context.FeedBacks.AddRange(feedBacks);
                        await context.SaveChangesAsync();
                    }

                    //Add RefreshTokens
                    if (!context.RefreshTokens.Any())
                    {
                        var refreshTokensData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "RefreshTokens.json"));
                        var refreshTokens = JsonSerializer.Deserialize<List<RefreshToken>>(refreshTokensData, option)!;
                        context.RefreshTokens.AddRange(refreshTokens);
                        await context.SaveChangesAsync();
                    }

                    //Add Payments
                    if (!context.Payments.Any())
                    {
                        var paymentsData = await File.ReadAllTextAsync(Path.Combine(seedFilesPath, "Payments.json"));
                        var payments = JsonSerializer.Deserialize<List<Payment>>(paymentsData, option)!;
                        context.Payments.AddRange(payments);
                        await context.SaveChangesAsync();
                    }
                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                try
                {
                    await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Users', RESEED, 0)");
                    await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Patients', RESEED, 0)");
                    await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Departments', RESEED, 0)");
                }
                catch (Exception innerEx)
                {
                    Console.WriteLine($"Failed to reset identities: {innerEx.Message}");
                }

                throw;
            }

        }

    }
    public class UserRoleMapping
    {
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
    }
}
