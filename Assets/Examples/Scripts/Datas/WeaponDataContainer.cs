using Examples.Scripts.Core;
using UnityEngine;

namespace Examples.Scripts.Datas
{
    [CreateAssetMenu(fileName = "WeaponDataContainer", menuName = "Examples/WeaponDataContainer")]
    public class WeaponDataContainer : BaseContainer<WeaponData>
    {
        protected override string LabelText => "武器資料";
    }
}