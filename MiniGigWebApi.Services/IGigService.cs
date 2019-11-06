namespace MiniGigWebApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MiniGigWebApi.Domain;

    public interface IGigService
    {
        Task<List<Gig>> GetGigs(int page, int pageSize);

        Task<Gig> GetGig(int id);

        Task<Gig> GetGigDetails(int id);

        Task InsertGig(Gig gig);

        Task UpdateGig(Gig gig);

        Task DeleteGig(Gig gig);
    }
}
