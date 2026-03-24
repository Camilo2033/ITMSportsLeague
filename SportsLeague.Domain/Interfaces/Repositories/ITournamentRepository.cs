using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentRepository : IGenericRepository<Tournament>

    {

        Task<IEnumerable<Tournament>> GetByStatusAsync(TournamentStatus status);

        Task<Tournament?> GetByIdWithTeamsAsync(int id);

    }
}
