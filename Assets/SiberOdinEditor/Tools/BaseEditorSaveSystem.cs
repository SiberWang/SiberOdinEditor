using SiberOdinEditor.Core;
using SiberUtility.Systems.FileSaves;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace SiberOdinEditor.Tools
{
    public interface IEditorSaveFileInfo
    {
    #region ========== [Public Variables] ==========

        /// <summary> 資料儲存路徑 </summary>
        /// <example> 例如: Assets/Resources </example>
        string DataPath { get; }
        /// <summary> 檔案名稱 (記得+ .json) </summary>
        /// /// <example> 例如: EditorSaveFile.json </example>
        string FileName { get; }

    #endregion
    }

    /// <summary> 基底 - 編輯儲存系統 </summary>
    /// <typeparam name="T"> 請放入有繼承 IEditorSaveFileInfo 的客製化腳本 </typeparam>
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

        public static void Save(EditorSaveFile newSaveFile = null)
        {
            newSaveFile ??= SaveFile;
            Contract();
            SaveHelper.SaveByJson(FileName, newSaveFile, DataPath);
            AssetDatabase.Refresh();
            LogFileMessage();
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 檢查實例、名稱、路徑 </summary>
        private static void Contract()
        {
            Assert.IsNotNull(Instance, "EditorSaveSystem's Instance is Null 需要建立一份 Static 的資料");
            Assert.IsFalse(string.IsNullOrEmpty(FileName), "FileName is NullOrEmpty");
            Assert.IsFalse(string.IsNullOrEmpty(DataPath), "DataPath is NullOrEmpty");
        }

        /// <summary> Debug.Log 紀錄檔案是否存在 </summary>
        private static void LogFileMessage()
        {
            if (SaveHelper.IsShowLog)
                Debug.Log($"EditorSaveFile !=null: [{SaveFile != null}]");
        }

    #endregion
    }
}