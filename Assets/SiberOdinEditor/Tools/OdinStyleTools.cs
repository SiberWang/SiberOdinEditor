using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Examples.Scripts.OdinWindows.Tools
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
            grayLittleLabel.fontSize         = 12;
            grayLittleLabel.fontStyle        = FontStyle.Italic;
            grayLittleLabel.alignment        = TextAnchor.MiddleLeft;
            grayLittleLabel.margin           = new RectOffset(0, 0, 0, 0);
            grayLittleLabel.stretchHeight    = true;
            grayLittleLabel.stretchWidth     = false;
            return grayLittleLabel;
        }

        /// <summary> 轉換 SdfIconType 為 Texture2D </summary>
        /// 解決有些icon Odin 只給抓 image (texture) 的困擾！！
        public static Texture2D GetSdfIcon(SdfIconType sdfIconType, Color iconColor, int size)
        {
            var texture2D = SdfIcons.CreateTransparentIconTexture(sdfIconType, iconColor, size, size, 0);
            return texture2D;
        }

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

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton(string label, SdfIconType sdfIconType, Color iconColor, int size = 18)
        {
            var resultLabel = !string.IsNullOrEmpty(label) ? $" {label} " : string.Empty;
            var guiContent  = CustomGUIContent(resultLabel, label, sdfIconType, iconColor, size);
            return SirenixEditorGUI.ToolbarButton(guiContent);
        }

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton(string label, SdfIconType sdfIconType, int size = 18)
        {
            return CustomToolbarButton(label, sdfIconType, Color.white, size);
        }

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton(SdfIconType sdfIconType)
        {
            return CustomToolbarButton(string.Empty, sdfIconType, Color.white, 18);
        }

        /// <summary> 實現可以使用 SdfIconType 的 ToolbarButton </summary>
        public static bool CustomToolbarButton(SdfIconType sdfIconType, Color iconColor, int size = 18)
        {
            return CustomToolbarButton(string.Empty, sdfIconType, iconColor, size);
        }
    }
}