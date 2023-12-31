﻿using System;
using Examples.Scripts.Core;
using Sirenix.OdinInspector;

namespace Examples.Scripts.Datas
{
    /// <summary> 真正的純資料！ </summary>
    /// 頂多加個順序 PropertyOrder 或是 LabelText
    [Serializable]
    public class CharacterData : BaseData
    {
    #region ========== [Public Variables] ==========

        [PropertyOrder(0)]
        public string Name;
        [PropertyOrder(1)]
        public float MoveSpeed;
        [PropertyOrder(2)]
        public int   HP;


    #endregion

    #region ========== [Constructor] ==========

        public CharacterData() : base() { }

        public CharacterData(string name) : base()
        {
            Name = name;
        }

    #endregion

    #region ========== [Protected Methods] ==========

        protected override void Init()
        {
            Name      ??= "New Data";
            HP        =   100;
            MoveSpeed =   3;
        }

    #endregion

    #region ========== [Private Methods] ==========

        [ShowIf(nameof(IsNullID))]
        [Button("新 DataID")]
        private void NewDataID()
        {
            dataID = Guid.NewGuid().ToString();
        }

    #endregion
    }
}