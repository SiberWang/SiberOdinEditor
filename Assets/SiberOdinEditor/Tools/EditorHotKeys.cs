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
        public static bool IsKeyQ       => current.keyCode == KeyCode.Q;
        public static bool IsKeyW       => current.keyCode == KeyCode.W;
        public static bool IsKeyE       => current.keyCode == KeyCode.E;
        public static bool IsKeyA       => current.keyCode == KeyCode.A;
        public static bool IsKeyD       => current.keyCode == KeyCode.D;

        // 官方說大 Enter 就是 Return
        public static bool IsKeyEnter => current.keyCode is KeyCode.Return or KeyCode.KeypadEnter;
        public static bool IsKeyESC   => current.keyCode == KeyCode.Escape;

        public static bool IsCtrlShift => current.modifiers == (EventModifiers.Control | EventModifiers.Shift);
        public static bool IsCtrlAlt   => current.modifiers == (EventModifiers.Control | EventModifiers.Alt);
        public static bool IsAltShift  => current.modifiers == (EventModifiers.Alt | EventModifiers.Shift);

        public static bool IsCtrl    => current.modifiers == (EventModifiers.Control);
        public static bool IsKeyESCDown => IsKeyESC && IsKeyDown;

    #endregion

    #region ========== [Public Methods] ==========

        public static void Init()
        {
            if (isDoOnce)
                isDoOnce = false;
        }

        public static void CtrlS(Action action) =>
            GetKeyDown(action, current.modifiers == EventModifiers.Control && IsKeyS, IsKeyS);

        public static void Delete(Action action) => GetKeyDown(action, IsKeyDelete);
        public static void Y(Action      action) => GetKeyDown(action, IsKeyY);
        public static void N(Action      action) => GetKeyDown(action, IsKeyN);
        public static void Enter(Action  action) => GetKeyDown(action, IsKeyEnter);

        public static void ESC(Action action) => GetKeyDown(action, IsKeyESC);
        public static void Q(Action   action) => GetKeyDown(action, IsKeyQ);
        public static void W(Action   action) => GetKeyDown(action, IsKeyW);
        public static void E(Action   action) => GetKeyDown(action, IsKeyE);
        public static void A(Action   action) => GetKeyDown(action, IsKeyA);
        public static void D(Action   action) => GetKeyDown(action, IsKeyD);
        public static void S(Action   action) => GetKeyDown(action, IsKeyS);

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 按鍵事件 </summary>
        /// <param name="action"> 執行事件 </param>
        /// <param name="isKey"> Condition: KeyUp and KeyDown </param>
        private static void GetKeyDown(Action action, bool isKey)
        {
            if (IsKeyDown && isKey)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }

            if (IsKeyUp && isKey)
                isDoOnce = false;
        }

        /// <summary> 按鍵事件 </summary>
        /// <param name="action"> 執行事件 </param>
        /// <param name="isKeyDown"> Condition: keyDown </param>
        /// <param name="isKeyUp"> Condition: keyUp </param>
        private static void GetKeyDown(Action action, bool isKeyDown, bool isKeyUp)
        {
            if (IsKeyDown && isKeyDown)
            {
                if (!isDoOnce)
                {
                    action?.Invoke();
                    isDoOnce = true;
                }
            }

            if (IsKeyUp && isKeyUp)
                isDoOnce = false;
        }

    #endregion
    }
}