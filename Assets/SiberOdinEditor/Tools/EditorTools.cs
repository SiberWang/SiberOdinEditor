using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace SiberOdinEditor.Tools
{
    public static class EditorTools
    {
        private const int IconSize = 50;

        /// <summary> 秀出 Save 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        public static void ShowSaveNotification(this EditorWindow window)
        {
            if (window == null) return;
            var context    = "All files Save";
            var texture2D  = OdinStyleTools.GetSdfIcon(SdfIconType.Save, Color.green, IconSize);
            var guiContent = new GUIContent(context, texture2D);
            window.ShowNotification(guiContent, 1f);
        }

        /// <summary> 秀出 Error 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        public static void ShowErrorNotification(this EditorWindow window)
        {
            if (window == null) return;
            var context    = "Error Something wrong!";
            var texture2D  = OdinStyleTools.GetSdfIcon(SdfIconType.Bug, Color.red, IconSize);
            var guiContent = new GUIContent(context, texture2D);
            window.ShowNotification(guiContent, 1f);
        }

        /// <summary> 秀出 Warning 資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        public static void ShowWarningNotification(this EditorWindow window)
        {
            if (window == null) return;
            var context    = "Warning Something Happened!";
            var texture2D  = OdinStyleTools.GetSdfIcon(SdfIconType.Bug, Color.yellow, IconSize);
            var guiContent = new GUIContent(context, texture2D);
            window.ShowNotification(guiContent, 1f);
        }

        /// <summary> 秀出客製化資訊欄 </summary>
        /// <param name="window"> 有繼承 OdinEditorWindow 的 Editor編輯視窗 </param>
        /// <param name="context"> 內容 </param>
        /// <param name="color"> Icon顏色 </param>
        /// <param name="sdfIconType"> Odin SdfIconType </param>
        public static void ShowCustomNotification(this EditorWindow window, string context, Color color, SdfIconType sdfIconType)
        {
            if (window == null) return;
            var texture2D  = OdinStyleTools.GetSdfIcon(sdfIconType, color, IconSize);
            var guiContent = new GUIContent(context, texture2D);
            window.ShowNotification(guiContent, 1f);
        }
    }
}