using UnityEngine;

namespace Utils.Logger.Handlers {
    struct LogItemExtension {
        public int     Tag;
        public LogType Type;
        public string  Message;
        public Object  Context;

        public LogItemExtension(int tag, LogType type, string message) {
            Tag     = tag;
            Type    = type;
            Message = message;
            Context = null;
        }

        public LogItemExtension(int tag, LogType type, string message, Object context) {
            Tag     = tag;
            Type    = type;
            Message = message;
            Context = context;
        }
    }
}