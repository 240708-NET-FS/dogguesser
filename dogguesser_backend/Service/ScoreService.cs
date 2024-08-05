using dogguesser_backend.Data;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
namespace dogguesser_backend.Service;
public class ScoreService : IScoreService
{
    private readonly ApplicationDbContext _context;

    public ScoreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Score> SubmitScoreAsync(Score score)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
        return score;
    }

    public async Task<List<Score>> GetLeaderboardAsync()
    {
        return await _context.Scores
            .Include(s => s.User)
            .OrderByDescending(s => s.ScoreValue)
            .ToListAsync();
    }
}
