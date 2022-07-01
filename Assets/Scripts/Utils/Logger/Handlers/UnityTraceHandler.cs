using System;
using UnityEngine;
using Utils.Exceptions;
using Object = UnityEngine.Object;

namespace Utils.Logger.Handlers
{
    public sealed class UnityTraceHandler
    {
        readonly LogTagUtility  _utility;

        private static UnityTraceHandler _instance;

        private UnityTraceHandler(LogTagUtility utility) {
            _utility = utility;
        }

        public static UnityTraceHandler Instance => _instance ??= new UnityTraceHandler(new LogTagUtility());

        public void Trace(int tag, LogType type, string message)
        {
            if (tag == LogTag.System)
            {
                return;
            }
            Write(new LogItemExtension(tag, type, message));
        }

        public void Trace(int tag, LogType type, Object context, string message)
        {
            if ( tag == LogTag.System )
            {
                return;
            }
            Write(new LogItemExtension(tag, type, message, context));
        }

        public void TraceException(Exception exception, bool important) {
            if ( string.IsNullOrEmpty(exception.StackTrace) ) {
                exception = WrapperException.Wrap(exception);
            }
            Debug.LogError(exception.ToString());
        }

        private void Write(LogItemExtension item)
        {
            var tagStr = _utility.GetName(item.Tag);
            if ( item.Context == null )
            {
                if ( item.Type == LogType.Log ) {
                    Debug.unityLogger.Log(tagStr, item.Message);
                } else {
                    Debug.unityLogger.Log(item.Type, tagStr, item.Message);
                }
            } else {
                if ( item.Type == LogType.Log ) {
                    Debug.unityLogger.Log(tagStr, item.Message, item.Context);
                } else {
                    Debug.unityLogger.Log(item.Type, tagStr, item.Message, item.Context);
                }
            }
        }
    }
}

