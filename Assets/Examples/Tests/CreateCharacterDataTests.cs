using Examples.Scripts.Datas;
using NUnit.Framework;

public class CreateCharacterDataTests
{
#region ========== [Main] ==========

    [Test(Description = "成功建立擁有DataID的角色資料")]
    public void Create_Default_CharacterData_And_GetDataID()
    {
        var characterData = new CharacterData();
        Assert.IsNotNull(characterData.DataID, "characterData.DataID == null");
    }

    [Test(Description = "成功用指定名稱建立角色資料")]
    public void Create_CharacterData_By_DataName()
    {
        var newDataName   = "ABCBBCCAA";
        var characterData = new CharacterData(newDataName);
        Assert.AreEqual(characterData.Name, newDataName, "Name is not equal");
    }

    [Test(Description = "成功建立角色資料 , 並獲得預設參數")]
    public void Create_CharacterData_And_Init_Values()
    {
        var characterData = new CharacterData();
        Assert.AreEqual(characterData.Name, "New Data", "Name is not equal");
        Assert.AreEqual(characterData.MoveSpeed, 3, "MoveSpeed is not equal");
        Assert.AreEqual(characterData.HP, 100, "HP is not equal");
    }

#endregion

#region ========== [Childs] ==========

    [Test(Description = "透過角色資料資料ID , 來建立外觀資料並紀錄ID")]
    public void Create_ExteriorData_And_Reference_By_CharacterData_DataID()
    {
        var characterData = new CharacterData();
        var exteriorData  = new ExteriorData(characterData.DataID);
        Assert.IsNotNull(exteriorData.DataID, "exteriorData.DataID == null");
        Assert.AreEqual(exteriorData.DataID, characterData.DataID, "exteriorData.DataID is not equal");
    }

    [Test(Description = "成功建立外觀資料 , 並獲得預設參數")]
    public void Create_ExteriorData_And_Init_Values()
    {
        var exteriorData = new ExteriorData("123");
        Assert.AreEqual(exteriorData.bodySprite, null, "bodySprite is not equal");
        Assert.AreEqual(exteriorData.someValue, 5, "someValue is not equal");
        Assert.AreEqual(exteriorData.someContext, "預設內容", "someContext is not equal");
    }

#endregion
}