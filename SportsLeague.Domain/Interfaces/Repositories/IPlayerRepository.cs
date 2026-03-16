using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IPlayerRepository : IGenericRepository<Player>

    {

        Task<IEnumerable<Player>> GetByTeamAsync(int teamId);

        Task<Player?> GetByTeamAndNumberAsync(int teamId, int number);

        Task<IEnumerable<Player>> GetAllWithTeamAsync();

        Task<Player?> GetByIdWithTeamAsync(int id);

    }
}
