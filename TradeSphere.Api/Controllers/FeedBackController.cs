using TradeSphere.Application.DTOs.FeedBackDto;

namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedBackController(FeedBackUseCase feedBackUseCase) : ControllerBase
    {
        [HttpGet("product/{id}")]
        public async Task<ActionResult<List<FeedBackReadDto>>> GetProductFeedBackById(int id)
        {
            try
            {
                var feedBacks = await feedBackUseCase.GetProductFeedBackById(id);
                return Ok(feedBacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<FeedBackAddDto>> AddFeedBack([FromBody] FeedBackAddDto feedBackAddDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                feedBackAddDto.AppUserId = userId;
                var createdFeedBack = await feedBackUseCase.AddFeedBack(feedBackAddDto);
                return Ok(feedBackAddDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<FeedBackReadDto>> UpdateFeedBack(int id, [FromBody] FeedBackUpdateDto feedBackUpdateDto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized();
                var setid = int.Parse(userId);
                var updatedFeedBack = await feedBackUseCase.UpdateFeedBack(setid, id, feedBackUpdateDto);
                return Ok(updatedFeedBack);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
