namespace AzureFunctionApps.Shared.Kernel
{
    public class Empty
    {
        public static readonly Empty Value = new Empty();

        public static Task<Empty> TaskValue => Task.FromResult(Value);
    }
}
