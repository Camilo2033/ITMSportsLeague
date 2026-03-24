using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentTeamRepository : IGenericRepository<TournamentTeam>

    {

        Task<TournamentTeam?> GetByTournamentAndTeamAsync(int tournamentId, int teamId);

        Task<IEnumerable<TournamentTeam>> GetByTournamentAsync(int tournamentId);

    }
}
