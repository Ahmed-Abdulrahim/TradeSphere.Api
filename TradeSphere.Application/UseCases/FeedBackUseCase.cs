
using TradeSphere.Application.DTOs.FeedBackDto;

namespace TradeSphere.Application.UseCases
{
    public class FeedBackUseCase(IFeedBackRepository feedBackRepository, IMapper mapper)
    {
        public async Task<FeedBack> AddFeedBack(FeedBackAddDto inputDto)
        {
            var feedBackEntity = mapper.Map<FeedBack>(inputDto);
            var createdFeedBack = await feedBackRepository.AddFeedBack(feedBackEntity);
            if (createdFeedBack == null)
            {
                throw new Exception("Failed to create feedback.");
            }
            return createdFeedBack;
        }

        public async Task<List<FeedBackReadDto>> GetProductFeedBackById(int ProductId)
        {
            var feedBack = await feedBackRepository.GetFeedBacksByProductId(ProductId);
            if (feedBack == null)
            {
                throw new Exception("Feedback not found.");
            }

            return mapper.Map<List<FeedBackReadDto>>(feedBack);
        }

        public async Task<FeedBackReadDto> UpdateFeedBack(int userId, int id, FeedBackUpdateDto updateDto)
        {
            var existingFeedBack = await feedBackRepository.GetFeedBacksId(id);
            if (existingFeedBack == null)
            {
                throw new Exception("Feedback not found.");
            }
            if (existingFeedBack.AppUserId != userId)
                throw new Exception("You not Allowed");

            if (updateDto.Rating < 1 || updateDto.Rating > 5)
                throw new Exception("Rating must be between 1 and 5");
            mapper.Map(updateDto, existingFeedBack);
            var updatedFeedBack = await feedBackRepository.UpdateFeedBack(existingFeedBack);
            return mapper.Map<FeedBackReadDto>(updatedFeedBack);
        }
    }
}
