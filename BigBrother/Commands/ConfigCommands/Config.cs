using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;

namespace BigBrother.Commands.ConfigCommands
{
    [CommandHandler]
    internal class Config : SlashCommandHandler
    {
        public override string Name => "config";
        public override string Description => "Set the bot configuration for this specific guild";

        public Config(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        public override SlashCommandBuilder CreateCommand()
        {
            return base.CreateCommand().WithDefaultMemberPermissions(GuildPermission.Administrator);
        }
    }
}
