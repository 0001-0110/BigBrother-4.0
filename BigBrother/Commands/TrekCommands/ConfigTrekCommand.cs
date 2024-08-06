using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Commands.ConfigCommands;
using BigBrother.Logger;
using InjectoPatronum;
using RogerRoger.Models.Settings;

namespace BigBrother.Commands.TrekCommands
{
    [SubCommandHandler<ConfigCommand>]
    internal class ConfigTrekCommand : SlashSubCommandHandler
    {
        public override string Name => "trek";
        public override string Description => "Set the path to load the trek story. Do not touch if you don't know what you're doing";

        private readonly SlashCommandOption<string> _pathOption = new SlashCommandOption<string>("path", "Don't touch this please", true);

        public ConfigTrekCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override Task Execute(ICommandRequest command, params object[] args)
        {
            GuildSettings? guildSettings = args[0] as GuildSettings;
            guildSettings.TrekPath = _pathOption.GetValue(command);
            return command.Respond($"Path updated");
        }
    }
}
