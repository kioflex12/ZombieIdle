using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils.Logger.Handlers;
using Object = UnityEngine.Object;

namespace Utils.Logger
{
    public static class Log
    {
        private static UnityTraceHandler Handler => UnityTraceHandler.Instance;

        public static void Trace(int tag, string message)
        {
            Handler.Trace(tag,LogType.Log,message);
        }

        public static void TraceWarning(int tag, string message)
        {
            Handler.Trace(tag,LogType.Warning,message);
        }

        public static void Trace(int tag, LogType type, string message) {
            Handler.Trace(tag, type, message);
        }

        public static void Trace(int tag, LogType type, Object context, string message) {
            Handler.Trace(tag, type, context, message);
        }

        public static void TraceWarning(int tag, Object context, string message) {
            Handler.Trace(tag, LogType.Warning, context, message);
        }

        public static void TraceError(int tag, Object context, string message) {
            Handler.Trace(tag, LogType.Error, context, message);
        }

        [StringFormatMethod("message")]
        public static void TraceFormat<T0>(int tag, string message, T0 arg0) {
            Handler.Trace(tag, LogType.Log, string.Format(message, arg0));
        }

        [StringFormatMethod("message")]
        public static void TraceFormat<T0, T1>(int tag, string message, T0 arg0, T1 arg1) {
            Handler.Trace(tag, LogType.Log, string.Format(message, arg0, arg1));
        }

        [StringFormatMethod("message")]
        public static void TraceErrorFormat(int tag, string message, params object[] args) {
            Handler.Trace(tag, LogType.Error, string.Format(message, args));
        }

        [StringFormatMethod("message")]
        public static void TraceErrorFormat<T0>(int tag, string message, T0 arg0) {
            Handler.Trace(tag, LogType.Error, string.Format(message, arg0));
        }

        [StringFormatMethod("message")]
        public static void TraceErrorFormat<T0>(int tag, Object context, string message, T0 arg0) {
            Handler.Trace(tag, LogType.Error, context, string.Format(message, arg0));
        }

        [StringFormatMethod("message")]
        public static void TraceErrorFormat(int tag, Object context, string message, params object[] args) {
                Handler.Trace(tag, LogType.Error, context, string.Format(message, args));
        }

        public static Exception TraceException(Exception exception, bool important = true) {
            if ( exception == null ) {
                return null;
            }
            Handler.TraceException(exception, important);
            return exception;
        }
    }
}

