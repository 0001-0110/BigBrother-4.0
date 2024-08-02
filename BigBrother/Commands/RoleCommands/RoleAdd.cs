using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;

namespace BigBrother.Commands.RoleCommands
{
    [SubCommandHandler<Role>]
    internal class RoleAdd : SlashSubCommandHandler
    {
        public override string Name => "add";
        public override string Description => "Get the requested role, if available";

        private readonly SlashCommandOption<IRole> _roleOption = new SlashCommandOption<IRole>("role", "The role you want to get", true);

        public RoleAdd(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override async Task Execute(ICommandRequest command, params object[] args)
        {
            IRole role = _roleOption.GetValue(command)!;

            if (false)
            {
                await command.Respond("This role is not freely available");
                return;
            }

            await (command.Sender as IGuildUser)!.AddRoleAsync(role);
            await command.Respond($"You now have the role {role.Name}");
        }
    }
}
