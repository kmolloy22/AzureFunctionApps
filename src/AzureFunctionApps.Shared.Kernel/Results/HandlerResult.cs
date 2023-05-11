//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AzureFunctionApps.Shared.Kernel.Results
//{
//    public class HandlerResult
//    {
//        private readonly object _model;

//        protected HandlerResult()
//        {
//            _model = Empty.Value;
//        }

//        protected HandlerResult(object model)
//        {
//            _model = model;
//        }

//        protected HandlerResult(HandlerError error)
//        {
//            var errorType = error.BrokenRules.Length == 1
//                ? $"{error.BrokenRules[0].Type}"
//                : $"{error.BrokenRules.Length} errors";

//            Title = $"{error.GetType().Name} ({errorType})";
//            Error = error;
//        }

//        public string Title { get; set; }

//        public bool IsSuccess => !HasError;

//        public bool HasError => Error != null;

//        public HandlerError Error { get; }

//        public bool HasModel => !IsEmpty;
//        public bool IsEmpty => _model == null || _model is Empty;

//        public T Model<T>()
//        {
//            if (_model is T model)
//                return model;

//            if (_model == null)
//                throw new InvalidOperationException("The handler result model was not set");

//            throw new InvalidCastException($"Not possible to cast {_model.GetType().Name} to {typeof(T).Name}");
//        }

//        public object Model() => _model;

//        /// <summary>
//        /// Verifies if model is of desired type
//        /// </summary>
//        /// <typeparam name="T"> desired type</typeparam>
//        public bool IsModelOfType<T>() => _model is T;
//    }
//}
