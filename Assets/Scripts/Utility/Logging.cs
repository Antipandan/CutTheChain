using System;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;


namespace Utility
{
    /// <summary>
    /// Class that contains functions that handle broadly applicable code and code that is boring / is a slog to rewrite
    /// If a code snippet fulfills said criteria, a function probably exists in here
    /// </summary>
    public static class Logging
    {
        #region Public Functions
        
        /// <summary>
        /// Creates a message in the Unity console regarding <see cref="NullReferenceException"/> error with a variable / function.
        /// For now to send the name of the variable, users have to either type the name of the variable / function.
        /// Users can circumvent this by using the 'nameof()' function to send the string name of a variable, function,
        /// class or more. This is to be replaced with the attribute [CallerArgumentExpression] in the future .NET SDKS. 
        /// </summary>
        /// <param name="errorObjectName">Variable that has some sort of error</param>
        /// <param name="parentObject">The UnityEngine.Object that contains
        /// this variable Parameter can be left null</param>
        /// <param name="severity">How severe is the error encountered?</param>
        /// <param name="extraLoggingFunction">Function that can be sent
        /// if the user wants extra logging capability or other</param>
        public static void LogNullReferenceError(string errorObjectName,ErrorSeverity severity = ErrorSeverity.None,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardLoggingOutPut(errorObjectName, severity, ErrorSubject.Reference, parentObject, extraLoggingFunction);
        }

        /// <summary>
        /// Creates a message in the Unity console regarding when a Singleton Object is destroyed.
        /// For now to send the name of the variable, users have to type the name of the class.
        /// Users can circumvent this by using the 'nameof()' function to send the string name of a variable, function,
        /// class or more. This is to be replaced with the attribute [CallerArgumentExpression] in the future .NET SDKS. 
        /// </summary>
        /// <param name="errorObjectName">Variable that has some sort of error</param>
        /// <param name="parentObject">The UnityEngine.Object that contains
        /// this variable Parameter can be left null</param>
        /// <param name="severity">How severe is the error encountered?</param>
        /// <param name="extraLoggingFunction">Function that can be sent
        /// if the user wants extra logging capability or other</param>
        public static void LogSingletonError(string errorObjectName,ErrorSeverity severity = ErrorSeverity.None,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardLoggingOutPut(errorObjectName, severity, ErrorSubject.Singleton, parentObject, extraLoggingFunction);
        }

        /// <summary>
        /// Creates a message in the Unity console regarding a variable having a higher than expected value.
        /// For now to send the name of the variable, users have to type the name of the variable.
        /// Users can circumvent this by using the 'nameof()' function to send the string name of a variable, function,
        /// class or more. This is to be replaced with the attribute [CallerArgumentExpression] in the future .NET SDKS. 
        /// </summary>
        /// <param name="errorObjectName">Variable that has some sort of error</param>
        /// <param name="parentObject">The UnityEngine.Object that contains
        /// this variable Parameter can be left null</param>
        /// <param name="severity">How severe is the error encountered?</param>
        /// <param name="extraLoggingFunction">Function that can be sent
        /// if the user wants extra logging capability or other</param>
        public static void LogPrimitiveHighValueError(string errorObjectName,ErrorSeverity severity = ErrorSeverity.None,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardLoggingOutPut(errorObjectName, severity, ErrorSubject.PrimitivesHighValue, parentObject, extraLoggingFunction);
        }

        /// <summary>
        /// Creates a message in the Unity console regarding a variable having a lower than expected value.
        /// For now to send the name of the variable, users have to type the name of the variable.
        /// Users can circumvent this by using the 'nameof()' function to send the string name of a variable, function,
        /// class or more. This is to be replaced with the attribute [CallerArgumentExpression] in the future .NET SDKS. 
        /// </summary>
        /// <param name="errorObjectName">Variable that has some sort of error</param>
        /// <param name="parentObject">The UnityEngine.Object that contains
        /// this variable Parameter can be left null</param>
        /// <param name="severity">How severe is the error encountered?</param>
        /// <param name="extraLoggingFunction">Function that can be sent
        /// if the user wants extra logging capability or other</param>
        public static void LogPrimitiveLowValueError(string errorObjectName,ErrorSeverity severity = ErrorSeverity.None,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardLoggingOutPut(errorObjectName, severity, ErrorSubject.PrimitivesLowValue, parentObject, extraLoggingFunction);
        }

