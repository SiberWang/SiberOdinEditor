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

        private static bool IsKeyUp   => current.type == EventType.KeyUp;
        private static bool IsKeyDown => current.type == EventType.KeyDown;

        private static bool IsKeyControl => current.keyCode is KeyCode.LeftControl or KeyCode.RightControl;
        private static bool IsKeyS       => current.keyCode == KeyCode.S;

        private static bool IsCtrlShift => current.modifiers == (EventModifiers.Control | EventModifiers.Shift);
        private static bool IsCtrlAlt   => current.modifiers == (EventModifiers.Control | EventModifiers.Alt);
        private static bool IsAltShift  => current.modifiers == (EventModifiers.Alt | EventModifiers.Shift);

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
            isDoOnce = false;
        }

    #endregion
    }
}