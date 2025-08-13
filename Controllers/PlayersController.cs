using GeekQuiz.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlayersController : ControllerBase
    {

        private readonly IPlayer _playerService;

        public PlayersController(IPlayer playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopPlayers()
        {
            var result = await _playerService.GetTopPlayers();

            if (!result.Status)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
