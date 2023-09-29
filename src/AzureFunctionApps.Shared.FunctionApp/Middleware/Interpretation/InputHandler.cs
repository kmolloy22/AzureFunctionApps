namespace AzureFunctionApps.Shared.FunctionApp.Middleware.Interpretation
{
    public class InputHandler<TRequest> where TRequest : class
    {
        public TRequest Request { get; }

        //public IScopeContext ScopeContext { get; }
        public InputInfo InputInfo { get; }

        public InputHandler(TRequest request, InputInfo inputInfo)
        {
            Request = request;
            //ScopeContext = scopeContext;
            InputInfo = inputInfo;
        }
    }
}