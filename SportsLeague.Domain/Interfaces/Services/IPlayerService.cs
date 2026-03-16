using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IPlayerService

    {

        Task<IEnumerable<Player>> GetAllAsync();

        Task<Player?> GetByIdAsync(int id);

        Task<IEnumerable<Player>> GetByTeamAsync(int teamId);

        Task<Player> CreateAsync(Player player);

        Task UpdateAsync(int id, Player player);

        Task DeleteAsync(int id);

    }
}
