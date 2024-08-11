using dogguesser_backend.Data;
using dogguesser_backend.Models.DTO;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using dogguesser_backend.Service;

[Route("api/[controller]")]
[ApiController]
public class ScoreController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoreController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitScore(ScoreDTO scoreDTO)
    {
        var submittedScore = await _scoreService.SubmitScoreAsync(scoreDTO);
        return Ok(submittedScore);
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard()
    {
        var leaderboard = await _scoreService.GetLeaderboardAsync();
        return Ok(leaderboard);
    }

     [HttpDelete("DeleteScore/{id}")]
    public async Task<IActionResult> DeleteScore(int id)
    {
        var result = await _scoreService.DeleteScoreAsync(id);

        if (result)
        {
            return NoContent(); // 204 No Content
        }

        return NotFound(); // 404 Not Found
    }
}
