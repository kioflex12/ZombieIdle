using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using Utils.Extensions;

namespace Utils.Logger
{
    public sealed class LogTagUtility
    {
        private Dictionary<int, string> _valueToName;
        private int[]                   _keys;
        private string[]                _values;

        public int[] Values
        {
            get
            {
                TryInit();
                return _keys;
            }
        }

        public string[] Names
        {
            get {
                TryInit();
                return _values;
            }
        }

        public bool Contains(int key)
        {
            TryInit();
            return _valueToName.ContainsKey(key);
        }

        [CanBeNull]
        public string GetName(int key)
        {
            TryInit();
            return _valueToName.GetOrElse(key, string.Empty);
        }

        void TryInit() {
            if ( _valueToName != null )
            {
                return;
            }
            _valueToName = FillTagsFromContext();
            _keys        = _valueToName.Keys.ToArray();
            _values      = _valueToName.Values.ToArray();
        }

        static Dictionary<int, string> FillTagsFromContext()
        {
            var dict = new Dictionary<int, string>();
            AddConstantsFromType(dict, typeof(LogTag));
            return dict;
        }

        static void AddConstantsFromType(Dictionary<int, string> dict, Type type)
        {
            if ( type == null )
            {
                return;
            }
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach ( var field in fields ) {
                if ( field.FieldType != typeof(int) )
                {
                    continue;
                }
                if ( field.IsLiteral && !field.IsInitOnly )
                {
                    var key = (int)field.GetValue(null);
                    var value = field.Name;
                    AddTag(dict, key, value);
                }
            }
        }

        static void AddTag(Dictionary<int, string> dict, int key, string value)
        {
            if ( dict.ContainsKey(key) ) {
                Debug.LogErrorFormat(
                    "Duplicated log tags for {0}: '{1}' and '{2}'",
                    key.ToString(), value, dict[key]
                );
            }
            dict[key] = value;
        }

    }
}

