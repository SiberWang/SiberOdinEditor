using Examples.Scripts.Core;
using UnityEngine;

namespace Examples.Scripts.Datas
{
    /// <summary> 這邊示範了如何儲存跟讀取 TempData  </summary>
    [CreateAssetMenu(fileName = "CharacterContainer", menuName = "Examples/CharacterContainer")]
    public class CharacterContainer : BaseContainer<CharacterData>
    {
        protected override string LabelText => "角色資料";
    }
}