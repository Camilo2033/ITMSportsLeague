using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentSponsorRepository : IGenericRepository<TournamentSponsor>
    {
        Task<TournamentSponsor?> GetByTournamentAndSponsorAsync(int tournamentId, int sponsorId);

        Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId);

        Task<IEnumerable<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId);
    }

}
