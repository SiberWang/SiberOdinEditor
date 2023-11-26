using SiberOdinEditor.InGame.Attributes;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace SiberOdinEditor.AttributeDrawers
{
    public class ColorIfAttributeDrawer : OdinAttributeDrawer<ColorIfAttribute>
    {
        private ValueResolver<Color> colorResolver;
        private ValueResolver<bool>  conditionResolver;

        protected override void Initialize()
        {
            colorResolver     = ValueResolver.Get<Color>(Property, Attribute.Color);
            conditionResolver = ValueResolver.Get<bool>(Property, Attribute.Condition);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            bool condition = conditionResolver.GetValue();
       
            if (colorResolver.HasError) condition = false;
            if (condition) GUIHelper.PushColor(colorResolver.GetValue());
            CallNextDrawer(label);
            if (condition) GUIHelper.PopColor();
        }
    }
}