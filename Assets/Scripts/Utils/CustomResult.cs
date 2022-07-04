using System;
using JetBrains.Annotations;

namespace Utils
{
    public abstract class CustomResult
    {
        public bool Success { get; }

        [CanBeNull]
        public Exception Exception { get; }

        protected CustomResult(bool success, [CanBeNull] Exception exception)
        {
            Success = success;
            Exception = exception;
        }
    }
}

