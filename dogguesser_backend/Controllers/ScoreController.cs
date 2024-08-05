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
}
