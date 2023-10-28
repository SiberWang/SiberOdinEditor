using System.Collections.Generic;
using Examples.Editor.Datas;
using Examples.Scripts.Datas;
using SiberUtility.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Examples.Editor.Windows
{
    /// <summary> Editor 資料庫 </summary>
    public static class EditorRepository
    {
    #region ========== [Public Variables] ==========

        //Setting
        public static WindowSettingData WindowSettingData;

        // Containers
        public static CharacterDataContainer CharacterDataContainer;
        public static ExteriorDataContainer  ExteriorDataContainer;
        public static WeaponDataContainer    WeaponDataContainer;

        // EditorDataList
        public static List<EditorReferenceData_Character> EditorCharacterDatas;
        public static List<EditorReferenceData_Weapon>    EditorWeaponDatas;

    #endregion

    #region ========== [Constructor] ==========

        public static void Init()
        {
            CharacterDataContainer = EditorHelper.GetScriptableObject<CharacterDataContainer>();
            ExteriorDataContainer  = EditorHelper.GetScriptableObject<ExteriorDataContainer>();
            WeaponDataContainer    = EditorHelper.GetScriptableObject<WeaponDataContainer>();
            WindowSettingData      = EditorHelper.GetScriptableObject<WindowSettingData>();
            Assert.IsNotNull(CharacterDataContainer, $"{nameof(CharacterDataContainer)} == null");
            Assert.IsNotNull(ExteriorDataContainer, $"{nameof(ExteriorDataContainer)} == null");
            Assert.IsNotNull(WeaponDataContainer, $"{nameof(WeaponDataContainer)} == null");
            if (WindowSettingData == null) Debug.Log($"EditorSettingData == null , Use default setting");

            EditorCharacterDatas = new List<EditorReferenceData_Character>();
            EditorWeaponDatas    = new List<EditorReferenceData_Weapon>();
        }

    #endregion

    #region ========== [Public Methods] ==========

        public static void SetAllDataDirty()
        {
            EditorUtility.SetDirty(CharacterDataContainer);
            EditorUtility.SetDirty(ExteriorDataContainer);
            EditorUtility.SetDirty(WeaponDataContainer);
            EditorUtility.SetDirty(WindowSettingData);
        }

        public static void AddCharacterData(EditorReferenceData_Character data)
        {
            // DataContainer
            CharacterDataContainer.Add(data.characterData);
            ExteriorDataContainer.Add(data.exteriorData);
            // Editor
            EditorCharacterDatas.Add(data);
        }

        public static void DeleteCharacterData(EditorReferenceData_Character data)
        {
            // DataContainer
            CharacterDataContainer.Remove(data.characterData);
            ExteriorDataContainer.Remove(data.exteriorData);
            // Editor
            EditorCharacterDatas.Remove(data);
        }

        public static void AddWeaponData(EditorReferenceData_Weapon data)
        {
            // Editor
            EditorWeaponDatas.Add(data);
            // DataContainer
            WeaponDataContainer.Add(data.weaponData);
        }

        public static void DeleteWeaponData(EditorReferenceData_Weapon data)
        {
            // Editor
            EditorWeaponDatas.Remove(data);
            // DataContainer
            WeaponDataContainer.Remove(data.weaponData);
        }

    #endregion
    }
}