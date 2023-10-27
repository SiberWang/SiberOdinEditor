using System.Collections.Generic;
using System.Linq;
using Examples.Editor.Windows;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Examples.Editor.Datas
{
    /// <summary> OdinMenuItem - 創建 Data 頁面 </summary>
    public class EditorCreateData_Character : BaseEditorCreateData
    {
    #region ========== [Protected Variables] ==========

        protected override List<BaseEditorReferenceData> EditorDatas =>
            EditorRepository.EditorCharacterDatas.ToList<BaseEditorReferenceData>();

    #endregion

    #region ========== [Constructor] ==========

        public EditorCreateData_Character(OdinMenuTree tree, string titleName) : base(tree, titleName) { }

    #endregion

    #region ========== [Protected Methods] ==========

        protected override void Create()
        {
            // 建立 EditorData，並加到 tree 菜單中
            var editorData = new EditorReferenceData_Character(newDataName);
            var resultName = $"{titleName}/{editorData.DataName}";
            tree.Add(resultName, editorData, SdfIconType.Check);
            EditorRepository.AddCharacterData(editorData);
            SaveNewJson(editorData);
        }

    #endregion
    }
}