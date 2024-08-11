using dogguesser_backend.Data;
using dogguesser_backend.Models.DTO;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using dogguesser_backend.Service;
using dogguesser_backend.Models;
using dogguesser_backend.Mapping;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet()]
    public async Task<IActionResult> MakeRounds()
    {
        var images = await _gameService.GetImages();

        var response = images.ToGame();
        return Ok(response);
    }
}