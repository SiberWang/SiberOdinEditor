using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Datas;
using Examples.Editor.Names;
using SiberOdinEditor.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Examples.Editor.Windows
{
    public static class MenuItemSetter
    {
    #region ========== [Private Variables] ==========

        private static EditorSaveFile SaveFile => EditorSaveSystem.SaveFile;

    #endregion

    #region ========== [Public Methods] ==========

        public static void SetCustomMenuItems(this OdinMenuTree tree)
        {
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
                    editorCharacterDatas.FirstOrDefault(s => s.characterData.DataID.Equals(exteriorData.DataID));
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

    #endregion
    }
}