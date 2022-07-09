using UnityEngine;

namespace Utils.Logger.Handlers
{
    struct LogItemExt
    {
        public int     Tag;
        public LogType Type;
        public string  Message;
        public Object  Context;

        public LogItemExt(int tag, LogType type, string message)
        {
            Tag     = tag;
            Type    = type;
            Message = message;
            Context = null;
        }

        public LogItemExt(int tag, LogType type, string message, Object context)
        {
            Tag     = tag;
            Type    = type;
            Message = message;
            Context = context;
        }
    }
}