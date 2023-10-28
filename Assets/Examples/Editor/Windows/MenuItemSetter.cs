using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Datas;
using Examples.Editor.Names;
using SiberOdinEditor.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Examples.Editor.Windows
{
    public static class MenuItemSetter
    {
    #region ========== [Private Variables] ==========

        private static DataManager    Window   => DataManager.Window;
        private static EditorSaveFile SaveFile => EditorSaveSystem.SaveFile;

    #endregion

    #region ========== [Public Methods] ==========

        public static void SetCustomMenuItems(this OdinMenuTree tree)
        {
            // Setting
            tree.AddTitle_EditorSetting();
            // Character
            tree.AddTitle_CreateCharacterData();
            tree.AddChild_CharacterDatas();
            // Weapon
            tree.AddTitle_CreateWeaponData();
            tree.AddChild_WeaponDatas();
        }

    #endregion

    #region ========== [Private Methods] ==========

        private static void AddChild_CharacterDatas(this OdinMenuTree tree)
        {
            var characterDatas       = EditorRepository.CharacterDataContainer.Datas;
            var exteriorDatas        = EditorRepository.ExteriorDataContainer.Datas;
            var editorCharacterDatas = EditorRepository.EditorCharacterDatas;

            // Main
            foreach (var characterData in characterDatas)
            {
                // 雙向 References
                var dataName   = SaveFile.GetEditorDisplayName(characterData.DataID);
                var editorData = new EditorReferenceData_Character(characterData);
                editorData.SetDataName(dataName ?? $"{characterData.Name}");
                var resultName = $"{MenuItemNames.TitleName_Character}/{editorData.DataName}";
                tree.Add(resultName, editorData, SdfIconType.JournalPlus);
                editorCharacterDatas.Add(editorData);
            }

            // Child
            foreach (var exteriorData in exteriorDatas)
            {
                var editorData =
                    editorCharacterDatas.FirstOrDefault(s => s.MainReferenceDataID.Equals(exteriorData.DataID));
                if (editorData == null) continue;
                editorData.exteriorData = exteriorData;
            }
        }

        private static void AddChild_WeaponDatas(this OdinMenuTree tree)
        {
            var weaponDatas     = EditorRepository.WeaponDataContainer.Datas;
            var editorItemDatas = EditorRepository.EditorWeaponDatas;

            // Main
            foreach (var weaponData in weaponDatas)
            {
                // 雙向 References
                var dataName   = SaveFile.GetEditorDisplayName(weaponData.DataID);
                var editorData = new EditorReferenceData_Weapon(weaponData);
                editorData.SetDataName(dataName ?? $"{weaponData.Name}");
                var resultName = $"{MenuItemNames.TitleName_Weapon}/{editorData.DataName}";
                tree.Add(resultName, editorData, SdfIconType.ConeStriped);
                editorItemDatas.Add(editorData);
            }
        }

        private static void AddTitle_CreateCharacterData(this OdinMenuTree tree)
        {
            var titleName = MenuItemNames.TitleName_Character;
            var data      = new EditorCreateData_Character(tree, titleName);
            tree.Add(titleName, data, SdfIconType.Ticket);
        }

        private static void AddTitle_CreateWeaponData(this OdinMenuTree tree)
        {
            var titleName = MenuItemNames.TitleName_Weapon;
            var data      = new EditorCreateData_Weapon(tree, titleName);
            tree.Add(titleName, data, SdfIconType.Joystick);
        }

        private static void AddTitle_EditorSetting(this OdinMenuTree tree)
        {
            var editorSettingData = EditorRepository.WindowSettingData;
            if (editorSettingData != null)
            {
                var setting             = editorSettingData.Setting;
                var customOdinMenuStyle = editorSettingData.CustomOdinMenuStyle;
                tree.DefaultMenuStyle = customOdinMenuStyle.GetNewOdinMenuStyle();
                SetWindowSize(setting.WindowSize);
                SetMenuWidth(setting.MenuWidth);
                tree.Add("視窗設定", new EditorReferenceData_WindowSetting(editorSettingData , tree.DefaultMenuStyle), SdfIconType.GearFill);
            }
            else
            {
                SetWindowSize(new Vector2(800, 600));
                SetMenuWidth(220);
                tree.DefaultMenuStyle.IconSize = 25.00f;
            }

            // tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle; // 這個會讓整個按鈕小小的
            tree.Config.UseCachedExpandedStates = true;
            tree.Config.DrawSearchToolbar       = true;
            Window.WindowPadding                = Vector4.zero;
        }

        public static void SetMenuWidth(float width)
        {
            Window.MenuWidth = width;
        }

        public static void SetWindowSize(Vector2 windowSize)
        {
            Window.position = GUIHelper.GetEditorWindowRect().AlignCenter(windowSize.x, windowSize.y);
        }

        public static Vector2 GetWindowSize()
        {
            return new Vector2(Window.position.width, Window.position.height);
        }

    #endregion
    }
}