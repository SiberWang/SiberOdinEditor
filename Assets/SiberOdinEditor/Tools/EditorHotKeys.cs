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
        public static bool IsKeyDelete  => current.keyCode == KeyCode.Delete;
        public static bool IsKeyY       => current.keyCode == KeyCode.Y;
        public static bool IsKeyN       => current.keyCode == KeyCode.N;

        // 官方說大 Enter 就是 Return
        public static bool IsKeyEnter => current.keyCode == KeyCode.Return || current.keyCode == KeyCode.KeypadEnter;
        public static bool IsKeyESC   => current.keyCode == KeyCode.Escape;

        public static bool IsCtrlShift => current.modifiers == (EventModifiers.Control | EventModifiers.Shift);
        public static bool IsCtrlAlt   => current.modifiers == (EventModifiers.Control | EventModifiers.Alt);
        public static bool IsAltShift  => current.modifiers == (EventModifiers.Alt | EventModifiers.Shift);

        public static bool IsKeyESCDown => IsKeyESC && IsKeyDown;

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

        public static void Delete(Action action)
        {
            if (IsKeyDown && IsKeyDelete)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }

            if (IsKeyUp && IsKeyDelete)
                isDoOnce = false;
        }

        public static void Init()
        {
            if (isDoOnce) isDoOnce = false;
        }

        public static void Y(Action action)
        {
            if (IsKeyDown && IsKeyY)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }

            if (IsKeyUp && IsKeyY)
                isDoOnce = false;
        }

        public static void N(Action action)
        {
            if (IsKeyDown && IsKeyN)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }

            if (IsKeyUp && IsKeyN)
                isDoOnce = false;
        }

        public static void Enter(Action action)
        {
            if (IsKeyDown && IsKeyEnter)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }

            if (IsKeyUp && IsKeyEnter)
                isDoOnce = false;
        }

        public static void ESC(Action action)
        {
            if (IsKeyDown && IsKeyESC)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }

            if (IsKeyUp && IsKeyESC)
                isDoOnce = false;
        }

    #endregion
    }
}