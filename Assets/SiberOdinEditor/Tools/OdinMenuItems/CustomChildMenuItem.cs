using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace SiberOdinEditor.Tools.OdinMenuItems
{
    /// <summary> 自製子項目的OdinMenuItem <br/>
    /// 加了顆刪除按鈕，可放入自定義事件
    /// </summary>
    public class CustomChildMenuItem : OdinMenuItem
    {
        private readonly Action onDeleteAction;

        public float       ButtonOffset = 150;
        public int         ButtonSize   = 20;
        public SdfIconType SDFIconType  = SdfIconType.XSquareFill;
        public int         IconSize     = 25;
        public Color       IconColor    = new Color(0.43f, 0.41f, 0.41f);

        public CustomChildMenuItem
            (OdinMenuTree tree, string name, object value, Action onDeleteAction = null) : base(tree, name, value)
        {
            this.onDeleteAction = onDeleteAction;
        }

        protected override void OnDrawMenuItem(Rect rect, Rect labelRect)
        {
            labelRect.x += ButtonOffset - ButtonSize;

            var skinButton = OdinStyleTools.CustomGUIContent(SDFIconType, IconColor, IconSize);
            var guiStyle   = new GUIStyle(SirenixGUIStyles.IconButton);
            if (GUI.Button(labelRect.AlignMiddle(ButtonSize).AlignLeft(ButtonSize), skinButton, guiStyle))
                onDeleteAction?.Invoke();
        }
    }
}