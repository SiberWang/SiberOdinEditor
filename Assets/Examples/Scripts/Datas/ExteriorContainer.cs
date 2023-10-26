using Examples.Scripts.Core;
using UnityEngine;

namespace Examples.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ExteriorContainer", menuName = "Examples/ExteriorContainer")]
    public class ExteriorContainer : BaseContainer<ExteriorData>
    {
        protected override string LabelText => "其他資料";
    }
}