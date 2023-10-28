using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace SiberOdinEditor.Tools
{
    public static class OdinCommonGUITools
    {
        /// <summary> 來自Odin 教呈 </summary>
        /// 在 title 製造一個 button , 然後選了以後，才會設定 selectedType
        /// https://github.com/onewheelstudio/SirenixTutorialFiles/blob/master/Data%20Manager/GUIUtils.cs
        public static bool SelectButtonList(ref Type selectedType, Type[] typesToDisplay)
        {
            var rect = GUILayoutUtility.GetRect(0, 25);

            for (int i = 0; i < typesToDisplay.Length; i++)
            {
                var name    = typesToDisplay[i].Name;
                var btnRect = rect.Split(i, typesToDisplay.Length);

                if (SelectButton(btnRect, name, typesToDisplay[i] == selectedType))
                {
                    selectedType = typesToDisplay[i];
                    return true;
                }
            }

            return false;
        }

        /// <summary> 來自Odin 教呈 </summary>
        public static bool SelectButton(Rect rect, string name, bool selected)
        {
            if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
                return true;

            if (Event.current.type == EventType.Repaint)
            {
                var style = new GUIStyle(EditorStyles.miniButtonMid);
                style.stretchHeight = true;
                style.fixedHeight   = rect.height;
                style.Draw(rect, GUIHelper.TempContent(name), false, false, selected, false);
            }

            return false;
        }

        /// <summary> 按下 ESC 關閉 </summary>
        /// <param name="window"> 指定window </param>
        public static void EndGUI(ref OdinEditorWindow window)
        {
            if (!EditorHotKeys.IsKeyESCDown) return;
            window.Close();
            window = null;
        }

        /// <summary> 右鍵可以 Ping 物件位置 </summary>
        public static void RightClickPingAsset(object selectedValue)
        {
            if (selectedValue == null) return;
            var asset = selectedValue as ScriptableObject;
            if (asset == null)
            {
                Debug.Log("SelectedValue is not a ScriptableObject!");
                return;
            }

            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
        }

        /// <summary> 自製開啟視窗 (置中 + 定義Size) </summary>
        /// <param name="window"> 指定視窗 </param>
        /// <param name="weight"> 寬度 </param>
        /// <param name="height"> 高度 </param>
        public static T OpenWindowWithSize<T>(this OdinEditorWindow window, float weight = 800f, float height = 600f)
        where T : OdinEditorWindow
        {
            if (window != null)
            {
                window.Close();
                return null;
            }

            window          =  EditorWindow.GetWindow<T>();
            window.position =  GUIHelper.GetEditorWindowRect().AlignCenter(weight, height);
            window.OnEndGUI += () => EndGUI(ref window);
            window.Show();
            return window as T;
        }

        /// <summary> 自製開啟視窗 (純ESC事件) </summary>
        /// <param name="window"> 指定視窗 </param>
        public static T OpenWindow<T>(this OdinEditorWindow window)
        where T : OdinEditorWindow
        {
            if (window != null)
            {
                window.Close();
                return null;
            }

            window          =  EditorWindow.GetWindow<T>();
            window.OnEndGUI += () => EndGUI(ref window);
            window.Show();
            return window as T;
        }
    }
}