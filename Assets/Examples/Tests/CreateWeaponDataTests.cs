using Examples.Scripts.Datas;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

public class CreateWeaponDataTests
{
#region ========== [Main] ==========

    [Test(Description = "成功建立擁有DataID的武器資料")]
    public void Create_Default_WeaponData_And_GetDataID()
    {
        var weaponData = new WeaponData();
        Assert.IsNotNull(weaponData, "weaponData == null");
    }

    [Test(Description = "成功用指定名稱建立武器資料")]
    public void Create_WeaponData_By_DataName()
    {
        var newDataName = "123";
        var weaponData  = new WeaponData(newDataName);
        Assert.AreEqual(weaponData.Name, newDataName, "Name is not equal");
    }

    [Test(Description = "成功建立武器資料 , 並獲得預設參數")]
    public void Create_WeaponData_And_Init_Values()
    {
        var weaponData = new WeaponData();
        Assert.AreEqual(weaponData.Name, "New Data", "Name is not equal");
    }

#endregion
}