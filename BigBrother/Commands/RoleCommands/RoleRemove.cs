using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling;
using BigBrother.Logger;
using InjectoPatronum;
using BigBrother.CommandHandling.CommandRequest;
using Discord;

namespace BigBrother.Commands.RoleCommands
{
    [SubCommandHandler<Role>]
    internal class RoleRemove : SlashSubCommandHandler
    {
        public override string Name => "remove";
        public override string Description => "Remove your role";

        private readonly SlashCommandOption<IRole> _roleOption = new SlashCommandOption<IRole>("role", "The role you want to remove", true);

        public RoleRemove(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override async Task Execute(ICommandRequest command, params object[] args)
        {
            IRole role = _roleOption.GetValue(command)!;

            if (false)
            {
                await command.Respond("This role is not freely available");
                return;
            }

            await (command.Sender as IGuildUser)!.RemoveRoleAsync(role);
            await command.Respond($"Role {role.Name} was removed");
        }
    }
}
