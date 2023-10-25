using SiberOdinEditor.Core;
using SiberUtility.Systems.FileSaves;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace SiberOdinEditor.Editor
{
    public abstract class BaseEditorSaveSystem<T> where T : IFileInfo, new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new T();
                return instance;
            }
        }

    #region ========== [Public Variables] ==========

        public static EditorSaveFile SaveFile;

    #endregion

    #region ========== [Private Variables] ==========

        public abstract string FileName { get; }

        public abstract string DataPath { get; }

    #endregion

    #region ========== [Public Methods] ==========

        [MenuItem(MenuHotKeys.EditorSaveFile_Delete)]
        public static void Delete()
        {
            Contract();
            SaveHelper.DeleteJson(Instance.FileName, Instance.DataPath);
            SaveFile = null;
            AssetDatabase.Refresh();
            LogFileMessage();
        }

        [MenuItem(MenuHotKeys.EditorSaveFile_Load)]
        public static void Load()
        {
            Contract();
            var editorSaveFile = SaveHelper.LoadFromJson<EditorSaveFile>(Instance.FileName, Instance.DataPath);
            if (editorSaveFile == null)
            {
                editorSaveFile = new EditorSaveFile();
                SaveHelper.SaveByJson(Instance.FileName, editorSaveFile, Instance.DataPath);
            }

            SaveFile = editorSaveFile;
            AssetDatabase.Refresh();
            LogFileMessage();
        }


        [MenuItem(MenuHotKeys.EditorSaveFile_Save)]
        public static void Save()
        {
            Contract();
            SaveHelper.SaveByJson(Instance.FileName, SaveFile, Instance.DataPath);
            AssetDatabase.Refresh();
            LogFileMessage();
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 檢查 </summary>
        private static void Contract()
        {
            Assert.IsFalse(string.IsNullOrEmpty(Instance.FileName), "FileName is NullOrEmpty");
            Assert.IsFalse(string.IsNullOrEmpty(Instance.DataPath), "DataPath is NullOrEmpty");
        }

        private static void LogFileMessage()
        {
            if (SaveHelper.IsShowLog)
                Debug.Log($"EditorSaveFile !=null: [{SaveFile != null}]");
        }

    #endregion
    }

    public interface IFileInfo
    {
        string FileName { get; }

        string DataPath { get; }
    }
}