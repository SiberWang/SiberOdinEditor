using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace SiberOdinEditor.Core.Utils
{
    /// <summary> 自製 OdinMenuStyle </summary>
    [Serializable]
    public class CustomOdinMenuStyle
    {
        [BoxGroup("General")] public int   Height;
        [BoxGroup("General")] public float Offset;
        [BoxGroup("General")] public float LabelVerticalOffset;
        [BoxGroup("General")] public float IndentAmount;

        [BoxGroup("Icons")] public float IconSize;
        [BoxGroup("Icons")] public float IconOffset;
        [BoxGroup("Icons")]
        [Range(0.0f, 1f)] public float NotSelectedIconAlpha;
        [BoxGroup("Icons")] public float IconPadding;

        [BoxGroup("Triangle")] public bool  DrawFoldoutTriangle;
        [BoxGroup("Triangle")] public float TriangleSize;
        [BoxGroup("Triangle")] public float TrianglePadding;
        [BoxGroup("Triangle")] public bool  AlignTriangleLeft;

        [BoxGroup("Borders")] public bool Borders;
        [BoxGroup("Borders")]
        [EnableIf("Borders")] public float BorderPadding;
        [BoxGroup("Borders")]
        [EnableIf("Borders")]
        [Range(0.0f, 1f)] public float BorderAlpha;

        [BoxGroup("Colors")] public Color SelectedColorDarkSkin;
        [BoxGroup("Colors")] public Color SelectedInactiveColorDarkSkin;
        [BoxGroup("Colors")] public Color SelectedColorLightSkin;
        [BoxGroup("Colors")] public Color SelectedInactiveColorLightSkin;

        public CustomOdinMenuStyle() => Init();

        public CustomOdinMenuStyle(OdinMenuStyle style) => SetData(style);

        public void Init()
        {
            Height                         = 30;
            Offset                         = 16;
            LabelVerticalOffset            = 0f;
            IndentAmount                   = 15f;
            IconSize                       = 16f;
            IconOffset                     = 0f;
            NotSelectedIconAlpha           = 0.85f;
            IconPadding                    = 3f;
            DrawFoldoutTriangle            = true;
            TriangleSize                   = 17f;
            TrianglePadding                = 8f;
            AlignTriangleLeft              = false;
            Borders                        = true;
            BorderPadding                  = 13f;
            BorderAlpha                    = 0.5f;
            SelectedColorDarkSkin          = new Color(0.243f, 0.373f, 0.588f, 1f);
            SelectedInactiveColorDarkSkin  = new Color(0.838f, 0.838f, 0.838f, 0.134f);
            SelectedColorLightSkin         = new Color(0.243f, 0.49f, 0.9f, 1f);
            SelectedInactiveColorLightSkin = new Color(0.5f, 0.5f, 0.5f, 1f);
        }

        public void SetData(CustomOdinMenuStyle customStyle)
        {
            Height                         = customStyle.Height;
            Offset                         = customStyle.Offset;
            LabelVerticalOffset            = customStyle.LabelVerticalOffset;
            IndentAmount                   = customStyle.IndentAmount;
            IconSize                       = customStyle.IconSize;
            IconOffset                     = customStyle.IconOffset;
            NotSelectedIconAlpha           = customStyle.NotSelectedIconAlpha;
            IconPadding                    = customStyle.IconPadding;
            DrawFoldoutTriangle            = customStyle.DrawFoldoutTriangle;
            TriangleSize                   = customStyle.TriangleSize;
            TrianglePadding                = customStyle.TrianglePadding;
            AlignTriangleLeft              = customStyle.AlignTriangleLeft;
            Borders                        = customStyle.Borders;
            BorderPadding                  = customStyle.BorderPadding;
            BorderAlpha                    = customStyle.BorderAlpha;
            SelectedColorDarkSkin          = customStyle.SelectedColorDarkSkin;
            SelectedInactiveColorDarkSkin  = customStyle.SelectedInactiveColorDarkSkin;
            SelectedColorLightSkin         = customStyle.SelectedColorLightSkin;
            SelectedInactiveColorLightSkin = customStyle.SelectedInactiveColorLightSkin;
        }

        public void SetData(OdinMenuStyle style)
        {
            Height                         = style.Height;
            Offset                         = style.Offset;
            LabelVerticalOffset            = style.LabelVerticalOffset;
            IndentAmount                   = style.IndentAmount;
            IconSize                       = style.IconSize;
            IconOffset                     = style.IconOffset;
            NotSelectedIconAlpha           = style.NotSelectedIconAlpha;
            IconPadding                    = style.IconPadding;
            DrawFoldoutTriangle            = style.DrawFoldoutTriangle;
            TriangleSize                   = style.TriangleSize;
            TrianglePadding                = style.TrianglePadding;
            AlignTriangleLeft              = style.AlignTriangleLeft;
            Borders                        = style.Borders;
            BorderPadding                  = style.BorderPadding;
            BorderAlpha                    = style.BorderAlpha;
            SelectedColorDarkSkin          = style.SelectedColorDarkSkin;
            SelectedInactiveColorDarkSkin  = style.SelectedInactiveColorDarkSkin;
            SelectedColorLightSkin         = style.SelectedColorLightSkin;
            SelectedInactiveColorLightSkin = style.SelectedInactiveColorLightSkin;
        }

        /// <summary> 轉換為 OdinMenuStyle(New) </summary>
        public OdinMenuStyle GetNewOdinMenuStyle()
        {
            return new OdinMenuStyle
            {
                Height                         = Height,
                Offset                         = Offset,
                LabelVerticalOffset            = LabelVerticalOffset,
                IndentAmount                   = IndentAmount,
                IconSize                       = IconSize,
                IconOffset                     = IconOffset,
                NotSelectedIconAlpha           = NotSelectedIconAlpha,
                IconPadding                    = IconPadding,
                DrawFoldoutTriangle            = DrawFoldoutTriangle,
                TriangleSize                   = TriangleSize,
                TrianglePadding                = TrianglePadding,
                AlignTriangleLeft              = AlignTriangleLeft,
                Borders                        = Borders,
                BorderPadding                  = BorderPadding,
                BorderAlpha                    = BorderAlpha,
                SelectedColorDarkSkin          = SelectedColorDarkSkin,
                SelectedInactiveColorDarkSkin  = SelectedInactiveColorDarkSkin,
                SelectedColorLightSkin         = SelectedColorLightSkin,
                SelectedInactiveColorLightSkin = SelectedInactiveColorLightSkin
            };
        }
    }
}