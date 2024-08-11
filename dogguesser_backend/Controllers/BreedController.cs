using dogguesser_backend.Data;
using dogguesser_backend.Models.DTO;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using dogguesser_backend.Service;
using dogguesser_backend.Models;
using dogguesser_backend.Mapping;

[Route("api/[controller]")]
[ApiController]
public class BreedController : ControllerBase
{
    private readonly IBreedService _breedService;

    public BreedController(IBreedService breedService)
    {
        _breedService = breedService;
    }

    [HttpGet()]
    public async Task<IActionResult> DogBreeds()
    {
        var dogs = await _breedService.GetBreedsList();

        var response = dogs.ToBreedList();
        return Ok(response);
    }
}