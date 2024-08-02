using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using InjectoPatronum;
using static BigBrother.Commands.TrekCommands.TrekCommand;

namespace BigBrother.Commands.TrekCommands
{
    [SubCommandHandler<TrekCommand>]
    internal class TrekStatus : SlashSubCommandHandler
    {
        public override string Name => "status";

        public override string Description => "Get your current status";

        public TrekStatus(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override Task Execute(ICommandRequest command, params object[] args)
        {
            Room room = (Room)args[0];
            return command.Respond(room.Players.GetValueOrDefault(command.Sender.Id)?.GetStatus() ?? "You did join the game yet, please use `trek join` first");
        }
    }
}
