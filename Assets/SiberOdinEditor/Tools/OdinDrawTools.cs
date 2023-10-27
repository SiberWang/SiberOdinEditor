using System;
using System.Collections.Generic;
using SiberOdinEditor.Core.Utils;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
#endif

namespace SiberOdinEditor.Tools
{
    /// <summary> Draw 工具包 </summary>
    public static class OdinDrawTools
    {
    #region ========== [Public Methods] ==========

        /// <summary> 全檔案儲存 Button <br/>
        /// 依照 OdinMenuItem 中內容來儲存 (常用於內容為 ScriptableObject)
        /// </summary>
        /// <param name="menuTreeMenuItems"> 放入 MenuTree.MenuItems </param>
        /// <param name="onSaveAction"> 額外事件 (可有可無) </param>
        public static void Draw_Button_AllDataSave
            (List<OdinMenuItem> menuTreeMenuItems, Action<Object> onSaveAction = null)
        {
            var iconColor = new Color(1f, 1f, 0.5f);
            if (OdinStyleTools.CustomToolbarButton("All Save (Ctrl+S)", SdfIconType.CalendarCheckFill, iconColor))
                DoAllDataSave(menuTreeMenuItems, onSaveAction);
        }

        public static void Draw_Button_AllDataSave(Action onSaveAction)
        {
            var iconColor = new Color(1f, 1f, 0.5f);
            if (OdinStyleTools.CustomToolbarButton("All Save (Ctrl+S)", SdfIconType.CalendarCheckFill, iconColor))
                DoAllDataSave(onSaveAction);
        }

        /// <summary> 建立檔案 Button </summary>
        public static void Draw_Button_CreateNewData(OdinMenuItem selected)
        {
            if (selected is not { Value: ICreateDataAction createDataAction }) return;
            GUI.backgroundColor = new Color(0.6f, 1.2f, 0.6f, 1f);
            if (OdinStyleTools.CustomToolbarButton("Create", SdfIconType.FileEarmarkPlus))
                createDataAction.Create();
            GUI.backgroundColor = Color.white;
        }

        /// <summary> 刪除當前檔案 Button (Delete SOData) </summary>
        public static void Draw_Button_DeleteCurrentSOData
            (OdinMenuItem selected, bool condition = true, Action onDeleteAction = null)
        {
            if (selected is not { Value: ScriptableObject }) return;
            if (!condition) return;
            GUI.backgroundColor = new Color(1f, 0.3f, 0.3f, 0.6f);
            if (OdinStyleTools.CustomToolbarButton("Delete", SdfIconType.DashCircle))
            {
                var asset = selected.Value as ScriptableObject;
                var path  = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                DoDeleteData(onDeleteAction);
            }
            GUI.backgroundColor = Color.white;
        }

        /// <summary> 刪除當前檔案 Button (Delete Custom object) </summary>
        public static void Draw_Button_DeleteCurrentObject(bool condition = true, Action onDeleteAction = null)
        {
            if (!condition) return;
            GUI.backgroundColor = new Color(1f, 0.3f, 0.3f, 0.6f);
            if (OdinStyleTools.CustomToolbarButton("Delete", SdfIconType.DashCircle)) 
                DoDeleteData(onDeleteAction);
            GUI.backgroundColor = Color.white;
        }

        /// <summary> 當前選擇檔案名稱 Label </summary>
        public static void Draw_Label_CurrentSelectName(OdinMenuItem selected)
        {
            var style        = OdinStyleTools.LightNameLabel();
            var selectedName = selected != null ? selected.Name : "你啥都沒選！";
            var text         = $"[ {selectedName} ]";
            GUILayout.Label(text, style);
        }

        /// <summary> 視窗Size顯示 Label (W,H) </summary>
        public static void Draw_Label_WindowSize(Rect winPos)
        {
            var text  = $"Size:(W: {winPos.width}, H: {winPos.height})";
            var style = OdinStyleTools.GrayLittleLabel();
            GUILayout.Label(text, style);
        }

        /// <summary> 全檔案儲存 <br/>
        /// 儲存清單內所有為 ScriptableObject 的項目 <br/>
        /// </summary>
        // MenuTree.MenuItems 等於一個 Group
        // menuItem.ChildMenuItems 則是 Group底下的子物件
        public static void DoAllDataSave(List<OdinMenuItem> menuTreeMenuItems, Action<Object> onSaveAction = null)
        {
            foreach (var menuItem in menuTreeMenuItems)
            {
                foreach (var childMenuItem in menuItem.ChildMenuItems)
                {
                    var asset = childMenuItem.Value as ScriptableObject;
                    if (asset == null) continue;
                    onSaveAction?.Invoke(asset);
                    EditorUtility.SetDirty(asset);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary> 全檔案儲存 <br/>
        /// 自定義儲存事件
        /// </summary>
        public static void DoAllDataSave(Action onSaveAction)
        {
            onSaveAction?.Invoke();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            GUIHelper.CurrentWindow.ShowSaveNotification();
        }
        
        /// <summary> 檔案刪除 <br/>
        /// 自定義儲存事件
        /// </summary>
        public static void DoDeleteData(Action onDeleteAction)
        {
            onDeleteAction?.Invoke();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            GUIHelper.CurrentWindow.ShowCustomNotification("Succeed Deleted!", Color.red, SdfIconType.Trash2);
        }

    #endregion
    }
}