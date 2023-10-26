using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SiberOdinEditor.Core.Utils
{
    [Serializable]
    public class CreateNewData<A, B> : ICreateDataAction
    where A : ScriptableObject, IEditorData<B>
    where B : class, new()
    {
        public CreateNewData()
        {
            newData = ScriptableObject.CreateInstance<A>();
            newData.SetData(new B());
            newData.SetDataName("[New Data]");
        }

        [SerializeField]
        [InlineEditor(Expanded = true, ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        private A newData;

        public void Create()
        {
        #if UNITY_EDITOR
            string fileName = $"{typeof(A).Name}_{newData.DataName}";
            AssetDatabase.CreateAsset(newData, $"{newData.FilePath}/{fileName}.asset");
            AssetDatabase.SaveAssets();
        #endif
        }
    }

    public interface ICreateDataAction
    {
        void Create();
    }
}