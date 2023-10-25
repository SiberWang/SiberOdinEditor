﻿using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Examples.Scripts.Core
{
    /// <summary> 這邊示範了如何儲存跟讀取 TempData  </summary>
    [CreateAssetMenu(fileName = "Example_TempData_SOData",
                     menuName = "BulletHellTools/Examples/Example_TempData_SOData", order = 0)]
    public class Example_TempData_SOData : SerializedScriptableObject
    {
        [ShowInInspector] [OdinSerialize]
        private SubData subData = new();

        public Example_TempData_SOData() => subData = new SubData();

        [HideLabel]
        [HideReferenceObjectPicker]
        private class SubData
        {
        #region ========== [Private Variables] ==========

            [PropertyOrder(0)]
            [OdinSerialize] [LabelText("主要資料")]
            private ExampleTempData mainData;

            [PropertyOrder(2)]
            [OdinSerialize]
            [ShowInInspector] [ReadOnly]
            private object tempSave;

            [PropertyOrder(1)]
            [ShowInInspector]
            private string display => tempSave != null ? "Has tempSave" : "--Null--";

        #endregion

        #region ========== [Constructor] ==========

            public SubData()
            {
                mainData = new ExampleTempData();
                tempSave = null;
            }

        #endregion

        #region ========== [Private Methods] ==========

            [PropertyOrder(11)]
            [ButtonGroup, Button]
            private void Load()
            {
                mainData = new ExampleTempData(tempSave);
            }

            [PropertyOrder(10)]
            [ButtonGroup, Button]
            private void Save()
            {
                tempSave = new ExampleTempData(mainData);
            }
            
            [PropertyOrder(12)]
            [ButtonGroup, Button]
            private void Clear()
            {
                tempSave = null;
            }

        #endregion

            [Serializable]
            private class ExampleTempData
            {
                public string Name;
                public int    Number;
                public Color  Color = Color.white;

                public ExampleTempData() { }

                public ExampleTempData(object tempSave)
                {
                    if (tempSave is not ExampleTempData mainData) return;
                    Name   = mainData.Name;
                    Number = mainData.Number;
                    Color  = mainData.Color;
                }
            }
        }
    }
}