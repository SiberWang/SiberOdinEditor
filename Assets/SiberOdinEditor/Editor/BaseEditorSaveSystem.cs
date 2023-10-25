using SiberOdinEditor.Core;
using SiberUtility.Systems.FileSaves;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace SiberOdinEditor.Editor
{
    public interface IEditorSaveFileInfo
    {
    #region ========== [Public Variables] ==========

        string DataPath { get; }
        string FileName { get; }

    #endregion
    }

    public abstract class BaseEditorSaveSystem<T> where T : class, IEditorSaveFileInfo
    {
    #region ========== [Public Variables] ==========

        public static EditorSaveFile SaveFile;

        protected static T Instance;

    #endregion

    #region ========== [Private Variables] ==========

        private static string DataPath => Instance.DataPath;
        private static string FileName => Instance.FileName;

    #endregion

    #region ========== [Public Methods] ==========

        public static void Delete()
        {
            Contract();
            SaveHelper.DeleteJson(FileName, DataPath);
            SaveFile = null;
            AssetDatabase.Refresh();
            LogFileMessage();
        }

        public static void Load()
        {
            Contract();
            var editorSaveFile = SaveHelper.LoadFromJson<EditorSaveFile>(FileName, DataPath);
            if (editorSaveFile == null)
            {
                editorSaveFile = new EditorSaveFile();
                SaveHelper.SaveByJson(FileName, editorSaveFile, DataPath);
            }

            SaveFile = editorSaveFile;
            AssetDatabase.Refresh();
            LogFileMessage();
        }

        public static void Save()
        {
            Contract();
            SaveHelper.SaveByJson(FileName, SaveFile, DataPath);
            AssetDatabase.Refresh();
            LogFileMessage();
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 檢查 </summary>
        private static void Contract()
        {
            Assert.IsNotNull(Instance, "EditorSaveSystem's Instance is Null 需要建立一份 Static 的資料");
            Assert.IsFalse(string.IsNullOrEmpty(FileName), "FileName is NullOrEmpty");
            Assert.IsFalse(string.IsNullOrEmpty(DataPath), "DataPath is NullOrEmpty");
        }

        private static void LogFileMessage()
        {
            if (SaveHelper.IsShowLog)
                Debug.Log($"EditorSaveFile !=null: [{SaveFile != null}]");
        }

    #endregion
    }
}