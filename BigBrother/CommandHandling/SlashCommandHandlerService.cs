using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord.WebSocket;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
    internal class SlashCommandHandlerService : CommandHandlerService<SlashCommandHandler>
    {
        public SlashCommandHandlerService(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        public override async Task CreateCommands(DiscordSocketClient client)
        {
            foreach (SocketGuild? guild in client.Guilds)
            {
                // TODO For now, all commands are active everywhere
                IEnumerable<string> activeCommands = _commandHandlers.Keys;

                await guild.BulkOverwriteApplicationCommandAsync(activeCommands.Select(command =>
                {
                    if (!_commandHandlers.TryGetValue(command, out SlashCommandHandler? commandHandler))
                    {
                        _logger.LogError($"The guild {client.GetGuild(guild.Id).Name} ({guild.Id}) requested a command that does not exist ({command})");
                        return null;
                    }

                    return commandHandler.CreateCommand().Build();
                }).Where(commandHandler => commandHandler != null).ToArray());
            }
        }

        public override Task ExecuteCommand(ICommandRequest command)
        {
            _logger.LogInfo($"Received command request {command}");
            Task.Run(async () =>
            {
                try
                {
                    await _commandHandlers[command.Name].Call(command);
                }
                catch (Exception exception)
                {
                    await _logger.LogError($"An error occured during the handling of the command ({command})", exception);
                }
            });
            return Task.CompletedTask;
        }
    }
}
