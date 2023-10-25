using System.Linq;
using Examples.Scripts.OdinWindows.Tools;
using SiberOdinEditor.Tools;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Examples.Scripts.OdinWindows
{
    /// <summary> 自製資料編輯器 </summary>
    public class DataManagerWindow : OdinMenuEditorWindow
    {
    #region ========== [Private Variables] ==========

        private const string Item_RunTime = "01 RunTime 資料編輯";

        private static DataManagerWindow window;

    #endregion

    #region ========== [Events] ==========

        protected override void OnBeginDrawEditors()
        {
            if (window == null)
                window = GetWindow<DataManagerWindow>();
            var selectedItem  = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                OdinDrawTools.Draw_Label_WindowSize(window.position);
                OdinDrawTools.Draw_Label_CurrentSelectName(selectedItem);
                OdinDrawTools.Draw_Button_CreateNewData(selectedItem);
                OdinDrawTools.Draw_Button_DeleteCurrentSOData(selectedItem, IsShowDelete(selectedItem.Value),
                                                            DoDeleteAction);
                OdinDrawTools.Draw_Button_AllDataSave(MenuTree.MenuItems);
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnGUI()
        {
            base.OnGUI();

            // Ctrl+S 快捷鍵 (儲存)
            EditorHotKeys.CtrlS(() => OdinDrawTools.DoAllDataSave(MenuTree.MenuItems));
            if (!GUIHelper.CurrentWindowHasFocus)
                EditorHotKeys.Init();
        }

    #endregion

    #region ========== [Protected Methods] ==========

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            AddSetting(tree);
            AddRunTimeEditorData(tree);
            // tree.AddAssetsAndTitle<ShootStrategySOData, ShootStrategy.Data>("02 射擊策略資料", FilePaths.ShootStrategyDatas);
            tree.SortMenuItemsByName(false);
            return tree;
        }

    #endregion

    #region ========== [Private Methods] ==========

        private void AddRunTimeEditorData(OdinMenuTree tree)
        {
            // tree.AddAssetAtPath(Item_RunTime, FilePaths.ShootStrategyRepository, typeof(ShootStrategyRepository));
            // tree.GetMenuItem(Item_RunTime).SetTitleStyle().AddIcon(SdfIconType.PencilSquare);
        }

        /// <summary> 視窗設定 </summary>
        private void AddSetting(OdinMenuTree tree)
        {
            var settingName = "00 編輯器設定";
            tree.AddAssetAtPath(settingName, FilePaths.DataManagerSetting, typeof(DataManagerSetting));
            var odinMenuItem = tree.GetMenuItem(settingName);
            odinMenuItem.SetTitleStyle().AddIcon(SdfIconType.Headset);

            if (odinMenuItem.Value is DataManagerSetting mainData)
            {
                var setting    = mainData.Setting;
                var windowSize = setting.WindowSize;
                SetWindowSize(windowSize);
                MenuWidth                      = setting.MenuWidth;
                tree.DefaultMenuStyle.IconSize = setting.IconSize;
            }
            else
            {
                MenuWidth                      = 220;
                tree.DefaultMenuStyle.IconSize = 25.00f;
            }

            tree.Config.UseCachedExpandedStates = true;
            tree.Config.DrawSearchToolbar       = true;
            WindowPadding                       = Vector4.zero;
        }

        /// <summary> 刪除資料 Action </summary>
        private void DoDeleteAction()
        {
            // var odinMenuItem = MenuTree.GetMenuItem(Item_RunTime);
            // Assert.IsNotNull(odinMenuItem, "odinMenuItem(Item_RunTime) == null");
            // var shootStrategyRepository = odinMenuItem.Value as ShootStrategyRepository;
            // shootStrategyRepository.RefreshList();
        }

        /// <summary> 判斷 - 當前頁面是否開啟 Delete Button </summary>
        private bool IsShowDelete(object selected)
        {
            if (selected is DataManagerSetting) return false;
            // if (selected is ShootStrategyRepository) return false;
            return true;
        }

        // [MenuItem(MenuHotKeys.DataManagerWindow)]
        private static void OpenEditor() => window = window.OpenWindow<DataManagerWindow>();


        private static void SetWindowSize(Vector2 windowSize)
        {
            if (window == null) return;
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(windowSize.x, windowSize.y);
        }

    #endregion
    }
}