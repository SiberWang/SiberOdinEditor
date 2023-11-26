using System;

namespace SiberOdinEditor.InGame.Attributes
{
    public class ColorIfAttribute : Attribute
    {
        public string Color;
        public string Condition;

        public ColorIfAttribute(string color, string condition)
        {
            Color     = color;
            Condition = condition;
        }
    }
}