        /// <summary>
        /// Creates a message in the Unity console regarding a variable having a strange value.
        /// For now to send the name of the variable, users have to type the name of the variable.
        /// Users can circumvent this by using the 'nameof()' function to send the string name of a variable, function,
        /// class or more. This is to be replaced with the attribute [CallerArgumentExpression] in the future .NET SDKS. 
        /// </summary>
        /// <param name="errorObjectName">Variable that has some sort of error</param>
        /// <param name="parentObject">The UnityEngine.Object that contains
        /// this variable Parameter can be left null</param>
        /// <param name="severity">How severe is the error encountered?</param>
        /// <param name="extraLoggingFunction">Function that can be sent
        /// if the user wants extra logging capability or other</param>
        public static void LogPrimitiveOddValueError(string errorObjectName,ErrorSeverity severity = ErrorSeverity.None,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardLoggingOutPut(errorObjectName, severity, ErrorSubject.PrimitivesOddValue, parentObject, extraLoggingFunction);
        }
        
        /// <summary>
        /// Creates a message in the Unity console regarding <see cref="IndexOutOfRangeException"/> error with a collection.
        /// For now to send the name of the variable, users have to either type the name of the collection.
        /// Users can circumvent this by using the 'nameof()' function to send the string name of a variable, function,
        /// class or more. This is to be replaced with the attribute [CallerArgumentExpression] in the future .NET SDKS. 
        /// </summary>
        /// <param name="errorObjectName">Variable that has some sort of error</param>
        /// <param name="parentObject">The UnityEngine.Object that contains
        /// this variable Parameter can be left null</param>
        /// <param name="severity">How severe is the error encountered?</param>
        /// <param name="extraLoggingFunction">Function that can be sent
        /// if the user wants extra logging capability or other</param>
        public static void LogEmptyCollectionError(string errorObjectName,ErrorSeverity severity = ErrorSeverity.None,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardLoggingOutPut(errorObjectName, severity, ErrorSubject.EmptyCollection, parentObject, extraLoggingFunction);
        }

