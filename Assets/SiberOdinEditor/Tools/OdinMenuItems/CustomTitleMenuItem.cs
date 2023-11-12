using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEngine;

namespace SiberOdinEditor.Tools.OdinMenuItems
{
    /// <summary> 自製主項目的OdinMenuItem <br/>
    /// 順便Show 出子項目的數量
    /// </summary>
    public class CustomTitleMenuItem : OdinMenuItem
    {
        private readonly OdinMenuTree tree;

        public CustomTitleMenuItem(OdinMenuTree tree, string name, object value) : base(tree, name, value)
        {
            this.tree = tree;
        }

        protected override void OnDrawMenuItem(Rect rect, Rect labelRect)
        {
            // 這招是跟隨字串長度
            var calcSizeA = GUI.skin.label.CalcSize(new GUIContent(SmartName));
            labelRect.x += calcSizeA.x + 10;
            var odinMenuItem = tree.GetMenuItem(SmartName);
            if (odinMenuItem == null || odinMenuItem.ChildMenuItems == null) return;
            var count = odinMenuItem.ChildMenuItems.Count;
            GUI.Label(labelRect.AlignMiddle(25).AlignLeft(100), $"({count})");
        }
    }
}