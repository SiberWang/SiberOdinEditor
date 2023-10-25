using System;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Names;
using Examples.Editor.Windows;
using Examples.Scripts;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Examples.Editor.Datas
{
    /// <summary> 創建頁面 </summary>
    [Serializable]
    public class Example_CreateData
    {
    #region ========== [Private Variables] ==========

        [ValidateInput(nameof(CanCreate), "@message", InfoMessageType.Warning)]
        [LabelText("新建名稱: ")]
        [LabelWidth(60)]
        [SerializeField]
        private string newDataName;

        private OdinMenuTree tree;
        private string       titleName;
        private string       message;

    #endregion

    #region ========== [Constructor] ==========

        public Example_CreateData(OdinMenuTree tree, string titleName)
        {
            this.tree      = tree;
            this.titleName = titleName;
        }

    #endregion

    #region ========== [Private Methods] ==========

        [ShowIf(nameof(CanCreate), false)]
        [Button]
        private void CreateNew()
        {
            // 建立 EditorData，並加到 tree 菜單中
            var editorData = new Example_EditorData(newDataName);
            editorData.SetChangeNameAction(Example_EditorWindow.Window.OnMenuNameChanged);
            var resultName = $"{titleName}/{editorData.DataName}";
            tree.Add(resultName, editorData, SdfIconType.JournalPlus);

            // 把新建的資料，加進List
            Example_EditorWindow.ADataSoData.Datas.Add(editorData.ARealData);
            Example_EditorWindow.BDataSoData.Datas.Add(editorData.BRealData);
            EditorSaveSystem.SaveFile.SetDisplayName(editorData.ARealData.DataID, newDataName);
            EditorSaveSystem.Save();
        }

        private bool CanCreate()
        {
            if (!string.IsNullOrEmpty(newDataName))
                return !IsSameDataName(newDataName);
            message = EditorWindowDescription.StringEmpty;
            return false;
        }

        private bool IsSameDataName(string dataName)
        {
            var editorDatas = Example_EditorWindow.EditorDatas;
            var editorData  = editorDatas.FirstOrDefault(data => string.Equals(data.DataName, dataName));
            if (editorData == null) return false;
            message = $"{EditorWindowDescription.DataIsExist} , Name: {dataName} , editorData:{editorData.DataName}";
            return true;
        }

    #endregion
    }
}