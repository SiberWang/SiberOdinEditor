using SiberOdinEditor.Editor;

namespace Examples.Editor.Core
{
    public class EditorSaveSystem : BaseEditorSaveSystem<EditorSaveSystem>, IFileInfo
    {
        public override string FileName => "EditorSaveFile.json";
        public override string DataPath => "Assets/Resources";
    }
}