using System;
using UnityEngine;

namespace SiberOdinEditor.Tools
{
    /// <summary> 編輯器快捷鍵 </summary>
    public static class EditorHotKeys
    {
    #region ========== [Private Variables] ==========

        private static bool isDoOnce;

        private static Event current = Event.current;

        public static bool IsKeyUp   => current.type == EventType.KeyUp;
        public static bool IsKeyDown => current.type == EventType.KeyDown;

        public static bool IsKeyControl => current.keyCode is KeyCode.LeftControl or KeyCode.RightControl;
        public static bool IsKeyS       => current.keyCode == KeyCode.S;

        public static bool IsCtrlShift => current.modifiers == (EventModifiers.Control | EventModifiers.Shift);
        public static bool IsCtrlAlt   => current.modifiers == (EventModifiers.Control | EventModifiers.Alt);
        public static bool IsAltShift  => current.modifiers == (EventModifiers.Alt | EventModifiers.Shift);

        public static bool IsKeyESCDown => current.keyCode == KeyCode.Escape && IsKeyDown;

    #endregion

    #region ========== [Public Methods] ==========

        public static void CtrlS(Action action)
        {
            if (IsKeyDown && current.modifiers == EventModifiers.Control && IsKeyS)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }


            if (IsKeyUp && IsKeyS)
                isDoOnce = false;
        }

        public static void Init()
        {
            if (isDoOnce) isDoOnce = false;
        }

    #endregion
    }
}