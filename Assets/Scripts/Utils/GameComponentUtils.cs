using System;
using System.Collections;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine;
using Utils.GameComponentAttributes;
using Utils.Logger;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class GameComponentUtils
    {
        public static void CheckAttributes<T>(T rawObj, Object context = null)
        {
#if UNITY_EDITOR
            if (!context)
            {
                context = rawObj as Object;
            }
            var isPrefab = false;
            if (context is MonoBehaviour mb)
            {
                isPrefab = string.IsNullOrEmpty(mb.gameObject.scene.name) || PrefabStageUtility.GetCurrentPrefabStage() != null;
            }

            foreach (var fieldInfo in rawObj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                foreach (var attributeRaw in fieldInfo.GetCustomAttributes(true))
                {
                    if (isPrefab && attributeRaw is BaseGameComponentAttribute bgca && !bgca.CheckPrefab)
                    {
                        continue;
                    }

                    switch (attributeRaw)
                    {
                        case NotNullAttribute _:
                        {
                            var valueRaw = fieldInfo.GetValue(rawObj);
                            if (valueRaw == null || valueRaw is Object unityObj && !unityObj)
                            {
                                Log.TraceErrorFormat(LogTag.Behaviour, context, "'{0}' is null", fieldInfo.Name);
                            }
                            break;
                        }
                        case NotNullOrEmptyAttribute attr:
                        {
                            var valueRaw = fieldInfo.GetValue(rawObj);
                            switch (valueRaw)
                            {
                                case ICollection collection:
                                {
                                    if (collection.Count == 0)
                                    {
                                        Log.TraceErrorFormat(LogTag.Behaviour, context, "Collection '{0}' is empty",
                                            fieldInfo.Name);
                                    }
                                    if (!attr.AllowNullNodes)
                                        foreach (var node in collection)
                                        {
                                            if (node is Object unityObj)
                                            {
                                                if (!unityObj)
                                                {
                                                    Log.TraceErrorFormat(LogTag.Behaviour, context,
                                                        "Collection '{0}' has null elements", fieldInfo.Name);
                                                    break;
                                                }
                                            }
                                            else if (node == null)
                                            {
                                                Log.TraceErrorFormat(LogTag.Behaviour, context,
                                                    "Collection '{0}' has null elements", fieldInfo.Name);
                                                break;
                                            }
                                        }
                                    break;
                                }
                                case Object valueObj:
                                {
                                    if (!valueObj)
                                    {
                                        Log.TraceErrorFormat(LogTag.Behaviour, context, "'{0}' is null",
                                            fieldInfo.Name);
                                    }
                                    break;
                                }
                                case string str:
                                {
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        Log.TraceErrorFormat(LogTag.Behaviour, context, "'{0}' is null or empty",
                                            fieldInfo.Name);
                                    }
                                    break;
                                }
                                default:
                                {
                                    if (valueRaw == null)
                                    {
                                        Log.TraceErrorFormat(LogTag.Behaviour, context, "'{0}' is null",
                                            fieldInfo.Name);
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                        case CountAttribute attr:
                        {
                            var valueRaw = fieldInfo.GetValue(rawObj);
                            if (valueRaw is ICollection collection)
                            {
                                if (attr.MinCount > 0 && collection.Count < attr.MinCount)
                                {
                                    Log.TraceErrorFormat(LogTag.Behaviour, context,
                                        "Collection '{0}' has less than '{1}' elements", fieldInfo.Name,
                                        attr.MinCount);
                                }

                                if (attr.MaxCount > 0 && collection.Count > attr.MaxCount)
                                {
                                    Log.TraceErrorFormat(LogTag.Behaviour, context,
                                        "Collection '{0}' has more than '{1}' elements", fieldInfo.Name,
                                        attr.MaxCount);
                                }

                                if (attr.ExactCount > 0 && collection.Count != attr.ExactCount)
                                {
                                    Log.TraceErrorFormat(LogTag.Behaviour, context,
                                        "Collection '{0}' must have '{1}' elements", fieldInfo.Name,
                                        attr.ExactCount);
                                }
                            }
                            else
                            {
                                Log.TraceErrorFormat(LogTag.Behaviour, context,
                                    "Field '{0}' has invalid type for attribute Count", fieldInfo.Name);
                            }
                            break;
                        }
                    }
                }
            }

            foreach (var propertyInfo in rawObj.GetType().GetProperties())
            {
                foreach (var attributeRaw in propertyInfo.GetCustomAttributes(true))
                {
                    if (!(attributeRaw is BaseGameComponentAttribute bgca))
                    {
                        continue;
                    }

                    if (isPrefab && !bgca.CheckPrefab)
                    {
                        continue;
                    }

                    switch (attributeRaw)
                    {
                        case IsTrueAttribute _:
                        {
                            try
                            {
                                if (!(bool)propertyInfo.GetValue(rawObj))
                                {
                                    Log.TraceErrorFormat(LogTag.Behaviour, context,
                                        "Property '{0}' on '{1}' is false", propertyInfo.Name, context);
                                }
                            }
                            catch (InvalidCastException e)
                            {
                                Log.TraceErrorFormat(LogTag.Utils,
                                    "GameComponentUtils.CheckAttributes: InvalidCastException when checking IsTrueAttribute. Was the attribute set on a non-boolean property?\n{0}",
                                    e.Message);
                            }
                            catch (Exception e)
                            {
                                Log.TraceException(e);
                            }

                            break;
                        }
                    }
                }
            }
#endif
        }
    }
}