        /// <summary>
        /// Creates a message in the Unity console regarding a general error with a variable / function / other.
        /// For now to send the name of the variable, users have to either type the name of the variable / function.
        /// Users can circumvent this by using the 'nameof()' function to send the string name of a variable, function,
        /// class or more. This is to be replaced with the attribute [CallerArgumentExpression] in the future .NET SDKS. 
        /// </summary>
        /// <param name="errorObjectName">Variable that has some sort of error</param>
        /// <param name="parentObject">The UnityEngine.Object that contains
        /// this variable Parameter can be left null</param>
        /// <param name="severity">How severe is the error encountered?</param>
        /// <param name="extraLoggingFunction">Function that can be sent
        /// if the user wants extra logging capability or other</param>
        public static void LogUnknownError(string errorObjectName,ErrorSeverity severity = ErrorSeverity.None,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardLoggingOutPut(errorObjectName, severity, ErrorSubject.UnknownError, parentObject, extraLoggingFunction);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Prevents the game from being built as an exe
        /// </summary>
        /// <exception cref="BuildFailedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowStandardBuildError()
        {
            return;
            // throw new BuildFailedException("Can Not build game because of missing reference(s)");
        }

        /// <summary>
        /// Method Containing standard responses to parameters defined in method signature. Logs a standardized message
        /// in the unity console hinting at the fact that said error is not serious
        /// </summary>
        /// <param name="errorObjectName">Name of object that caused an error</param>
        /// <param name="error">What kind of error occured?</param>
        /// <param name="parentObject">UnityEngine.Object where this error occured. Consider providing a reference to simplify debugging</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogStandardStringMessageWeak(string errorObjectName, ErrorSubject error = ErrorSubject.Reference, Object parentObject = null)
        {
            switch (error)
            {
                case ErrorSubject.Singleton:
                    Debug.Log($"Object ''{errorObjectName}'' was destroyed because a different object" +
                              $" of the same type already exists in memory", parentObject);
                    break;
                case ErrorSubject.Reference:
                    Debug.Log($"Reference: ''{errorObjectName}'' is null. This reference can be left null", parentObject);
                    break;
                case ErrorSubject.PrimitivesHighValue:
                    Debug.Log($"value of: ''{errorObjectName}'' is too high. Consider lowering the value", parentObject);
                    break;
                case ErrorSubject.PrimitivesLowValue:
                    Debug.Log($"value of: ''{errorObjectName}'' is too low. Consider increasing the value", parentObject);
                    break;
                case ErrorSubject.PrimitivesOddValue:
                    Debug.Log($"value of: ''{errorObjectName}'' was unexpected. Consider changing the value", parentObject);
                    break;
                case ErrorSubject.EmptyCollection:
                    Debug.Log($"Collection: ''{errorObjectName}'' is empty. This collection can be left empty", parentObject);
                    break;
                case ErrorSubject.UnknownError:
                    Debug.Log($"Unknown Error: ''{errorObjectName}''", parentObject);
                    break;
                default:
                    Debug.Log($"The origin of this error is unknown", parentObject);
                    break;
                
            }
        }
        
        /// <summary>
        /// Method Containing standard responses to parameters defined in method signature. Logs a standardized message
        /// in the unity console hinting at the fact that said error can cause issues
        /// </summary>
        /// <param name="errorObjectName">Name of object that caused an error</param>
        /// <param name="error">What kind of error occured?</param>
        /// <param name="parentObject">UnityEngine.Object where this error occured. Consider providing a reference to simplify debugging</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogWarningStandardStringMessage(string errorObjectName, ErrorSubject error = ErrorSubject.Reference, Object parentObject = null)
        {
            switch (error)
            {
                case ErrorSubject.Singleton:
                    Debug.LogWarning($"Object ''{errorObjectName}'' was destroyed because a different object" +
                              $" of the same type already exists in memory", parentObject);
                    break;
                case ErrorSubject.Reference:
                    Debug.LogWarning($"Reference: '{errorObjectName}' is null. Please fill this reference", parentObject);
                    break;
                case ErrorSubject.PrimitivesHighValue:
                    Debug.LogWarning($"value of: '{errorObjectName}' is too high. This value should be lowered", parentObject);
                    break;
                case ErrorSubject.PrimitivesLowValue:
                    Debug.LogWarning($"value of: '{errorObjectName}' is too low. This value should be increased", parentObject);
                    break;
                case ErrorSubject.PrimitivesOddValue:
                    Debug.LogWarning($"value of: '{errorObjectName}' was unexpected. This value should be changed to something else", parentObject);
                    break;
                case ErrorSubject.EmptyCollection:
                    Debug.LogWarning($"Collection: '{errorObjectName}' is empty. This collection should not be left empty", parentObject);
                    break;
                case ErrorSubject.UnknownError:
                    Debug.LogWarning($"Unknown Error: '{errorObjectName}'. This error should be investigated", parentObject);
                    break;
                default:
                    Debug.LogWarning($"The origin of this error is unknown. This error should be investigated", parentObject);
                    break;
            }
        }
        
        /// <summary>
        /// Method Containing standard responses to parameters defined in method signature. Logs a standardized message
        /// in the unity console hinting at the fact that said error can cause considerable issues
        /// </summary>
        /// <param name="errorObjectName">Name of object that caused an error</param>
        /// <param name="error">What kind of error occured?</param>
        /// <param name="parentObject">UnityEngine.Object where this error occured. Consider providing a reference to simplify debugging</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogErrorStandardStringMessage(string errorObjectName, ErrorSubject error = ErrorSubject.Reference, Object parentObject = null)
        {
            switch (error)
            {
                case ErrorSubject.Singleton:
                    Debug.LogError($"***Object: '{errorObjectName}' was destroyed because a different object" +
                                     $" of the same type already exists in memory!***", parentObject);
                    break;
                case ErrorSubject.Reference:
                    Debug.LogError($"***Reference: '{errorObjectName}' is null. This reference needs to be filled!***", parentObject);
                    break;
                case ErrorSubject.PrimitivesHighValue:
                    Debug.LogError($"***Value of: '{errorObjectName}' is too high. This value MUST be lowered!***", parentObject);
                    break;
                case ErrorSubject.PrimitivesLowValue:
                    Debug.LogError($"***Value of: '{errorObjectName}' is too low. This value MUST be increased!***", parentObject);
                    break;
                case ErrorSubject.PrimitivesOddValue:
                    Debug.LogError($"***Value of: '{errorObjectName}' was unexpected. This value MUST be changed to something else!***", parentObject);
                    break;
                case ErrorSubject.EmptyCollection:
                    Debug.LogError($"Collection: '{errorObjectName}' is empty. This collection MUST NOT be empty!***", parentObject);
                    break;
                case ErrorSubject.UnknownError:
                    Debug.LogError($"***Unknown Error: '{errorObjectName}'. The cause of this error MUST be investigated!***", parentObject);
                    break;
                default:
                    Debug.LogError($"***The origin of this error is unknown. The cause of this error Must be investigated!***", parentObject);
                    break;
            }        
        }

        /// <summary>
        /// Method Containing standard responses to parameters defined in method signature. Logs a standardized message
        /// in the unity console hinting at the fact that said error can cause very serious issues
        /// </summary>
        /// <param name="errorObjectName">Name of object that caused an error</param>
        /// <param name="error">What kind of error occured?</param>
        /// <param name="parentObject">UnityEngine.Object where this error occured. Consider providing a reference to simplify debugging</param>
        private static void LogPivotalStringMessage(string errorObjectName, ErrorSubject error,
            Object parentObject = null)
        {
            switch (error)
            {
                case ErrorSubject.Singleton:
                    Debug.LogError($"***Object: '{errorObjectName}' was destroyed because a different object" +
                                   $" of the same type already exists in memory!***", parentObject);
                    break;
                case ErrorSubject.Reference:
                    Debug.LogError($"***Object '{errorObjectName}' is null. This reference was expected" +
                                   $" to be part of the scene but wasn't found. Ensure that said " +
                                   $"reference / component is present in the current scene!'***", parentObject);
                    break;
                case ErrorSubject.PrimitivesHighValue:
                    Debug.LogError($"***Value of: '{errorObjectName}' is too high. This value MUST be lowered" +
                                   $" inorder to ensure that things function normally!***");
                    break;
                case ErrorSubject.PrimitivesLowValue:
                    Debug.LogError($"***Value of: '{errorObjectName}' it too low. This value MUST be increased" +
                                   $" inorder to ensure that things function normally!*** ***");
                    break;
                case ErrorSubject.PrimitivesOddValue:
                    Debug.LogError($"***Value of: '{errorObjectName}' was unexpected. This value MUST be changed" +
                                   $" in order to ensure that things function normally!***", parentObject);
                    break;
                case ErrorSubject.EmptyCollection:
                    Debug.LogError($"Collection: '{errorObjectName}' is empty. This collection was" +
                                   $" expected to be filled. Fill this collection in order" +
                                   $" to ensure that thing function normally!***", parentObject);
                    break;
                case ErrorSubject.UnknownError:
                    Debug.LogError($"***Unknown Error: '{errorObjectName}'. The cause of" +
                                   $" this error MUST be investigated!***", parentObject);
                    break;
                default:
                    Debug.LogError($"***The origin of this error is unknown. The cause of" +
                                   $" this error Must be investigated!***", parentObject);
                    break;
            }
        }

        /// <summary>
        /// Standard design of a switch case designed to forward responsibility to appropriate functions handling specified severity
        /// </summary>
        /// <param name="severity">How severe is the issue encountered?</param>
        /// <param name="errorObjectName">Name of the object / reference that caused the error</param>
        /// <param name="error">What was the type of error that was caused</param>
        /// <param name="parentObject">UnityEngine.Object where the error occured. Include to make debugging easier</param>
        private static void StandardSwitch(ErrorSeverity severity, string errorObjectName, ErrorSubject error, Object parentObject = null)
        {
            switch (severity)
            {
                case ErrorSeverity.None:
                    LogStandardStringMessageWeak(errorObjectName, error, parentObject);
                    break;
                case ErrorSeverity.Warning:
                    LogWarningStandardStringMessage(errorObjectName, error, parentObject);
                    break;
                case ErrorSeverity.Error:
                    LogErrorStandardStringMessage(errorObjectName, error, parentObject);
                    break;
                case ErrorSeverity.ScenePivotal:
                    LogPivotalStringMessage(errorObjectName, error, parentObject);
                    break;
                case ErrorSeverity.FatalError:
                    // ThrowStandardBuildError();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }
        }
        
        /// <summary>
        /// Forwards responsibility to other function to log error
        /// </summary>
        /// <param name="severity">How severe is the issue encountered?</param>
        /// <param name="errorObjectName">Name of the object / reference that caused the error</param>
        /// <param name="subject">What was the type of error that was caused</param>
        /// <param name="parentObject">UnityEngine.Object where the error occured. Include to make debugging easier</param>
        /// <param name="extraLoggingFunction">Extra function to be passed in if needed. Completely optional</param>
        private static void StandardLoggingOutPut(string errorObjectName, ErrorSeverity severity = ErrorSeverity.None, ErrorSubject subject = ErrorSubject.Reference,
            Object parentObject = null, Action extraLoggingFunction = null)
        {
            StandardSwitch(severity, errorObjectName, subject, parentObject);
            extraLoggingFunction?.Invoke();
        }

        #endregion
    }
}