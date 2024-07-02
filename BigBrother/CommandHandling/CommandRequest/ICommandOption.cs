namespace BigBrother.CommandHandling.CommandRequest
{
    internal interface ICommandOption<T>
    {
        T GetValue(ICommandRequest command);
    }
}
