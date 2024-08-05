using dogguesser_backend.Data;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
namespace dogguesser_backend.Service;

public interface IScoreService
{ 
    Task<ScoreDTO> SubmitScoreAsync(ScoreDTO scoreDTO);
    Task<List<ScoreDTO>> GetLeaderboardAsync();
}
