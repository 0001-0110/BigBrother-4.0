namespace BigBrother.CommandHandling
{
	internal interface ICommandHandlerBase
	{
		string Name { get; }
		string Description { get; }

		Task Execute(ICommandRequest command);
	}
}
