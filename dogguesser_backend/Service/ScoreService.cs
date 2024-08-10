using dogguesser_backend.Data;
using dogguesser_backend.Mapping;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace dogguesser_backend.Service;
public class ScoreService : IScoreService
{
    private readonly ApplicationDbContext _context;

    public ScoreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ScoreDTO> SubmitScoreAsync(ScoreDTO scoreDTO)
    {
        var score = scoreDTO.ToEntity(); // Convert DTO to Entity
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();
            return score.ToDTO(); // Optionally return the DTO of the saved score
    }

    public async Task<List<ScoreDTO>> GetLeaderboardAsync()
    {
        var scores = await _context.Scores
                .Include(s => s.User)
                .OrderByDescending(s => s.ScoreValue)
                .ToListAsync();

            return scores.Select(score => score.ToDTO()).ToList(); 
    }

        public async Task<bool> DeleteScoreAsync(int id)
        {
            var score = await _context.Scores.FindAsync(id);

            if (score == null)
            {
                return false; // Score not found
            }

            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();
            return true; // Deletion successful
        }

    
}
