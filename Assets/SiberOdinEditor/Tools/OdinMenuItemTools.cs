using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SiberOdinEditor.Core;
using SiberOdinEditor.Core.Utils;
using SiberUtility.Tools;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SiberOdinEditor.Tools
{
    /// <summary> MenuItem 工具 </summary>
    /// 用於歸納自定義編輯寫法的地方
    public static class OdinMenuItemTools
    {
        private static readonly Color SelectColor_Yellow = ColorHelper.GetColorByHtml("FFEB4B", 0.3f);
        private static readonly Color SelectColor_Green  = ColorHelper.GetColorByHtml("9CFF4A", 0.3f);

        /// <summary> 依照 IDataName 的名稱設定檔案名稱 </summary>
        /// <param name="menuPath"> 同為menu名稱 </param>
        /// <param name="assetFolderPath"> 資料夾路徑 </param>
        /// <param name="includeSubDirectories"> 是否包含底下子項目? </param>
        /// <param name="flattenSubDirectories"> 是否擴展子項目? </param>
        /// <typeparam name="A">IDataInfo</typeparam>
        /// <returns>IEnumerable OdinMenuItem</returns>
        public static IEnumerable<OdinMenuItem> AddAllAssetsAtPath<A>
        (this OdinMenuTree tree,                          string menuPath, string assetFolderPath,
         bool              includeSubDirectories = false, bool   flattenSubDirectories = false) where A : IEditorDataInfo
        {
        #region --- Origin Odin AddAllAssetsAtPath Content ---

            assetFolderPath = (assetFolderPath ?? "").TrimEnd('/') + "/";
            string lower = assetFolderPath.ToLower();
            if (!lower.StartsWith("assets/") && !lower.StartsWith("packages/"))
                assetFolderPath = "Assets/" + assetFolderPath;
            assetFolderPath = assetFolderPath.TrimEnd('/') + "/";
            var strings = AssetDatabase.GetAllAssetPaths().Where(x =>
            {
                if (includeSubDirectories)
                    return x.StartsWith(assetFolderPath, StringComparison.InvariantCultureIgnoreCase);
                return string.Compare(PathUtilities.GetDirectoryName(x).Trim('/'), assetFolderPath.Trim('/'), true) ==
                       0;
            });
            menuPath = menuPath ?? "";
            menuPath = menuPath.TrimStart('/');
            HashSet<OdinMenuItem> result = new HashSet<OdinMenuItem>();

        #endregion

            foreach (string str1 in strings)
            {
                var asset = AssetDatabase.LoadAssetAtPath(str1, typeof(A));
                if (asset == null) continue;
                if (asset is not IEditorDataInfo dataName) continue; // 一定要有 IDataName

                var withoutExtension = Path.GetFileNameWithoutExtension(str1);
                var path             = menuPath;
                if (!flattenSubDirectories)
                {
                    string str2 =
                        (PathUtilities.GetDirectoryName(str1).TrimEnd('/') + "/").Substring(assetFolderPath.Length);
                    if (str2.Length != 0)
                        path = path.Trim('/') + "/" + str2;
                }

                path = path.Trim('/') + "/" + withoutExtension;
                SplitMenuPath(path, out path);

                var displayDataName = dataName.DataName; // 顯示指定名稱
                var odinMenuItem    = NewOdinMenuItem(tree, displayDataName, asset);
                tree.AddMenuItemAtPath(result, path, odinMenuItem);
            }

            return result;
        }

        /// <summary> 依照 IDataName 的名稱設定檔案名稱 </summary>
        /// , Title 名稱自動寫為 typeof(T).Name
        /// <param name="assetFolderPath"> 資料夾路徑 </param>
        /// <param name="includeSubDirectories"> 是否包含底下子項目? </param>
        /// <param name="flattenSubDirectories"> 是否擴展子項目? </param>
        /// <typeparam name="A">IDataInfo</typeparam>
        /// <returns>IEnumerable OdinMenuItem</returns>
        public static IEnumerable<OdinMenuItem> AddAllAssetsAtPath<A>
        (this OdinMenuTree tree, string assetFolderPath, bool includeSubDirectories = false,
         bool              flattenSubDirectories = false) where A : IEditorDataInfo
        {
            return AddAllAssetsAtPath<A>(tree, $"{typeof(A).Name}s", assetFolderPath, includeSubDirectories,
                                         flattenSubDirectories);
        }

        /// <summary> Title Create Data 專頁 </summary>
        /// <param name="tree">this OdinMenuTree</param>
        /// <param name="titleName"> 標題名稱 </param>
        /// <typeparam name="A"> 通常為 SO + IData B </typeparam>
        /// <typeparam name="B"> 通常為 XXX.Data() </typeparam>
        public static OdinMenuItem AddTitleDataPanel<A, B>(this OdinMenuTree tree, string titleName)
        where A : ScriptableObject, IEditorData<B>
        where B : class, new()
        {
            tree.Add($"{titleName}", new CreateNewData<A, B>(), SdfIconType.JournalPlus);
            var odinMenuItem = tree.GetMenuItem(titleName).SetTitleStyle();
            return odinMenuItem;
        }

        /// <summary> 改變 Style (Title) </summary>
        public static OdinMenuItem SetTitleStyle(this OdinMenuItem odinMenuItem)
        {
            odinMenuItem.Style = new OdinMenuStyle
            {
                SelectedLabelStyle    = OdinStyleTools.TitleLabel(),
                SelectedColorDarkSkin = SelectColor_Green
            };
            return odinMenuItem;
        }

        /// <summary> Create Data 專頁 </summary>
        /// , 標題名稱自動為 typeof(A).Name
        /// <param name="tree"> this OdinMenuTree </param>
        /// <typeparam name="A"> 通常為 SO + IData B </typeparam>
        /// <typeparam name="B"> 通常為 XXX.Data() </typeparam>
        public static OdinMenuItem AddTitleDataPanel<A, B>(this OdinMenuTree tree)
        where A : ScriptableObject, IEditorData<B>
        where B : class, new()
        {
            return AddTitleDataPanel<A, B>(tree, $"{typeof(A).Name}s");
        }


        /// <summary> AllAssets + TitleDataPanel </summary>
        /// , 標題名稱自動為 typeof(A).Name
        /// <param name="tree"> this OdinMenuTree </param>
        /// <param name="path"> 資料夾路徑 </param>
        /// <typeparam name="A"> 通常為 SO + IData B </typeparam>
        /// <typeparam name="B"> 通常為 XXX.Data() </typeparam>
        public static (OdinMenuItem, IEnumerable<OdinMenuItem>) AddAssetsAndTitle<A, B>
            (this OdinMenuTree tree, string path)
        where A : ScriptableObject, IEditorData<B>
        where B : class, new()
        {
            return AddAssetsAndTitle<A, B>(tree, $"{typeof(A).Name}s", path);
        }

        /// <summary> AllAssets + TitleDataPanel </summary>
        /// <param name="tree"> this OdinMenuTree </param>
        /// <param name="titleName"> 標題名稱 </param>
        /// <param name="path"> 資料夾路徑 </param>
        /// <typeparam name="A"> 通常為 SO + IData B </typeparam>
        /// <typeparam name="B"> 通常為 XXX.Data() </typeparam>
        public static (OdinMenuItem, IEnumerable<OdinMenuItem>) AddAssetsAndTitle<A, B>
            (this OdinMenuTree tree, string titleName, string path)
        where A : ScriptableObject, IEditorData<B>
        where B : class, new()
        {
            var a = AddTitleDataPanel<A, B>(tree, titleName);
            var b = AddAllAssetsAtPath<A>(tree, titleName, path, true, true);
            return (a, b);
        }

        /// <summary> 自定義 OdinMenuItem 詳細設定 </summary>
        /// <param name="tree"> OdinMenuTree </param>
        /// <param name="name"> 秀出的名稱 </param>
        /// <param name="asset"> 物件 </param>
        /// <returns> OdinMenuItem </returns>
        public static OdinMenuItem NewOdinMenuItem(OdinMenuTree tree, string name, Object asset)
        {
            var odinMenuItem = new OdinMenuItem(tree, name, asset);
            odinMenuItem.Style = new OdinMenuStyle
            {
                SelectedLabelStyle    = SirenixGUIStyles.BoldLabelCentered,
                SelectedColorDarkSkin = SelectColor_Yellow
            };
            odinMenuItem.OnRightClick = _ => OdinWindowTools.RightClickPingAsset(asset);
            odinMenuItem.AddIcon(SdfIconType.JournalText);
            return odinMenuItem;
        }

        /// <summary> 截自於 OdinMenuTreeExtensions.SplitMenuPath <br/>
        /// 由於 Odin 把它設為 Private 所以自己複製一份過來
        /// </summary>
        public static void SplitMenuPath(string menuPath, out string path)
        {
            menuPath = menuPath.Trim('/');
            var length = menuPath.LastIndexOf('/');
            path = length == -1 ? "" : menuPath.Substring(0, length);
        }
        
        /// <summary> 截自於 OdinMenuTreeExtensions.SplitMenuPath <br/>
        /// 由於 Odin 把它設為 Private 所以自己複製一份過來
        /// </summary>
        /// <param name="menuPath"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public static void SplitMenuPath(string menuPath, out string path, out string name)
        {
            menuPath = menuPath.Trim('/');
            var length = menuPath.LastIndexOf('/');
            if (length == -1)
            {
                path = "";
                name = menuPath;
            }
            else
            {
                path = menuPath.Substring(0, length);
                name = menuPath.Substring(length + 1);
            }
        }

        /// <summary> 針對沒有SdfIcon的項目，填充 SdfIcon </summary>
        public static void FillSdfIcon(this OdinMenuTree tree, SdfIconType sdfIconType)
        {
            foreach (var odinMenuItem in tree.EnumerateTree())
            {
                if (odinMenuItem.SdfIcon != SdfIconType.None) continue;
                odinMenuItem.AddIcon(sdfIconType);
            }
        }
    }
}