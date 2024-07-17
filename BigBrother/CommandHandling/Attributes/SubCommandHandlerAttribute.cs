using UtilityMinistry.Extensions;

namespace BigBrother.CommandHandling.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class SubCommandHandlerAttribute : Attribute
    {
        public Type Parent { get; }

        protected SubCommandHandlerAttribute(Type parent)
        {
            if (!parent.HasInterface(typeof(ICommandHandlerBase)))
                throw new ArgumentException("The given type is not a valid command handler");

            Parent = parent;
        }
    }

    internal class SubCommandHandlerAttribute<TCommandHandler> : SubCommandHandlerAttribute where TCommandHandler : ICommandHandlerBase
    {
        public SubCommandHandlerAttribute() : base(typeof(TCommandHandler)) { }
    }
}
