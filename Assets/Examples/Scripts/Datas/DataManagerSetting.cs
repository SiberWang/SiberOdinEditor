using System;
using Examples.Scripts.Misc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Examples.Scripts.Datas
{
    /// <summary> 用來儲存 DataManagerWindow 的設定 </summary>
    [CreateAssetMenu(fileName = "DataManagerSetting", menuName = "Examples/DataManagerSetting", order = 0)]
    public class DataManagerSetting : SerializedScriptableObject
    {
        [BoxGroup] [HideLabel]
        public Setting Setting = new();
    }

    [Serializable]
    public class Setting
    {
    #region ========== [Public Variables] ==========

        [InlineButton(nameof(DoSetWindowSize), "Reset Size")]
        [PropertyOrder(0)]
        public Vector2 WindowSize;

        [PropertyOrder(1)]
        public float IconSize;

        [PropertyOrder(2)]
        public float MenuWidth;

        [PropertyOrder(3)]
        [SerializeField]
        [PropertyTooltip("Odin Editor Action: " + nameof(EditorStringActions.EnableFlexibleSpace))]
        [OnValueChanged("DoStaticUpdate")]
        private bool enableFlexibleSpace;

        [PropertyOrder(4)]
        [SerializeField]
        [PropertyTooltip("Odin Editor Action: " + nameof(EditorStringActions.Enable_SubData_EditorButtons))]
        [OnValueChanged("DoStaticUpdate")]
        private bool enable_SubData_EditorButtons;

        [PropertyOrder(5)]
        [SerializeField]
        [PropertyTooltip("Odin Editor Action: " + nameof(EditorStringActions.Enable_MainData_EditorButtons))]
        [OnValueChanged("DoStaticUpdate")]
        private bool enable_SOData_EditorButtons;

        public static bool EnableFlexibleSpace;
        public static bool Enable_SubData_EditorButtons;
        public static bool Enable_SOData_EditorButtons;

        [OnInspectorInit]
        private void DoStaticUpdate()
        {
            EnableFlexibleSpace          = enableFlexibleSpace;
            Enable_SubData_EditorButtons = enable_SubData_EditorButtons;
            Enable_SOData_EditorButtons  = enable_SOData_EditorButtons;
        }

        public Setting() => Init();

        [PropertyOrder(100)]
        [Button("Default Setting")]
        private void Init()
        {
            WindowSize                   = new Vector2(1250, 700);
            IconSize                     = 25f;
            MenuWidth                    = 220;
            enableFlexibleSpace          = true;
            enable_SubData_EditorButtons = true;
            enable_SOData_EditorButtons  = true;
        }

        public void DoSetWindowSize()
        {
            // DataManagerWindow.SetWindowSize(WindowSize);
        }

    #endregion
    }
}