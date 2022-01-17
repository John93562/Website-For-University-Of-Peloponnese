using DeluzionalPenguinz.API.Services;
using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeluzionalPenguinz.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("DeluzionalPenguinz")]
    [Authorize]
    [ApiController]
    public class AnouncementsController : ControllerBase
    {
        private readonly AnouncementsRepository anouncementsDataService;

        public AnouncementsController(AnouncementsRepository anouncementsDataService)
        {
            this.anouncementsDataService = anouncementsDataService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSingleAnouncement(int id)
        {
           AnouncementDTO response = await anouncementsDataService.GetSingleAnouncement(id);

            return response is null ? BadRequest() : Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnouncements()
        {
            IEnumerable<AnouncementDTO> response = await anouncementsDataService.GetAllAnouncements();

            return response is null ? BadRequest() : Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewAnouncement([FromBody] CreateNewAnouncementResponse anouncement)
        {
            BooleanResponse response = await anouncementsDataService.AddNewAnouncement(anouncement);

            return response is null ? BadRequest() : Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAnAnouncement([FromBody] UpdateAnouncementResponse anouncement)
        {
            BooleanResponse response = await anouncementsDataService.UpdateAnAnouncement(anouncement);

            return response is null ? BadRequest() : Ok(response);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnouncement(int id)
        {
            BooleanResponse response = await anouncementsDataService.DeleteAnAnouncement(id);

            return response is null ? BadRequest() : Ok(response);
        }

    }
}
