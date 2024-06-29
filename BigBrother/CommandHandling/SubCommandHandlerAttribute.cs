using BigBrother.Extensions;

namespace BigBrother.CommandHandling
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class SubCommandHandlerAttribute : Attribute
	{
		public Type Parent { get; }

		public SubCommandHandlerAttribute(Type parent)
		{
			if (!parent.HasInterface(typeof(ICommandHandlerBase)))
				throw new ArgumentException("The given type is not a valid command handler");

			Parent = parent;
		}
	}
}
