using GeekQuiz.Helpers;
using GeekQuiz.Interfaces;
using GeekQuiz.Models;
using GeekQuiz.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GeekQuiz.Services
{
    public class PlayerService : IPlayer
    {
        private readonly GeekQuizContext _dbContext;
        public PlayerService(GeekQuizContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResult<IEnumerable<PlayerDTO>>> GetTopPlayers()
        {
            var topPlayersList = new List<PlayerDTO>();

            var topPlayers = _dbContext.Players
                .Select(e => new { 
                    PlayerId = e.PlayerId,
                    PlayerName = e.Username,
                    Score = e.TotalScore,
                })
                .OrderByDescending(e => e.Score)
                .Take(10)
                .ToList();

            foreach (var player in topPlayers)
            {
                var topCategory = await _dbContext.Scores
                    .Where(e => e.PlayerId == player.PlayerId)
                    .GroupBy(e => new { e.CategoryId, e.Category.Name })
                    .Select(e => new
                    {
                        CategoryName = e.Key.Name,
                        TimesPlayed = e.Count(),
                    })
                    .OrderByDescending(e => e.TimesPlayed)
                    .FirstOrDefaultAsync();

                topPlayersList.Add(new PlayerDTO
                {
                    PlayerName = player.PlayerName,
                    Score = player.Score,
                    MostPlayedCategoryName = topCategory?.CategoryName ?? "No hay infromación",
                    TimesPlayed = topCategory?.TimesPlayed ?? 0 
                });

            }

            return ServiceResult<IEnumerable<PlayerDTO>>.Ok(topPlayersList);
        }
    }
}
