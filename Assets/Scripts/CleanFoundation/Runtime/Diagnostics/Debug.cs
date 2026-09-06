using System;
using System.Diagnostics;

#if UNITY_5_3_OR_NEWER
using UObject = UnityEngine.Object;
#endif

namespace CleanFoundation.Diagnostics
{
    /// <summary>
    /// Unity では UnityEngine.Debug、非 Unity 環境では Console を使用する Debug facade。
    /// </summary>
    public static class Debug
    {
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object message)
            => LogCore(message);

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object message, object source)
            => LogCore(message, source);

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object message)
            => LogWarningCore(message);

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object message, object source)
            => LogWarningCore(message, source);

        public static void LogError(object message)
            => LogErrorCore(message);

        public static void LogError(object message, object source)
            => LogErrorCore(message, source);

        public static void LogException(Exception exception)
            => LogExceptionCore(exception);

        public static void LogException(Exception exception, object source)
            => LogExceptionCore(exception, source);

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(bool condition)
            => AssertCore(condition);

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(bool condition, object message)
            => AssertCore(condition, message);

        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(bool condition, object message, object source)
            => AssertCore(condition, message, source);

        private static string FormatMessage(object message, object source)
            => $"{GetPrefix(source)} {message}";

        private static string GetPrefix(object source)
        {
            if (source is null)
            {
                return "[null]";
            }

#if UNITY_5_3_OR_NEWER
            if (source is UObject unityObject)
            {
                // UnityEngine.Object は CLR null とは別に、破棄済みオブジェクトを
                // operator == で null とみなすため、先に判定する。
                if (unityObject == null)
                {
                    return "[destroyed]";
                }

                if (unityObject is UnityEngine.Component component)
                {
                    return $"[{component.gameObject.name}/{component.GetType().Name}]";
                }

                if (unityObject is UnityEngine.GameObject gameObject)
                {
                    return $"[{gameObject.name}/GameObject]";
                }

                return $"[{unityObject.name}/{unityObject.GetType().Name}]";
            }
#endif

            return $"[{source.GetType().Name}]";
        }

#if UNITY_5_3_OR_NEWER
        private static UObject GetUnityContext(object source)
            => source as UObject;

        private static void LogCore(object message)
            => UnityEngine.Debug.Log(message);

        private static void LogCore(object message, object source)
            => UnityEngine.Debug.Log(FormatMessage(message, source), GetUnityContext(source));

        private static void LogWarningCore(object message)
            => UnityEngine.Debug.LogWarning(message);

        private static void LogWarningCore(object message, object source)
            => UnityEngine.Debug.LogWarning(FormatMessage(message, source), GetUnityContext(source));

        private static void LogErrorCore(object message)
            => UnityEngine.Debug.LogError(message);

        private static void LogErrorCore(object message, object source)
            => UnityEngine.Debug.LogError(FormatMessage(message, source), GetUnityContext(source));

        private static void LogExceptionCore(Exception exception)
            => UnityEngine.Debug.LogException(exception);

        private static void LogExceptionCore(Exception exception, object source)
        {
            UObject context = GetUnityContext(source);

            if (context != null)
            {
                UnityEngine.Debug.LogException(exception, context);
                return;
            }

            UnityEngine.Debug.LogError($"{GetPrefix(source)} Exception thrown.");
            UnityEngine.Debug.LogException(exception);
        }

        private static void AssertCore(bool condition)
            => UnityEngine.Debug.Assert(condition);

        private static void AssertCore(bool condition, object message)
            => UnityEngine.Debug.Assert(condition, message);

        private static void AssertCore(bool condition, object message, object source)
        {
            if (condition)
            {
                return;
            }

            UObject context = GetUnityContext(source);
            string text = FormatMessage(message, source);

            if (context != null)
            {
                UnityEngine.Debug.Assert(false, text, context);
                return;
            }

            UnityEngine.Debug.Assert(false, text);
        }
#else
        private static void LogCore(object message)
            => Console.WriteLine(message);

        private static void LogCore(object message, object source)
            => Console.WriteLine(FormatMessage(message, source));

        private static void LogWarningCore(object message)
            => Console.WriteLine($"Warning: {message}");

        private static void LogWarningCore(object message, object source)
            => Console.WriteLine($"Warning: {FormatMessage(message, source)}");

        private static void LogErrorCore(object message)
            => Console.Error.WriteLine(message);

        private static void LogErrorCore(object message, object source)
            => Console.Error.WriteLine(FormatMessage(message, source));

        private static void LogExceptionCore(Exception exception)
            => Console.Error.WriteLine(exception);

        private static void LogExceptionCore(Exception exception, object source)
        {
            Console.Error.WriteLine($"{GetPrefix(source)} Exception thrown.");
            Console.Error.WriteLine(exception);
        }

        private static void AssertCore(bool condition)
        {
            if (!condition)
            {
                Console.Error.WriteLine("Assertion failed.");
            }
        }

        private static void AssertCore(bool condition, object message)
        {
            if (!condition)
            {
                Console.Error.WriteLine($"Assertion failed: {message}");
            }
        }

        private static void AssertCore(bool condition, object message, object source)
        {
            if (!condition)
            {
                Console.Error.WriteLine($"Assertion failed: {FormatMessage(message, source)}");
            }
        }
#endif
    }
}
