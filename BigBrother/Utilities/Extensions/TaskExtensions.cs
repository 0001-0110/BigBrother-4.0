namespace BigBrother.Utilities.Extensions
{
    internal static class TaskExtensions
    {
        public static T AwaitSync<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}
