using System;
using System.Collections.Generic;

namespace SiberOdinEditor.Core
{
    /// <summary> 編輯器存檔用 </summary>
    [Serializable]
    public class EditorSaveFile
    {
    #region ========== [Public Variables] ==========

        public List<EditorInfoData> infoDataList = new();

    #endregion

    #region ========== [Public Methods] ==========

        public string GetEditorDisplayName(string searchID)
        {
            return FindEditorInfoData(searchID)?.Name;
        }

        public void SetDisplayName(string searchID, string newName)
        {
            var data = FindEditorInfoData(searchID);
            if (data == null)
            {
                data = new EditorInfoData();
                data.SetName(newName);
                data.SetID(searchID);
                infoDataList.Add(data);
            }

            data.SetName(newName);
        }

    #endregion

    #region ========== [Private Methods] ==========

        protected EditorInfoData FindEditorInfoData(string searchID)
        {
            var data = infoDataList.Find(a => a.SearchID.Equals(searchID));
            return data;
        }

    #endregion
    }

    [Serializable]
    public class EditorInfoData
    {
        public string SearchID;
        public string Name;

        public EditorInfoData() { }
        
        public EditorInfoData(string searchID, string newName)
        {
            SetID(searchID);
            SetName(newName);
        }

        public void SetID(string id)
        {
            SearchID = id;
        }

        public void SetName(string newName)
        {
            Name = newName;
        }
    }
}