using System;
using jovetools.gameserialization;
using NUnit.Framework;
using UnityEngine;

public class GameDataValidationTest
{
    [Test]
    public void HealthyData_Expect_NoError()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "Healthy.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), !Throws.TypeOf<Exception>());
    }

    [Test]
    public void NoSelectedShip_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "NoSelectedShip.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void NoUnlockedShip_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "NoUnlockedShip.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void UnlockedShipsWithoutEnoughCurrency_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "UnlockedShipWithoutEnoughCurrency.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void SelectedLockedShip_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "SelectedLockedShip.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void SelectedNonExistingShip_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "SelectedNonExistingShip.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void UnlockedNegativeShip_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "NegativeShipUnlocked.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void WrongTotalCollectedCurrency_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "WrongTotalCurrencyCollected.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void WrongFurthestLevelCompleted_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "WrongFurthestLevelCompleted.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void WrongCurrentCurrencyAvailable_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "WrongCurrencyAvailable.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void WrongAmountSpent_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "WrongAmountSpent.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void SkippedLevel_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "SkippedLevel.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }

    [Test]
    public void CollectedGemsInNonCompletedLevel_Expect_Error()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.dataPath + "/Tests/Integration/Runtime/GameDataValidation/", "CollectedGemsInNonCompletedLevel.json");
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        Assert.That(() => PersistanceManager<GameData>.Instance.LoadGame(), Throws.TypeOf<Exception>());
    }
}
