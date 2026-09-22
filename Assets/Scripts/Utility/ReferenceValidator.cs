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
            ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Component
        {
#if UNITY_EDITOR
            component ??= gameObject.GetComponent<TComponent>();
            if (component == null && gameObject != null) Logging.LogNullReferenceError(nameof(component), severity, gameObject);
            else Logging.LogNullReferenceError(nameof(component), severity, gameObject);
            if (preventPlay) EditorApplication.isPlaying = false;
#endif
        }

        public static void CheckComponentForNull<TComponent>(ref TComponent component, GameObject gameObject,
            string componentName, ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Component
        {
#if UNITY_EDITOR
            component ??= gameObject.GetComponent<TComponent>();
            if (component == null && gameObject != null) Logging.LogNullReferenceError(componentName, severity, gameObject);
            if (preventPlay) EditorApplication.isPlaying = false;
#endif
        }

        public static void CheckGameObjectForNull<TComponent>(ref TComponent component,
            ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Object
        {
#if UNITY_EDITOR
            if (component == null) Logging.LogNullReferenceError(nameof(component), severity);
            if (preventPlay) EditorApplication.isPlaying = false;
#endif
        }

        public static void CheckGameObjectForNull<TComponent>(ref TComponent component, string componentName,
            ErrorSeverity severity = ErrorSeverity.Error, bool preventPlay = false) where TComponent : Object
        {
#if UNITY_EDITOR
            if (component == null) Logging.LogNullReferenceError(componentName, severity);
            if (preventPlay) EditorApplication.isPlaying = false;
#endif
        }
    }   

}