using System.IO;
using System.Linq;
using Examples.Scripts.OdinWindows.Tools;
using SiberOdinEditor.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Examples.Scripts.OdinWindows
{
    /// <summary> Json 內容轉換視窗 </summary>
    public class JsonCustomEditorWindow : OdinEditorWindow
    {
    #region ========== [Public Variables] ==========

        [HideLabel] [PropertyOrder(-100)]
        [DisplayAsString(false, TextAlignment.Center, EnableRichText = true)]
        [ShowInInspector]
        private string TitleDisplay =>
            "<b><size=24><color=#FF5555>Json</color> <color=#66FFE1>內文轉換器</color></size></b>";

        [Space]
        [SerializeField] [LabelText("檔案")]
        public ScriptableObject assetFile;
        [SerializeField] [LabelText("檔案路徑")] [FolderPath]
        private string folderPath = "Assets/";
        [SerializeField] [LabelText("原本的內容")]
        private string originalString = "SomeName.AAA.BBB";
        [SerializeField] [LabelText("要覆蓋的內容")]
        private string coverString = "SomeName.AAA.BBB.CCC";

    #endregion

    #region ========== [Private Variables] ==========

        private static JsonCustomEditorWindow window;

    #endregion

    #region ========== [Public Methods] ==========

        [MenuItem(MenuHotKeys.JsonEditorWindow)]
        private static void OpenWindow()
        {
            window = window.OpenWindow<JsonCustomEditorWindow>(460, 240);
        }

    #endregion

    #region ========== [Private Methods] ==========

        [Button("覆蓋單一檔案")]
        public void ReplaceFileContent()
        {
            if (!CanEditorContent()) return;
            var jsonPath = AssetDatabase.GetAssetPath(assetFile);
            ReplaceContent(jsonPath);

            AssetDatabase.Refresh();
            Debug.Log($"[單一檔案] 替換完成！");
        }

        [Button("覆蓋路徑檔案")]
        public void ReplaceFilesContent()
        {
            if (!CanEditorAllContent(out var assetPaths)) return;
            foreach (var assetPath in assetPaths)
                ReplaceContent(assetPath);

            AssetDatabase.Refresh();
            Debug.Log($"[全部檔案] 替換完成！");
        }

        private void ReplaceContent(string jsonPath)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(jsonPath), "jsonPath == null or empty");
            Debug.Log($"jsonPath:{jsonPath}");
            var jsonContent = File.ReadAllText(jsonPath);
            jsonContent = jsonContent.Replace(originalString, coverString);
            File.WriteAllText(jsonPath, jsonContent);
            Debug.Log("JSON modified!");
        }

        private bool CanEditorContent()
        {
            if (assetFile == null)
            {
                Debug.LogError("AssetFile is null!");
                return false;
            }

            if (string.IsNullOrEmpty(originalString))
            {
                Debug.LogError("OriginalString is null or empty!");
                return false;
            }

            return true;
        }

        private bool CanEditorAllContent(out string[] assetPaths)
        {
            assetPaths = null;
            if (string.IsNullOrEmpty(folderPath))
            {
                Debug.LogError("FolderPath is null or empty!");
                return false;
            }

            var assetGuids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { folderPath });
            if (assetGuids.Length <= 0)
            {
                Debug.LogError("AssetGuids.Length <=0");
                return false;
            }

            Debug.Log($"assets.count :{assetGuids.Length}");
            assetPaths = assetGuids.Select(AssetDatabase.GUIDToAssetPath).ToArray();

            if (string.IsNullOrEmpty(originalString))
            {
                Debug.LogError("OriginalString is null or empty!");
                return false;
            }

            return true;
        }

    #endregion
    }
}