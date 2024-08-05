using dogguesser_backend.Data;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
namespace dogguesser_backend.Service;

public interface IScoreService
{ 
    Task<Score> SubmitScoreAsync(Score score);
    Task<List<Score>> GetLeaderboardAsync();
}
