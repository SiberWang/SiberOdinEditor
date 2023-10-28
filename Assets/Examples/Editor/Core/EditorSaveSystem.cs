using SiberOdinEditor.Core;
using SiberOdinEditor.Tools;
using UnityEditor;
using UnityEngine;

namespace Examples.Editor.Core
{
    /// <summary> 編輯儲存系統 <br/>
    /// 專門處理Editor模式下，編輯器需要儲存的Json數據所用
    /// 並可以在上方Menu介面使用 Save , Load , Delete 功能
    /// 需繼承 IEditorSaveFileInfo , 並填入你需要的 DataPath , FileName
    /// </summary>
    public class EditorSaveSystem : BaseEditorSaveSystem<EditorSaveSystem>, IEditorSaveFileInfo
    {
    #region ========== [Public Variables] ==========

        public string DataPath => "Assets/Examples/Resources";
        public string FileName => "EditorSaveFile.json";

    #endregion

    #region ========== [Public Methods] ==========

        [MenuItem(MenuHotKeys.EditorSaveFile_Delete)]
        public static void DoDelete()
        {
            Delete();
        }

        [MenuItem(MenuHotKeys.EditorSaveFile_Load)]
        public static void DoLoad()
        {
            Load();
        }

        [MenuItem(MenuHotKeys.EditorSaveFile_Save)]
        public static void DoSave()
        {
            Save();
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 建立一份 EditorSaveSystem <br/>
        /// Editor模式下，Unity初始化會自動執行 <br/>
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Create()
        {
            if (Instance != null) return;
            Instance = new EditorSaveSystem();
            Debug.Log("Editor SaveSystem 準備就緒");
        }

    #endregion
    }
}