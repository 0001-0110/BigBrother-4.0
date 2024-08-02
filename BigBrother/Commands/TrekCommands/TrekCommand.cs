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
            public TrekGame? Game { get; }
            public Dictionary<ulong, Player> Players { get; }

            public Room(TrekGame game)
            {
                Game = game;
                Players = new Dictionary<ulong, Player>();
            }
        }

        private readonly GuildSettingsRepository _guildSettingsRepository;
        private Dictionary<ulong, Room> _rooms;

        public override string Name => "trek";

        public TrekCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
            _guildSettingsRepository = injector.Instantiate<GuildSettingsRepository>();
            _rooms = new Dictionary<ulong, Room>();
        }

        public override Task Call(ICommandRequest command, params object[] args)
        {
            // TODO Remove
            //return command.Respond("Coming soon !");

            if (!_rooms.TryGetValue(command.Guild.Id, out Room? room) || room.Game == null)
            {
                //string? path = _guildSettingsRepository.GetById(command.Guild.Id)?.TrekPath;
                string? path = "";
                if (path == null)
                    return command.Respond("Trek is not available for this guild");

                _rooms[command.Guild.Id] = new Room(TrekGame.Load(path));
            }

            return base.Call(command, _rooms[command.Guild.Id]);
        }
    }
}
