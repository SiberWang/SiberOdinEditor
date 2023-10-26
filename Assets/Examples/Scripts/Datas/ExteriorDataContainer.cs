using Examples.Scripts.Core;
using UnityEngine;

namespace Examples.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ExteriorDataContainer", menuName = "Examples/ExteriorDataContainer")]
    public class ExteriorDataContainer : BaseContainer<ExteriorData>
    {
        protected override string LabelText => "外觀資料";
    }
}