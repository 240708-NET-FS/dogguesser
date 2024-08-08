using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Service;

public interface IGameService
{
    public Task<DogApiImages> GetImages();
}