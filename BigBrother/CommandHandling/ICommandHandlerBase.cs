﻿namespace BigBrother.CommandHandling
{
	internal interface ICommandHandlerBase
	{
		string Name { get; }
		string Description { get; }

		Task Call(ICommandRequest command);
	}
}
