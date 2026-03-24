using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IRefereeRepository : IGenericRepository<Referee>

    {

        Task<IEnumerable<Referee>> GetByNationalityAsync(string nationality);

    }
}
