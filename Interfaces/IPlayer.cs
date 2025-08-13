using GeekQuiz.Helpers;
using GeekQuiz.Models.DTOs;

namespace GeekQuiz.Interfaces
{
    public interface IPlayer
    {
        public Task<ServiceResult<IEnumerable<PlayerDTO>>> GetTopPlayers();
    }
}
