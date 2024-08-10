using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using InjectoPatronum;
using RogerRoger.DataAccess.Repositories;
using Trek;
using Trek.Entities;

namespace BigBrother.Commands.TrekCommands
{
    [CommandHandler]
    internal class TrekCommand : SlashCommandHandler
    {
        public class Room
        {
            public TrekGame Game { get; }
            public Dictionary<ulong, Player> Players { get; }

            public Room(TrekGame game)
            {
                Game = game;
                Players = new Dictionary<ulong, Player>();
            }
        }

        private readonly IDependencyInjector _injector;
        private readonly GuildSettingsRepository _guildSettingsRepository;
        private Dictionary<ulong, Room> _rooms;

        public override string Name => "trek";

        public TrekCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
            _injector = injector;
            _guildSettingsRepository = injector.Instantiate<GuildSettingsRepository>();
            _rooms = new Dictionary<ulong, Room>();
        }

        public override async Task Call(ICommandRequest command, params object[] args)
        {
            // Load the game when the first player tries to interact with it
            if (!_rooms.ContainsKey(command.Guild.Id))
            {
                await _logger.LogVerbose("Loading trek game");
                await command.Respond("Loading the game...");
                string? path = _guildSettingsRepository.GetById(command.Guild.Id)?.TrekPath;
                if (path == null)
                {
                    await command.Respond("Trek is not available for this guild");
                    await _logger.LogWarning($"Guild {command.Guild.Name} does not have a valid trek path");
                    return;
                }

                // TODO This line crashes
                _rooms[command.Guild.Id] = new Room(_injector.Execute<TrekGame>(typeof(TrekGame), TrekGame.Load, path)!);
                await _logger.LogVerbose("Loaded trek game");
            }

            await base.Call(command, _rooms[command.Guild.Id]);
        }
    }
}
