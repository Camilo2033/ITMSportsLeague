using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>

    {

        Task<Team?> GetByNameAsync(string name);

        Task<IEnumerable<Team>> GetByCityAsync(string city);

    }
}
