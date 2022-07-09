using UnityEngine;

using System;
using System.Diagnostics;

namespace Utils.Exceptions
{
    public class WrapperException : UnityException
    {
        private readonly string _entryStackTrace;

        public override string StackTrace
        {
            get
            {
                var baseStackTrace = base.StackTrace;
                if ( string.IsNullOrEmpty(baseStackTrace) )
                {
                    return _entryStackTrace;
                }
                return baseStackTrace;
            }
        }

        protected WrapperException(string message) : base(message)
        {
            _entryStackTrace = InitStackTrace();
        }

        protected WrapperException(string message, Exception innerException) : base(message, innerException)
        {
            _entryStackTrace = InitStackTrace();
        }

        WrapperException(Exception innerException) :
            base(
                string.Format(
                    "Exception of type '{0}' with missing stacktrace (trace frames recorded).",
                    innerException.GetType().Name),
                innerException)
        {
            _entryStackTrace = InitStackTrace(5); // Skip log calls
        }

        WrapperException(string message, string stacktrace) : base(message) {
            _entryStackTrace = stacktrace;
        }

        string InitStackTrace(int skipFrames = 2)
        {
            // Make stacktrace without derived and this constructor calls
            var st = new StackTrace(skipFrames: skipFrames, fNeedFileInfo: true);
            return st.ToString();
        }

        public static WrapperException Wrap(Exception exception)
        {
            return new WrapperException(exception);
        }

        public static WrapperException Wrap(string message, string stacktrace)
        {
            return new WrapperException(message, stacktrace);
        }
    }
}

