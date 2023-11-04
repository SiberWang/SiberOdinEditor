namespace Examples.Editor.Names
{
    public static class EditorWindowDescription
    {
        public static string StringEmpty =
            $"<color=#{StrColor.Blue}>[Note]</color> Name is empty. Please name something.";

        public static string DataCanUse = $"<color=#{StrColor.Green}>[OK]</color> The Data name is OK!";
        public static string DataIsExist = $"<color=#{StrColor.Red}>[Error]</color> The Data name is exist";
        public static string SomeError   = $"<color=#{StrColor.Red}>[Error]</color> Cannot Delete.  Something wrong!";
    }

    // https://www.ifreesite.com/color/ 色票
    public static class StrColor
    {
        public static string Red    = "AD5A5A";
        public static string Green  = "00CACA";
        public static string Blue   = "66B3FF";
        public static string Yellow = "FFDC35";
    }
}