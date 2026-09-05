using System;
using System.Diagnostics;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace CleanFoundation.Diagnostics
{
    /// <summary>
    /// UnityEngine.Debug の薄い Facade。
    /// Domain / Application から UnityEngine.Debug を直接参照せず、
    /// Unity と同じ Debug.Log(...) の書き味を維持する。
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// 通常ログを出力する。
        /// UNITY_EDITOR / DEVELOPMENT_BUILD 以外では呼び出し自体が除去される。
        /// </summary>
        [HideInCallstack]
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object message)
            => UnityEngine.Debug.Log(message);

        /// <summary>
        /// 通常ログを出力する。
        /// source はログ発生元の表示、および UnityEngine.Object の場合は
        /// Console の context として使用する。
        /// UNITY_EDITOR / DEVELOPMENT_BUILD 以外では呼び出し自体が除去される。
        /// </summary>
        [HideInCallstack]
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object message, object source)
        {
            UnityEngine.Debug.Log(
                FormatMessage(message, source),
                GetUnityContext(source));
        }

        /// <summary>
        /// 警告ログを出力する。
        /// UNITY_EDITOR / DEVELOPMENT_BUILD 以外では呼び出し自体が除去される。
        /// </summary>
        [HideInCallstack]
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object message)
            => UnityEngine.Debug.LogWarning(message);

        /// <summary>
        /// 警告ログを出力する。
        /// source はログ発生元の表示、および UnityEngine.Object の場合は
        /// Console の context として使用する。
        /// UNITY_EDITOR / DEVELOPMENT_BUILD 以外では呼び出し自体が除去される。
        /// </summary>
        [HideInCallstack]
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(object message, object source)
        {
            UnityEngine.Debug.LogWarning(
                FormatMessage(message, source),
                GetUnityContext(source));
        }

        /// <summary>
        /// エラーログを出力する。
        /// リリースビルドでも残る。
        /// </summary>
        [HideInCallstack]
        public static void LogError(object message)
            => UnityEngine.Debug.LogError(message);

        /// <summary>
        /// エラーログを出力する。
        /// source はログ発生元の表示、および UnityEngine.Object の場合は
        /// Console の context として使用する。
        /// リリースビルドでも残る。
        /// </summary>
        [HideInCallstack]
        public static void LogError(object message, object source)
        {
            UnityEngine.Debug.LogError(
                FormatMessage(message, source),
                GetUnityContext(source));
        }

        /// <summary>
        /// 例外を出力する。
        /// 例外は障害解析に必要なためリリースビルドでも残る。
        /// </summary>
        [HideInCallstack]
        public static void LogException(Exception exception)
            => UnityEngine.Debug.LogException(exception);

        /// <summary>
        /// 例外を出力する。
        /// source が UnityEngine.Object の場合は Console の context として使用する。
        /// </summary>
        [HideInCallstack]
        public static void LogException(Exception exception, object source)
        {
            UObject context = GetUnityContext(source);

            if (context != null)
            {
                UnityEngine.Debug.LogException(exception, context);
                return;
            }

            // Domain object 等は Unity の context にできないため、
            // 発生元を別ログとして付与してから例外を出す。
            UnityEngine.Debug.LogError($"{GetPrefix(source)} Exception thrown.");

            UnityEngine.Debug.LogException(exception);
        }

        /// <summary>
        /// 条件が false の場合に Assertion を出力する。
        /// UNITY_EDITOR / DEVELOPMENT_BUILD 以外では呼び出し自体が除去される。
        /// </summary>
        [HideInCallstack]
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(bool condition)
            => UnityEngine.Debug.Assert(condition);

        /// <summary>
        /// 条件が false の場合に Assertion を出力する。
        /// UNITY_EDITOR / DEVELOPMENT_BUILD 以外では呼び出し自体が除去される。
        /// </summary>
        [HideInCallstack]
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(bool condition, object message)
            => UnityEngine.Debug.Assert(condition, message);

        /// <summary>
        /// 条件が false の場合に Assertion を出力する。
        /// source の情報をメッセージに付与する。
        /// UNITY_EDITOR / DEVELOPMENT_BUILD 以外では呼び出し自体が除去される。
        /// </summary>
        [HideInCallstack]
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        public static void Assert(
            bool condition,
            object message,
            object source)
        {
            if (condition) return;

            UObject context = GetUnityContext(source);
            string text = FormatMessage(message, source);

            if (context != null)
            {
                UnityEngine.Debug.Assert(false, text, context);
                return;
            }

            UnityEngine.Debug.Assert(false, text);
        }

        private static string FormatMessage(object message, object source)
            => $"{GetPrefix(source)} {message}";

        private static UObject GetUnityContext(object source)
            => source as UObject;

        private static string GetPrefix(object source)
        {
            // CLR null。
            // UnityEngine.Object の「破棄済み == null」とは区別する。
            if (source is null)
            {
                return "[null]";
            }

            if (source is UObject unityObject)
            {
                // UnityEngine.Object の特殊 null 判定。
                if (unityObject == null)
                {
                    return "[destroyed]";
                }

                if (unityObject is Component component)
                {
                    return $"[{component.gameObject.name}/{component.GetType().Name}]";
                }

                if (unityObject is GameObject gameObject)
                {
                    return $"[{gameObject.name}/GameObject]";
                }

                return $"[{unityObject.name}/{unityObject.GetType().Name}]";
            }

            return $"[{source.GetType().Name}]";
        }
    }
}
