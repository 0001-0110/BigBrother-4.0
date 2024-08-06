using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using InjectoPatronum;
using Trek.Entities;
using static BigBrother.Commands.TrekCommands.TrekCommand;

namespace BigBrother.Commands.TrekCommands
{
    [SubCommandHandler<TrekCommand>]
    internal class TrekActionCommand : SlashSubCommandHandler
    {
        public override string Name => "action";

        public override string Description => "Execute an action";

        private readonly SlashCommandOption<string> _actionOption = new SlashCommandOption<string>("action", "the action you want to perform", true);

        public TrekActionCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override Task Execute(ICommandRequest command, params object[] args)
        {
            Room room = (Room)args[0];
            if (!room.Players.TryGetValue(command.Sender.Id, out Player? player))
            {
                return command.Respond("You are not part of the game yet. Please join using `trek join`");
            }

            return command.Respond(room.Game.ExecuteAction(player, _actionOption.GetValue(command)!));
        }
    }
}
