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
                DoAllDataSaveByAction(onSaveAction);
        }

        /// <summary> 建立檔案 Button </summary>
        public static void Draw_Button_CreateNewData(OdinMenuItem selected)
        {
            if (selected is not { Value: ICreateDataAction createDataAction }) return;
            CustomGUIHelper.ColorBackGround(new Color(0.6f, 1.2f, 0.6f), () =>
            {
                if (!OdinStyleTools.CustomToolbarButton("Create", SdfIconType.FileEarmarkPlus)) return;
                createDataAction.Create();
            });
        }

        /// <summary> 刪除檔案 Button (Delete SOData) </summary>
        /// <param name="selected"> 當前選取的 OdinMenuItem 項目 </param>
        /// <param name="condition"> 顯示此 Button 的判斷 </param>
        /// <param name="onDeleteAction"> 可自定義額外事件 </param>
        public static void Draw_Button_ImmediatelyDeleteSOData
            (OdinMenuItem selected, bool condition = true, Action onDeleteAction = null)
        {
            if (selected is not { Value: ScriptableObject }) return;
            if (!condition) return;

            CustomGUIHelper.ColorBackGround(new Color(1f, 0.3f, 0.3f) * 1.2f, () =>
            {
                if (!OdinStyleTools.CustomToolbarButton("Delete", SdfIconType.XSquareFill)) return;
                var asset = selected.Value as ScriptableObject;
                var path  = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                DoDeleteData(onDeleteAction);
            });
        }

        /// <summary> 刪除檔案 Button (Do DeleteAction) </summary>
        /// <param name="condition"> 顯示此 Button 的判斷 </param>
        /// <param name="onDeleteAction"> 自定義事件 - 立即刪除 </param>
        public static void Draw_Button_ImmediatelyDeleteData(bool condition = true, Action onDeleteAction = null)
        {
            if (!condition) return;
            CustomGUIHelper.ColorBackGround(new Color(1f, 0.3f, 0.3f) * 1.2f, () =>
            {
                if (!OdinStyleTools.CustomToolbarButton("Delete", SdfIconType.XSquareFill)) return;
                DoDeleteData(onDeleteAction);
            });
        }

        /// <summary> 刪除當前檔案 Button (Safe Do DeleteAction) </summary>
        /// <param name="condition"> 顯示此 Button 的判斷 </param>
        /// <param name="onDeleteAction"> 自定義事件 - 立即刪除 </param>
        public static void Draw_Button_SafeDeleteData(bool condition = true, Action onDeleteAction = null)
        {
            if (!condition) return;
            CustomGUIHelper.ColorBackGround(new Color(1f, 0.3f, 0.3f) * 1.2f, () =>
            {
                if (!OdinStyleTools.CustomToolbarButton("Delete", SdfIconType.XSquareFill)) return;
                onDeleteAction?.Invoke();
            });
        }
        
        /// <summary> 複製當前檔案 Button (Clone) </summary>
        /// <param name="condition"> 顯示此 Button 的判斷 </param>
        /// <param name="onCloneAction"> 自定義事件 - 複製 </param>
        public static void Draw_Button_CloneData(bool condition = true, Action onCloneAction = null)
        {
            if (!condition) return;
            CustomGUIHelper.ColorBackGround(new Color(0.64f, 1f, 0.73f) * 1.2f, () =>
            {
                if (!OdinStyleTools.CustomToolbarButton("Clone", SdfIconType.Subtract)) return;
                onCloneAction?.Invoke();
            });
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

        /// <summary> 視窗 MenuWidth Size </summary>
        public static void Draw_Label_WidthSize(float width)
        {
            var text  = $"Menu Width: ({width})";
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
        public static void DoAllDataSaveByAction(Action onSaveAction)
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
            GUIHelper.CurrentWindow.ShowDeleteNotification();
        }

        /// <summary> 設定預設選擇項目 </summary>
        /// <param name="tree"> 當前的 tree </param>
        /// <param name="titleName"> 項目名稱 </param>
        public static void DefaultFirstSelect(this OdinMenuTree tree, string titleName)
        {
            var defaultItem = tree.GetMenuItem(titleName);
            if (defaultItem == null) return;
            tree.Selection.Clear();
            tree.Selection.Add(defaultItem);
        }

    #endregion
    }
}