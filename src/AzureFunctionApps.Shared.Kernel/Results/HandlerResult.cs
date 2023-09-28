using AzureFunctionApps.Shared.Kernel.Errors;

namespace AzureFunctionApps.Shared.Kernel.Results
{
    public class HandlerResult
    {
        private readonly object _model;

        public HandlerResult()
        {
            _model = Empty.Value;
        }

        public HandlerResult(object model)
        {
            _model = model;
        }

        public HandlerResult(HandlerError error)
        {
            var errorType = error.ErrorRules.Length == 1
                ? $"{error.ErrorRules[0].Type}"
                : $"{error.ErrorRules.Length} errors";

            Title = $"{error.GetType().Name} ({errorType})";
            Error = error;
        }

        public string Title { get; set; }

        public bool IsSuccess => !HasError;

        public bool HasError => Error != null;

        public HandlerError Error { get; }

        public bool HasModel => !IsEmpty;
        public bool IsEmpty => _model == null || _model is Empty;

        public T Model<T>()
        {
            if (_model is T model)
                return model;

            if (_model == null)
                throw new InvalidOperationException("The handler result model was not set");

            throw new InvalidCastException($"Not possible to cast {_model.GetType().Name} to {typeof(T).Name}");
        }

        public object Model() => _model;

        /// <summary>
        /// Verifies if model is of desired type
        /// </summary>
        /// <typeparam name="T"> desired type</typeparam>
        public bool IsModelOfType<T>() => _model is T;
    }
}