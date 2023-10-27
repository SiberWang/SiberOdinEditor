using System.Collections.Generic;
using System.Linq;
using Examples.Editor.Windows;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Examples.Editor.Datas
{
    public class EditorCreateData_Weapon : BaseEditorCreateData
    {
    #region ========== [Protected Variables] ==========

        protected override List<BaseEditorReferenceData> EditorDatas =>
            EditorRepository.EditorWeaponDatas.ToList<BaseEditorReferenceData>();

    #endregion

    #region ========== [Constructor] ==========

        public EditorCreateData_Weapon(OdinMenuTree tree, string titleName) : base(tree, titleName) { }

    #endregion

    #region ========== [Protected Methods] ==========

        protected override void Create()
        {
            var editorData = new EditorReferenceData_Weapon(newDataName);
            var resultName = $"{titleName}/{editorData.DataName}";
            tree.Add(resultName, editorData, SdfIconType.Check);
            EditorRepository.AddWeaponData(editorData);
            SaveNewJson(editorData);
        }

    #endregion
    }
}