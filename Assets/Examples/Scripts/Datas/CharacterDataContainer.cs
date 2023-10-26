using Examples.Scripts.Core;
using UnityEngine;

namespace Examples.Scripts.Datas
{
    /// <summary> 這邊示範了如何儲存跟讀取 TempData  </summary>
    [CreateAssetMenu(fileName = "CharacterDataContainer", menuName = "Examples/CharacterDataContainer")]
    public class CharacterDataContainer : BaseContainer<CharacterData>
    {
        protected override string LabelText => "角色資料";
    }
}