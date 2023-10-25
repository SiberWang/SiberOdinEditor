using System;
using Examples.Scripts.Core;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Examples.Scripts.OdinWindows.Utils
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
            string fileName = $"{typeof(A).Name}_{newData.DataName}";
            AssetDatabase.CreateAsset(newData, $"{newData.FilePath}/{fileName}.asset");
            AssetDatabase.SaveAssets();
        }
    }

    public interface ICreateDataAction
    {
        void Create();
    }
}