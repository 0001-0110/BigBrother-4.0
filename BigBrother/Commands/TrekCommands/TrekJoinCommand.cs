using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord.WebSocket;
using InjectoPatronum;
using Trek.Entities;
using static BigBrother.Commands.TrekCommands.TrekCommand;

namespace BigBrother.Commands.TrekCommands
{
    [SubCommandHandler<TrekCommand>]
    internal class TrekJoinCommand : SlashSubCommandHandler
    {
        public override string Name => "join";

        public override string Description => "Join the game";

        public TrekJoinCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override Task Execute(ICommandRequest command, params object[] args)
        {
            Room room = (Room)args[0];
            if (room.Players.ContainsKey(command.Sender.Id))
                return command.Respond("You already joined the game");

            string message = room.Game.Join((command.Sender as SocketGuildUser).DisplayName, out Player player);
            room.Players[command.Sender.Id] = player;
            return command.Respond(message);
        }
    }
}
