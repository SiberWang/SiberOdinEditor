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

#region ========== [BaseEditorSaveSystem T,D] ==========

    /// <summary> 基底 - 編輯儲存系統 </summary>
    /// <typeparam name="T"> 請放入有繼承 IEditorSaveFileInfo 的客製化腳本 </typeparam>
    /// <typeparam name="D"> 自定義 Class Data , ex: EditorSaveFile </typeparam>
    public abstract class BaseEditorSaveSystem<T, D> where T : class, IEditorSaveFileInfo
                                                     where D : class, new()
    {
    #region ========== [Public Variables] ==========

        public static D SaveFile;

        protected static T Instance;

    #endregion

    #region ========== [Private Variables] ==========

        private static string DataPath => Instance.DataPath;
        private static string FileName => Instance.FileName;

    #endregion

    #region ========== [Public Methods] ==========

        public static void Delete()
        {
            SaveCheck.Contract(Instance, FileName, DataPath);
            SaveHelper.DeleteJson(FileName, DataPath);
            SaveFile = null;
            AssetDatabase.Refresh();
            SaveCheck.LogFileMessage(SaveFile);
        }

        public static void Load()
        {
            SaveCheck.Contract(Instance, FileName, DataPath);
            var editorSaveFile = SaveHelper.LoadFromJson<D>(FileName, DataPath);
            if (editorSaveFile == null)
            {
                editorSaveFile = new D();
                SaveHelper.SaveByJson(FileName, editorSaveFile, DataPath);
            }

            SaveFile = editorSaveFile;
            AssetDatabase.Refresh();
            SaveCheck.LogFileMessage(SaveFile);
        }

        public static void Save(D newSaveFile = null)
        {
            newSaveFile ??= SaveFile;
            SaveCheck.Contract(Instance, FileName, DataPath);
            SaveHelper.SaveByJson(FileName, newSaveFile, DataPath);
            AssetDatabase.Refresh();
            SaveCheck.LogFileMessage(SaveFile);
        }

        public static A GetSaveFile<A>() where A : class
        {
            return SaveFile as A;
        }

    #endregion
    }

#endregion

#region ========== [BaseEditorSaveSystem T] ==========

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
            SaveCheck.Contract(Instance, FileName, DataPath);
            SaveHelper.DeleteJson(FileName, DataPath);
            SaveFile = null;
            AssetDatabase.Refresh();
            SaveCheck.LogFileMessage(SaveFile);
        }

        public static void Load()
        {
            SaveCheck.Contract(Instance, FileName, DataPath);
            var editorSaveFile = SaveHelper.LoadFromJson<EditorSaveFile>(FileName, DataPath);
            if (editorSaveFile == null)
            {
                editorSaveFile = new EditorSaveFile();
                SaveHelper.SaveByJson(FileName, editorSaveFile, DataPath);
            }

            SaveFile = editorSaveFile;
            AssetDatabase.Refresh();
            SaveCheck.LogFileMessage(SaveFile);
        }

        public static void Save(EditorSaveFile newSaveFile = null)
        {
            newSaveFile ??= SaveFile;
            SaveCheck.Contract(Instance, FileName, DataPath);
            SaveHelper.SaveByJson(FileName, newSaveFile, DataPath);
            AssetDatabase.Refresh();
            SaveCheck.LogFileMessage(SaveFile);
        }

        public static A GetSaveFile<A>() where A : EditorSaveFile
        {
            return SaveFile as A;
        }

    #endregion
    }

#endregion

#region ========== [Common] ==========

    internal struct SaveCheck
    {
    #region ========== [Private Methods] ==========

        /// <summary> 檢查實例、名稱、路徑 </summary>
        public static void Contract<T>
            (T instance, string fileName, string dataPath) where T : class, IEditorSaveFileInfo
        {
            Assert.IsNotNull(instance, "EditorSaveSystem's Instance is Null 需要建立一份 Static 的資料");
            Assert.IsFalse(string.IsNullOrEmpty(fileName), "FileName is NullOrEmpty");
            Assert.IsFalse(string.IsNullOrEmpty(dataPath), "DataPath is NullOrEmpty");
        }

        /// <summary> Debug.Log 紀錄檔案是否存在 </summary>
        public static void LogFileMessage<T>(T saveFile) where T : class
        {
            if (SaveHelper.IsShowLog)
                Debug.Log($"EditorSaveFile !=null: [{saveFile != null}]");
        }

    #endregion
    }

#endregion

    // <Lazy Example>
    // public class EditorSaveSystem : BaseEditorSaveSystem<EditorSaveSystem>, IEditorSaveFileInfo
    // {
    // #region ========== [Public Variables] ==========
    //
    //     public string DataPath => "Assets/Resources";
    //     public string FileName => "EditorSaveFile.json";
    //
    // #endregion
    //
    // #region ========== [Public Methods] ==========
    //
    //     [MenuItem("EditorSaveSystem/Delete")]
    //     public static void DoDelete()
    //     {
    //         Delete();
    //     }
    //
    //     [MenuItem("EditorSaveSystem/Load")]
    //     public static void DoLoad()
    //     {
    //         Load();
    //     }
    //
    //     [MenuItem("EditorSaveSystem/Save")]
    //     public static void DoSave()
    //     {
    //         Save();
    //     }
    //
    // #endregion
    //
    // #region ========== [Private Methods] ==========
    //
    //     /// <summary> 建立一份 EditorSaveSystem <br/>
    //     /// Editor模式下，Unity初始化會自動執行 <br/>
    //     /// </summary>
    //     [InitializeOnLoadMethod]
    //     private static void Create()
    //     {
    //         if (Instance != null) return;
    //         Instance = new EditorSaveSystem();
    //         Debug.Log("Editor SaveSystem 準備就緒");
    //     }
    //
    // #endregion
    // }
}