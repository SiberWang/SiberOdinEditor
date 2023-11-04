using Examples.Editor.Windows;
using SiberOdinEditor.Core.Utils;
using SiberOdinEditor.Tools;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Examples.Editor.Datas
{
    public class EditorReferenceData_WindowSetting
    {
    #region ========== [Public Variables] ==========

        [FoldoutGroup("OdinStyle 設定")]
        [PropertyOrder(1)]
        [HideLabel] [ShowInInspector] [HideReferenceObjectPicker]
        public OdinMenuStyle OdinMenuStyle;

        [FoldoutGroup("編輯視窗設定")]
        [OnValueChanged(nameof(UpdateWindow), IncludeChildren = true)]
        [PropertyOrder(0)]
        [HideLabel]
        public Setting Setting;

    #endregion

    #region ========== [Private Variables] ==========

        private CustomOdinMenuStyle referenceStyle;
        private Setting             referenceSetting;

        private bool safeLock = true;

    #endregion

    #region ========== [Constructor] ==========

        public EditorReferenceData_WindowSetting
            (WindowSettingData WindowSettingData, OdinMenuStyle odinMenuStyle)
        {
            // 參考->真實資料
            referenceSetting = WindowSettingData.Setting;
            referenceStyle   = WindowSettingData.CustomOdinMenuStyle;

            // Editor 資料
            Setting       = new Setting(referenceSetting);
            OdinMenuStyle = odinMenuStyle;
        }

    #endregion

    #region ========== [Events] ==========

        [PropertyOrder(-100)]
        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            SirenixEditorGUI.BeginHorizontalToolbar(30);
            {
                if (safeLock && OdinStyleTools.CustomToolbarButton(SdfIconType.CaretRightFill))
                    safeLock = false;
                if (!safeLock && OdinStyleTools.CustomToolbarButton(SdfIconType.CaretLeft))
                    safeLock = true;

                if (!safeLock)
                {
                    GUI.backgroundColor = new Color(0.77f, 1f, 0.7f) * 1.5f;
                    if (OdinStyleTools.CustomToolbarButton("儲存設定", SdfIconType.Camera))
                    {
                        SaveCustomStyle();
                        SaveSetting();
                    }

                    GUI.backgroundColor = Color.white;

                    GUI.backgroundColor = new Color(0.62f, 1f, 0.94f) * 1.5f;
                    if (OdinStyleTools.CustomToolbarButton("抓視窗尺寸", SdfIconType.Window))
                    {
                        Setting.WindowSize = MenuItemSetter.GetWindowSize();
                    }
                    GUI.backgroundColor = Color.white;

                    GUI.backgroundColor = new Color(1f, 0.81f, 0.56f) * 1.5f;
                    if (OdinStyleTools.CustomToolbarButton("初始化視窗", SdfIconType.ArrowRepeat))
                    {
                        Setting = new Setting(referenceSetting);
                        UpdateWindow();
                    }

                    GUI.backgroundColor = Color.white;

                    GUI.backgroundColor = new Color(1f, 0.68f, 0.47f) * 1.5f;
                    if (OdinStyleTools.CustomToolbarButton("初始化OdinStyle", SdfIconType.ArrowRepeat))
                    {
                        var newOdinMenuStyle = referenceStyle.GetNewOdinMenuStyle();
                        OdinMenuStyle.CloneNewStyle(newOdinMenuStyle);
                    }

                    GUI.backgroundColor = Color.white;
                }
                else
                {
                    var style = OdinStyleTools.CenterLabel();
                    var text  = " 隱藏式功能列表";
                    GUILayout.Label(text, style);
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

    #endregion

    #region ========== [Private Methods] ==========

        private void SaveSetting()
        {
            referenceSetting.SetData(Setting);
        }

        private void SaveCustomStyle()
        {
            referenceStyle.SetData(OdinMenuStyle);
        }

        private void UpdateWindow()
        {
            MenuItemSetter.SetWindowSize(Setting.WindowSize);
            MenuItemSetter.SetMenuWidth(Setting.MenuWidth);
        }

    #endregion
    }
}