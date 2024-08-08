using dogguesser_backend.Data;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dogguesser_backend.Service;

public interface IBreedService
{
    public Task<DogApiBreedList> GetBreedsList();
}