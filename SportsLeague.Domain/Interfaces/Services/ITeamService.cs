using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ITeamService

    {

        Task<IEnumerable<Team>> GetAllAsync();

        Task<Team?> GetByIdAsync(int id);

        Task<Team> CreateAsync(Team team);

        Task UpdateAsync(int id, Team team);

        Task DeleteAsync(int id);

    }
}
