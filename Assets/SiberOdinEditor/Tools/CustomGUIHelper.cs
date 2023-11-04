using System;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace SiberOdinEditor.Tools
{
    /// <summary> Custom Odin GUIHelper <br/>
    /// 有自製一些方法 
    /// </summary>
    public class CustomGUIHelper
    {
    #region ========== [Private Variables] ==========

        private static readonly GUIScopeStack<Color> ColorBackgroundStack = new GUIScopeStack<Color>();

    #endregion

    #region ========== [Public Methods] ==========

        /// <summary>
        /// 改 Editor Button , label 之類的背景顏色 <br/>
        /// 懶人法，不想在自己放 push , pop , 那就用這個
        /// </summary>
        public static void ColorBackGround(Color color, Action action)
        {
            PushBackgroundColor(color , true);
            action?.Invoke();
            PopBackgroundColor();
        }

        public static void PopBackgroundColor() => GUI.backgroundColor = ColorBackgroundStack.Pop();

        /// <summary>
        /// Set Background Color <br/>
        /// 解決 Odin GUIHelper 沒有單獨改變 Background 顏色的手段
        /// </summary>
        public static void PushBackgroundColor(Color color, bool blendAlpha = false)
        {
            ColorBackgroundStack.Push(GUI.color);
            if (blendAlpha)
                color.a *= GUI.backgroundColor.a;
            GUI.backgroundColor = color;
        }

    #endregion
    }
}