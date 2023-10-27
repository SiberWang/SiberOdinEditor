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
    [InitializeOnLoad]
    public static class EditorRepository
    {
    #region ========== [Public Variables] ==========

        // Containers
        public static CharacterDataContainer CharacterDataContainer;
        public static ExteriorDataContainer  ExteriorDataContainer;
        public static WeaponDataContainer    WeaponDataContainer;

        // EditorDataList
        public static List<EditorReferenceData_Character> EditorCharacterDatas;
        public static List<EditorReferenceData_Weapon>    EditorWeaponDatas;

    #endregion

    #region ========== [Constructor] ==========

        static EditorRepository()
        {
            CharacterDataContainer = EditorHelper.GetScriptableObject<CharacterDataContainer>();
            ExteriorDataContainer  = EditorHelper.GetScriptableObject<ExteriorDataContainer>();
            WeaponDataContainer    = EditorHelper.GetScriptableObject<WeaponDataContainer>();
            Assert.IsNotNull(CharacterDataContainer, $"{nameof(CharacterDataContainer)} == null");
            Assert.IsNotNull(ExteriorDataContainer, $"{nameof(ExteriorDataContainer)} == null");
            Assert.IsNotNull(WeaponDataContainer, $"{nameof(WeaponDataContainer)} == null");

            EditorCharacterDatas = new List<EditorReferenceData_Character>();
            EditorWeaponDatas    = new List<EditorReferenceData_Weapon>();
            Debug.Log($"Editor Repository 準備就緒");
        }

    #endregion

    #region ========== [Public Methods] ==========

        public static void SetAllDataDirty()
        {
            EditorUtility.SetDirty(CharacterDataContainer);
            EditorUtility.SetDirty(ExteriorDataContainer);
            EditorUtility.SetDirty(WeaponDataContainer);
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