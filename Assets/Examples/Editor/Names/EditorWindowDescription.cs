namespace Examples.Editor.Names
{
    public static class EditorWindowDescription
    {
        public static string SelectedNull =
            "<color=Red>Note :</color>Current selected is null. Please select an object.";

        public static string SettingIsNull = "GameEditorSetting 不存在，因此無法使用";

        public static string StringEmpty =
            $"<color=#{StrColor.Blue}>[Note]</color> Name is empty. Please name something.";

        public static string DataCanUse = $"<color=#{StrColor.Green}>[OK]</color> The Data name is OK!";
        public static string DataIsExist = $"<color=#{StrColor.Red}>[Error]</color> The Data name is exist";
        public static string SomeError   = $"<color=#{StrColor.Red}>[Error]</color> Cannot Delete.  Something wrong!";
        public static string CopyData    = $"<color=#{StrColor.Green}>[Success!]</color> Copy Data : ";
        public static string NewData     = $"<color=#{StrColor.Green}>[Success!]</color> Create New Data : ";
        public static string Delete      = $"<color=#{StrColor.Green}>[Success!]</color> Delete!";
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