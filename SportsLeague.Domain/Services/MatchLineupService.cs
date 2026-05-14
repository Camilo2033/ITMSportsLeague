using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchLineupRepository _lineupRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<MatchLineupService> _logger;

        public MatchLineupService(
            IMatchLineupRepository lineupRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            ILogger<MatchLineupService> logger)
        {
            _lineupRepository = lineupRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<MatchLineup> AddPlayerToLineupAsync(int matchId, MatchLineup lineup)
        {
            // V1 – El partido debe existir
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            // V6 – El partido debe estar en estado Scheduled
            if (match.Status != MatchStatus.Scheduled)
                throw new InvalidOperationException(
                    "Solo se pueden registrar alineaciones en partidos Scheduled");

            // V2 – El jugador debe existir
            var player = await _playerRepository.GetByIdAsync(lineup.PlayerId);
            if (player == null)
                throw new KeyNotFoundException($"No se encontró el jugador con ID {lineup.PlayerId}");

            // V3 – El jugador debe pertenecer al HomeTeam o AwayTeam del partido
            if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
                throw new InvalidOperationException(
                    "El jugador no pertenece a ninguno de los equipos del partido");

            // V4 – El jugador no puede estar registrado dos veces en la misma alineación
            var alreadyRegistered = await _lineupRepository
                .ExistsByMatchAndPlayerAsync(matchId, lineup.PlayerId);
            if (alreadyRegistered)
                throw new InvalidOperationException(
                    "El jugador ya está registrado en la alineación de este partido");

            // V5 – Máximo 11 titulares por equipo por partido
            if (lineup.IsStarter)
            {
                var starterCount = await _lineupRepository
                    .CountStartersByMatchAndTeamAsync(matchId, player.TeamId);
                if (starterCount >= 11)
                    throw new InvalidOperationException(
                        "El equipo ya tiene 11 titulares registrados en este partido");
            }

            lineup.MatchId = matchId;

            _logger.LogInformation(
                "Adding player {PlayerId} to lineup of match {MatchId}. IsStarter: {IsStarter}",
                lineup.PlayerId, matchId, lineup.IsStarter);

            return await _lineupRepository.CreateAsync(lineup);
        }

        public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAsync(int matchId)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            return await _lineupRepository.GetByMatchAsync(matchId);
        }

        public async Task<IEnumerable<MatchLineup>> GetLineupByMatchAndTeamAsync(
            int matchId, int teamId)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            return await _lineupRepository.GetByMatchAndTeamAsync(matchId, teamId);
        }

        public async Task DeleteLineupEntryAsync(int matchId, int lineupId)
        {
            var entry = await _lineupRepository.GetByIdAsync(lineupId);
            if (entry == null || entry.MatchId != matchId)
                throw new KeyNotFoundException(
                    $"No se encontró el registro de alineación con ID {lineupId}");

            await _lineupRepository.DeleteAsync(lineupId);
        }
    }

}
