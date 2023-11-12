using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace SiberOdinEditor.Tools
{
    public static class EditorWindowTools
    {
        private const int   IconSize    = 50;
        private const float FadeoutWait = 1f;

        /// <summary> 秀出客製化資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        /// <param name="context"> 內容 </param>
        /// <param name="color"> Icon顏色 </param>
        /// <param name="sdfIconType"> Odin SdfIconType </param>
        public static void ShowCustomNotification
            (this EditorWindow window, string context, Color color, SdfIconType sdfIconType)
        {
            if (window == null) return;
            var texture2D = OdinStyleTools.GetSdfIcon(sdfIconType, color, IconSize);
            SetNotification(window, context, texture2D);
        }

        /// <summary> 秀出 Save 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        public static void ShowSaveNotification(this EditorWindow window, string context = null)
        {
            if (window == null) return;
            var resultContext = string.IsNullOrEmpty(context) ? "檔案儲存!!" : context;
            window.ShowCustomNotification(resultContext, Color.green, SdfIconType.Save);
        }

        /// <summary> 秀出 Error 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        public static void ShowErrorNotification(this EditorWindow window, string context)
        {
            var resultContext = string.IsNullOrEmpty(context) ? "發生錯誤!!" : context;
            window.ShowCustomNotification(resultContext, Color.red, SdfIconType.BugFill);
        }

        /// <summary> 秀出 Warning 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        /// <param name="context"> 內容 </param>
        public static void ShowWarningNotification(this EditorWindow window, string context)
        {
            var resultContext = string.IsNullOrEmpty(context) ? "發生警告!!" : context;
            window.ShowCustomNotification(resultContext, Color.yellow, SdfIconType.Bug);
        }

        /// <summary> 秀出 "成功" 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        /// <param name="context"> 內容 </param>
        public static void ShowSucceedNotification(this EditorWindow window, string context = null)
        {
            var resultContext = string.IsNullOrEmpty(context) ? "成功!!" : context;
            window.ShowCustomNotification(resultContext, Color.green, SdfIconType.Check);
        }

        /// <summary> 秀出 "失敗" 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        /// <param name="context"> 內容 </param>
        public static void ShowFailedNotification(this EditorWindow window, string context = null)
        {
            var resultContext = string.IsNullOrEmpty(context) ? "失敗!!" : context;
            window.ShowCustomNotification(resultContext, Color.red, SdfIconType.EmojiDizzy);
        }

        /// <summary> 秀出 "刪除" 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        public static void ShowDeleteNotification(this EditorWindow window, string context = null)
        {
            var resultContext = string.IsNullOrEmpty(context) ? "刪除成功!!" : context;
            window.ShowCustomNotification(resultContext, Color.red, SdfIconType.Trash2);
        }

        public static bool IsFocusedWindow<T>() where T : EditorWindow
        {
            var focusedWindow = EditorWindow.focusedWindow;
            var isWindowExist = focusedWindow != null && focusedWindow.GetType() == typeof(T);
            return isWindowExist;
        }

        private static void SetNotification(EditorWindow window, string context, Texture2D texture2D)
        {
            var guiContent = new GUIContent(context, texture2D);
            window.ShowNotification(guiContent, FadeoutWait);
        }
    }
}