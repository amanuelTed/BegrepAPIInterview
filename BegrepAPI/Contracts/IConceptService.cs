using BegrepAPI.Models;

namespace BegrepAPI.Contracts
{
    public interface IConceptService
    {
        Task<List<Concept>> GetAllConceptsAsync(int page);
        Task<Concept> GetConceptAsync(string id);
    }
}
