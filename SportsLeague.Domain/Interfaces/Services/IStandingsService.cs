using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IStandingsService

    {

        Task<object> GetStandingsAsync(int tournamentId);

        Task<object> GetTopScorersAsync(int tournamentId);

        Task<object> GetCardStatsAsync(int tournamentId);

    }
}
