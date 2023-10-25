using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Examples.Scripts.Core
{
    public abstract class BaseContainer<T> : ScriptableObject where T : IBaseData
    {
    #region ========== [Public Variables] ==========

        public List<T> Datas => datas;

        protected virtual string LabelText => $"{typeof(T).Name}s";

    #endregion

    #region ========== [Private Variables] ==========

        [ListDrawerSettings(OnTitleBarGUI = nameof(ListButtons), HideAddButton = true, HideRemoveButton = true)]
        [LabelText("@LabelText")]
        [SerializeField]
        private List<T> datas = new List<T>();

    #endregion

    #region ========== [Public Methods] ==========

        public void Add(T data)
        {
            if (!datas.Contains(data)) return;
            datas.Add(data);
        }

        public void Remove(T data)
        {
            if (!datas.Contains(data)) return;
            datas.Remove(data);
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> Odin List Buttons </summary>
        private void ListButtons()
        {
        #if UNITY_EDITOR
            if (SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh))
            {
                datas = datas.OrderByDescending(s => s.DataID).ToList();
                EditorUtility.SetDirty(this);
            }
        #endif
        }

    #endregion
    }
}