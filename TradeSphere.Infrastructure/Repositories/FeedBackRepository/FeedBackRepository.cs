using TradeSphere.Domain.Models;

namespace TradeSphere.Infrastructure.Repositories.FeedBackRepository
{
    public class FeedBackRepository(IUnitOfWork unit) : IFeedBackRepository
    {
        public async Task<FeedBack> AddFeedBack(FeedBack feedBack)
        {
            await unit.Repository<FeedBack>().AddAsync(feedBack);
            return await unit.CommitAsync() > 0 ? feedBack : null!;
        }

        public async Task<bool> DeleteFeedBack(FeedBack feedBack)
        {
            unit.Repository<FeedBack>().Delete(feedBack);
            return await unit.CommitAsync() > 0 ? true : false!;
        }

        public async Task<IEnumerable<FeedBack>> GetFeedBacksByProductId(int productId)
        {
            var feedBack = await unit.Repository<FeedBack>().GetAllWithSpec(new FeedBackSpecification(f => f.ProductId == productId));
            if (feedBack is null) return null;
            return feedBack;
        }

        public Task<FeedBack> GetFeedBacksId(int id)
        {
            var feedBack = unit.Repository<FeedBack>().GetByIdAsync(id);
            return feedBack;
        }

        public async Task<FeedBack> UpdateFeedBack(FeedBack feedBack)
        {
            unit.Repository<FeedBack>().Update(feedBack);
            return await unit.CommitAsync() > 0 ? feedBack : null!;

        }
    }
}
