using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace SiberOdinEditor.Tools
{
    /// <summary> 自製 GUIStyles </summary>
    public static class OdinStyleTools
    {
        private static GUIStyle titleLabel;
        private static GUIStyle grayLittleLabel;
        private static GUIStyle lightNameLabel;
        private static GUIStyle centerLabel;

        public static GUIStyle TitleLabel()
        {
            return titleLabel ??= new GUIStyle(SirenixGUIStyles.BoldLabel)
            {
                fontSize  = 14,
                fontStyle = FontStyle.Bold,
            };
        }

        public static GUIStyle CenterLabel()
        {
            centerLabel               ??= new GUIStyle(GUI.skin.label);
            centerLabel.alignment     =   TextAnchor.MiddleCenter;
            centerLabel.margin        =   new RectOffset(0, 0, 0, 0);
            centerLabel.stretchHeight =   true;
            centerLabel.stretchWidth  =   false;
            return centerLabel;
        }

        public static GUIStyle LightNameLabel()
        {
            lightNameLabel                  ??= new GUIStyle(GUI.skin.label);
            lightNameLabel.normal.textColor =   Color.yellow;
            lightNameLabel.hover.textColor  =   Color.green;
            lightNameLabel.fontSize         =   14;
            lightNameLabel.fontStyle        =   FontStyle.Bold;
            lightNameLabel.alignment        =   TextAnchor.MiddleCenter;
            lightNameLabel.margin           =   new RectOffset(0, 0, 0, 0);
            lightNameLabel.stretchHeight    =   true;
            // selectNameLabel.stretchWidth  = false;
            return lightNameLabel;
        }

        public static GUIStyle GrayLittleLabel()
        {
            grayLittleLabel ??= new GUIStyle(GUI.skin.label);
            var normalTextColor = Color.gray;
            grayLittleLabel.normal.textColor = normalTextColor;
            grayLittleLabel.hover.textColor  = normalTextColor;
            grayLittleLabel.active.textColor = normalTextColor;
            grayLittleLabel.fontSize         = Default_TextSize;
            grayLittleLabel.fontStyle        = FontStyle.Italic;
            grayLittleLabel.alignment        = TextAnchor.MiddleLeft;
            grayLittleLabel.margin           = new RectOffset(0, 0, 0, 0);
            grayLittleLabel.stretchHeight    = true;
            grayLittleLabel.stretchWidth     = false;
            return grayLittleLabel;
        }

        /// <summary> 轉換 SdfIconType 為 Texture2D <br/>
        /// 解決有些icon Odin 只給抓 image (texture) 的困擾！！
        /// </summary>
        public static Texture2D GetSdfIcon(SdfIconType sdfIconType, Color iconColor, int size)
        {
            var texture2D = SdfIcons.CreateTransparentIconTexture(sdfIconType, iconColor, size, size, 0);
            return texture2D;
        }

        /// <summary> 自製GUIContent <br/>
        /// 可以放 SdfIconType , 改顏色 , 設定Icon Size
        /// </summary>
        public static GUIContent CustomGUIContent
            (string text, string tooltip, SdfIconType sdfIconType, Color iconColor, int size)
        {
            var content = new GUIContent
            {
                text    = text,
                image   = GetSdfIcon(sdfIconType, iconColor, size),
                tooltip = tooltip
            };
            return content;
        }

        public static GUIContent CustomGUIContent(SdfIconType sdfIconType, Color iconColor, int size)
        {
            var content = new GUIContent
            {
                text    = string.Empty,
                image   = GetSdfIcon(sdfIconType, iconColor, size),
                tooltip = string.Empty
            };
            return content;
        }

        public static GUIContent CustomGUIContent(string text, string tooltip)
        {
            var content = new GUIContent
            {
                text    = text,
                image   = null,
                tooltip = tooltip
            };
            return content;
        }

    #region ========== [CustomToolbarButton] ==========

        private const  int   Default_IconSize  = 18;
        private const  int   Default_TextSize  = 12;
        private const  bool  Default_IsExpand  = false;
        private static Color Default_IconColor = Color.white;

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton
        (string      label,
         string      tooltip,
         SdfIconType sdfIconType,
         Color       iconColor,
         int         iconSize = Default_IconSize,
         int         textSize = Default_TextSize,
         bool        isExpand = Default_IsExpand)
        {
            var resultLabel = !string.IsNullOrEmpty(label) ? $" {label} " : string.Empty;
            var guiContent  = CustomGUIContent(resultLabel, tooltip, sdfIconType, iconColor, iconSize);
            var guiStyle    = SirenixGUIStyles.ToolbarButton;
            guiStyle.fontSize = textSize;
            var options = GUILayoutOptions.Height(SirenixEditorGUI.currentDrawingToolbarHeight).ExpandWidth(isExpand);

            if (!GUILayout.Button(guiContent, guiStyle, options))
                return false;
            GUIHelper.RemoveFocusControl();
            GUIHelper.RequestRepaint();
            return true;
            // return SirenixEditorGUI.ToolbarButton(guiContent);
        }

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton
        (string      label,
         SdfIconType sdfIconType,
         Color       iconColor,
         int         iconSize = Default_IconSize,
         int         textSize = Default_TextSize,
         bool        isExpand = Default_IsExpand) =>
            CustomToolbarButton(label, label, sdfIconType, iconColor, iconSize, textSize, isExpand);

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton
        (string      label,
         SdfIconType sdfIconType,
         int         iconSize = Default_IconSize,
         int         textSize = Default_TextSize,
         bool        isExpand = Default_IsExpand) =>
            CustomToolbarButton(label, label, sdfIconType, Default_IconColor, iconSize, textSize, isExpand);

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton
        (SdfIconType sdfIconType,
         int         iconSize = Default_IconSize,
         bool        isExpand = Default_IsExpand) =>
            CustomToolbarButton(string.Empty, sdfIconType, Default_IconColor, iconSize: iconSize, isExpand: isExpand);

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton
        (SdfIconType sdfIconType,
         Color       iconColor,
         int         iconSize = Default_IconSize,
         bool        isExpand = Default_IsExpand) =>
            CustomToolbarButton(string.Empty, sdfIconType, iconColor, iconSize: iconSize, isExpand: isExpand);

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton
        (string label,
         string tooltip,
         int    textSize = Default_TextSize,
         bool   isExpand = Default_IsExpand)
        {
            var resultLabel = !string.IsNullOrEmpty(label) ? $" {label} " : string.Empty;
            var guiContent  = CustomGUIContent(resultLabel, tooltip);
            var guiStyle    = SirenixGUIStyles.ToolbarButton;
            guiStyle.fontSize = textSize;
            var options = GUILayoutOptions.Height(SirenixEditorGUI.currentDrawingToolbarHeight).ExpandWidth(isExpand);

            if (!GUILayout.Button(guiContent, guiStyle, options))
                return false;
            GUIHelper.RemoveFocusControl();
            GUIHelper.RequestRepaint();
            return true;
        }

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton
            (string label, int textSize = Default_TextSize, bool isExpand = Default_IsExpand)
        {
            return CustomToolbarButton(label, label, textSize, isExpand);
        }

    #endregion

        /// <summary> OdinMenuStyle 設定複製 </summary>
        public static OdinMenuStyle CloneNewStyle(this OdinMenuStyle current, OdinMenuStyle newStyle)
        {
            current.Height                         = newStyle.Height;
            current.Offset                         = newStyle.Offset;
            current.LabelVerticalOffset            = newStyle.LabelVerticalOffset;
            current.IndentAmount                   = newStyle.IndentAmount;
            current.IconSize                       = newStyle.IconSize;
            current.IconOffset                     = newStyle.IconOffset;
            current.NotSelectedIconAlpha           = newStyle.NotSelectedIconAlpha;
            current.IconPadding                    = newStyle.IconPadding;
            current.DrawFoldoutTriangle            = newStyle.DrawFoldoutTriangle;
            current.TriangleSize                   = newStyle.TriangleSize;
            current.TrianglePadding                = newStyle.TrianglePadding;
            current.AlignTriangleLeft              = newStyle.AlignTriangleLeft;
            current.Borders                        = newStyle.Borders;
            current.BorderPadding                  = newStyle.BorderPadding;
            current.BorderAlpha                    = newStyle.BorderAlpha;
            current.SelectedColorDarkSkin          = newStyle.SelectedColorDarkSkin;
            current.SelectedInactiveColorDarkSkin  = newStyle.SelectedInactiveColorDarkSkin;
            current.SelectedColorLightSkin         = newStyle.SelectedColorLightSkin;
            current.SelectedInactiveColorLightSkin = newStyle.SelectedInactiveColorLightSkin;
            return current;
        }
    }
}