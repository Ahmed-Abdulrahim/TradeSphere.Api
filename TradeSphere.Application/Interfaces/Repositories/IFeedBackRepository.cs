namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IFeedBackRepository
    {
        Task<FeedBack> AddFeedBack(FeedBack feedBack);
        Task<FeedBack> UpdateFeedBack(FeedBack feedBack);
        Task<IEnumerable<FeedBack>> GetFeedBacksByProductId(int productId);
        Task<FeedBack> GetFeedBacksId(int id);
        Task<bool> DeleteFeedBack(FeedBack feedBackId);
    }
}
