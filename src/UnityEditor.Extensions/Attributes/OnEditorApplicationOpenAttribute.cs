using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace UnityEditor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OnEditorApplicationOpenAttribute : CallbackOrderAttribute
    {
        private const string UnityEditorApplicationOpenKey = "EnityEditor.ApplicationOpen";

        public OnEditorApplicationOpenAttribute() { }

        public OnEditorApplicationOpenAttribute(int callbackOrder)
        {
            this.m_CallbackOrder = callbackOrder;
        }

        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {

            if (!EditorPrefs.HasKey(UnityEditorApplicationOpenKey))
            {
                EditorPrefs.SetBool(UnityEditorApplicationOpenKey, true);

                foreach (var mInfo in AppDomain.CurrentDomain
                       .GetAssemblies()
                       .SelectMany(o => o.GetTypes())
                       .SelectMany(o => o.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                       .Where(o => o.IsDefined(typeof(OnEditorApplicationOpenAttribute), false) && o.GetParameters().Length == 0)
                       .OrderBy(o => ((OnEditorApplicationOpenAttribute)o.GetCustomAttributes(typeof(OnEditorApplicationOpenAttribute), false)[0]).m_CallbackOrder))
                {
                    mInfo.Invoke(null, null);
                }
            }
            EditorApplication.quitting += EditorApplication_quitting;

        }
        private static void EditorApplication_quitting()
        {
            EditorApplication.quitting -= EditorApplication_quitting;
            EditorPrefs.DeleteKey(UnityEditorApplicationOpenKey);
        }

    }
}