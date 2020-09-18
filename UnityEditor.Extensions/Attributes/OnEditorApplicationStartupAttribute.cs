using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Reflection.Extensions;

namespace UnityEditor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OnEditorApplicationStartupAttribute : CallbackOrderAttribute
    {
        private const string UnityEditorApplicationOpenKey = "UnityEditor.ApplicationStartup";

        public OnEditorApplicationStartupAttribute() { }

        public OnEditorApplicationStartupAttribute(int callbackOrder)
        {
            this.m_CallbackOrder = callbackOrder;
        }

        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            EditorApplication.quitting -= EditorApplication_quitting;
            EditorApplication.quitting += EditorApplication_quitting;

            if (!EditorPrefs.HasKey(UnityEditorApplicationOpenKey))
            {
                EditorPrefs.SetBool(UnityEditorApplicationOpenKey, true);

                Type originType = typeof(OnEditorApplicationStartupAttribute);

                Dictionary<MethodInfo, int> methods = new Dictionary<MethodInfo, int>();
                FieldInfo m_CallbackOrderField = typeof(CallbackOrderAttribute).GetField("m_CallbackOrder", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (Type attrType in FindTypes(new string[] { originType.FullName }, typeof(Attribute)))
                {
                    bool isCallbackOrder = attrType.IsSubclassOf(typeof(CallbackOrderAttribute));
                    foreach (var mInfo in AppDomain.CurrentDomain
                           .GetAssemblies()
                           .Referenced(attrType.Assembly)
                           .SelectMany(o => o.GetTypes())
                           .Where(o => !o.IsGenericTypeDefinition)
                           .SelectMany(o => o.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                           .Where(o => o.IsDefined(attrType, false) && o.GetParameters().Length == 0)
                           )
                    {
                        int order = 0;
                        if (isCallbackOrder)
                        {
                            Attribute attr = ((CallbackOrderAttribute)mInfo.GetCustomAttribute(attrType, false));
                            order = (int)m_CallbackOrderField.GetValue(attr);
                        }
                        methods[mInfo] = order;
                    }
                }

                foreach (var mInfo in methods.OrderBy(o => o.Value).Select(o => o.Key))
                {
                    try
                    {
                        mInfo.Invoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }

                }

            }

        }

        static IEnumerable<Type> FindTypes(string[] typeNames, Type baseType)
        {
            HashSet<Type> types = new HashSet<Type>();

            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var typeName in typeNames)
                {
                    Type type = ass.GetType(typeName);
                    if (type == null)
                        continue;
                    if (baseType != null && !type.IsSubclassOf(baseType))
                        continue;
                    types.Add(type);
                }
            }
            return types;
        }

        private static void EditorApplication_quitting()
        {
            EditorApplication.quitting -= EditorApplication_quitting;
            EditorPrefs.DeleteKey(UnityEditorApplicationOpenKey);
        }

    }
}