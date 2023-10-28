using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Examples.Editor.Datas
{
    /// <summary> 用來儲存 DataManager Window 的設定 </summary>
    [CreateAssetMenu(fileName = "WindowSettingData", menuName = "Examples/WindowSettingData", order = 0)]
    public class WindowSettingData : SerializedScriptableObject
    {
        public Setting             Setting = new();
        public CustomOdinMenuStyle CustomOdinMenuStyle;
    }

    [Serializable]
    public class Setting
    {
    #region ========== [Public Variables] ==========

        [PropertyOrder(0)]
        public Vector2 WindowSize;

        [PropertyOrder(1)]
        public float MenuWidth;

        public Setting() => Init();

        public Setting(Setting setting) => SetData(setting);

        public void SetData(Setting setting)
        {
            WindowSize = setting.WindowSize;
            MenuWidth  = setting.MenuWidth;
        }

        public void Init()
        {
            WindowSize = new Vector2(680, 700);
            MenuWidth  = 220;
        }

    #endregion
    }
}