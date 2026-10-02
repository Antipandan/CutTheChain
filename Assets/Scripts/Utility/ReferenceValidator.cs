using System;
using UnityEditor;
using UnityEngine;
using Utility;
using Object = UnityEngine.Object;

namespace Utility
{
    public static class ReferenceValidator
    {
        public static void CheckComponentForNull<TComponent>(ref TComponent component, GameObject gameObject,
            string componentName, ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Component
        {
            component ??= gameObject.GetComponent<TComponent>();
            if (component == null && gameObject != null) Logging.LogNullReferenceError(componentName, severity, gameObject);
            if (preventPlay) EditorApplication.isPlaying = false;
        }
        
        public static void CheckComponentForNull<TComponent>(ref TComponent component, GameObject gameObject,
            ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Component
        {
            CheckComponentForNull(ref component, gameObject, nameof(component), severity, preventPlay);
        }
        
        public static void CheckComponentForNull<TComponent>(ref TComponent component, GameObject gameObject, out bool wasNull,
            string componentName = nameof(Component), ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Component
        {
            component ??= gameObject.GetComponent<TComponent>();
            if (component == null && gameObject != null)
            {
                Logging.LogNullReferenceError(componentName, severity, gameObject);
                wasNull = true;
            }
            else wasNull = false;
            if (preventPlay) EditorApplication.isPlaying = false;
        }

        public static void CheckComponentForNullChild<TComponent>(ref TComponent component, GameObject gameObject,
            ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Component
        {
            component ??= gameObject.GetComponentInChildren<TComponent>();
            if (component == null && gameObject != null) Logging.LogNullReferenceError(nameof(component), severity, gameObject);
            if (preventPlay) EditorApplication.isPlaying = false;
        }

        public static void CheckComponentForNullChild<TComponent>(ref TComponent component, GameObject gameObject,
            string componentName, ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false)
            where TComponent : Component
        {
            component ??= gameObject.GetComponentInChildren<TComponent>();
            if (component == null && gameObject != null) Logging.LogNullReferenceError(componentName, severity, gameObject);
            if (preventPlay) EditorApplication.isPlaying = false;
        }

        public static void CheckUnityObjectForNull<TComponent>(ref TComponent component,
            ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Object
        {
            if (component == null) Logging.LogNullReferenceError(nameof(component), severity);
            if (preventPlay) EditorApplication.isPlaying = false;
        }

        public static void CheckUnityObjectForNull<TComponent>(ref TComponent component, string componentName,
            ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Object
        {
            if (component == null) Logging.LogNullReferenceError(componentName, severity);
            if (preventPlay) EditorApplication.isPlaying = false;
        }
    }   

}