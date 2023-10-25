using SiberOdinEditor.Core;
using SiberOdinEditor.Editor;
using UnityEditor;
using UnityEngine;

namespace Examples.Editor.Core
{
    public class EditorSaveSystem : BaseEditorSaveSystem<EditorSaveSystem>, IEditorSaveFileInfo
    {
    #region ========== [Public Variables] ==========

        public string DataPath => "Assets/Examples/Resources";
        public string FileName => "EditorSaveFile.json";

    #endregion

        [InitializeOnLoadMethod]
        private static void Create()
        {
            if (Instance != null) return;
            Instance = new EditorSaveSystem();
            Debug.Log($"Editor SaveSystem 準備就緒");
        }

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
    }
}