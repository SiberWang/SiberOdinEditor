using UnityEngine;

namespace Examples.Scripts.Core
{
    /// <summary> 這邊示範了如何儲存跟讀取 TempData  </summary>
    [CreateAssetMenu(fileName = "Example_RealSOData_A", menuName = "BulletHellTools/Examples/RealSOData_A", order = 0)]
    public class Example_RealSOData_A : BaseContainer<ARealData>
    {
        protected override string LabelText => "角色資料";
    }
}