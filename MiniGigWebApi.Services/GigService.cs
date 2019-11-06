namespace MiniGigWebApi.Services
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using MiniGigWebApi.Domain;
    using MiniGigWebApi.SharedKernel.Data;

    public class GigService : IGigService
    {
        private readonly IGenericRepository<Gig> gigRepository;
        private readonly IGenericRepository<MusicGenre> musicGenreRepository;

        public GigService(IGenericRepository<Gig> gigRepository, IGenericRepository<MusicGenre> musicGenreRepository)
        {
            this.gigRepository = gigRepository;
            this.musicGenreRepository = musicGenreRepository;
        }

        public async Task<List<Gig>> GetGigs(int page, int pageSize)
        {
            var query = this.gigRepository.GetAllIncluding(g => g.MusicGenre);

            if (page > 0 && pageSize > 0)
            {
                query = query.OrderByDescending(c => c.GigDate).Skip(pageSize * (page - 1)).Take(pageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<Gig> GetGigDetails(int id)
        {
            var gig = (await this.gigRepository.FindByIncludeAsync(c => c.Id == id, c => c.MusicGenre)).FirstOrDefault();             
            return gig;
        }

        public async Task<Gig> GetGig(int id)
        {
            var gig = await this.gigRepository.FindByKeyAsync(id);
            return gig;
        }

        public async Task InsertGig(Gig gig)
        {
            await this.gigRepository.InsertAsync(gig);
        }

        public async Task UpdateGig(Gig gig)
        {
            await this.gigRepository.UpdateAsync(gig);
        }

        public async Task DeleteGig(Gig gig)
        {
            await this.gigRepository.DeleteAsync(gig);
        }
    }
}
