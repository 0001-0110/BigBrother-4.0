namespace BigBrother.CommandHandling
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class SubCommandHandlerAttribute : Attribute
	{
		public Type Parent { get; }

		public SubCommandHandlerAttribute(Type parent)
		{
			Parent = parent;
		}
	}
}
