using dogguesser_backend.Data;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace dogguesser_backend.Service;

public interface IScoreService
{ 
    Task<ScoreDTO> SubmitScoreAsync(ScoreDTO scoreDTO);
    Task<List<ScoreDTO>> GetLeaderboardAsync();
    Task<bool> DeleteScoreAsync(int id);
    
}
