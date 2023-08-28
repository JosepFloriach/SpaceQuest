using jovetools.gameserialization;

public class CurrencyController : SingletonMonoBehaviour<CurrencyController>, ISerializable
{
    public int TotalCollectedGems
    {
        get;
        private set;
    }

    public int CurrentGems
    {
        get;
        private set;
    }

    public int GemsSpent
    {
        get;
        private set;
    }

    public void OnDestroy()
    {
        PersistanceManager<GameData>.Instance.DeregisterSerializableObject(this);
    }

    public void AddCollectedGems(int amount)
    {
        TotalCollectedGems += amount;
        CurrentGems += amount;
    }

    public void SpendGems(int amount)
    {
        CurrentGems -= amount;
        GemsSpent += amount;
    }

    public void ClearData(IGameData data)
    {
        CurrentGems = 0;
        GemsSpent = 0;
        TotalCollectedGems = 0;
    }

    public void CreateData(ref IGameData data)
    {
        var gameData = (GameData)data;
        gameData.Currency.CurrentAmountAvailable = 0;
        gameData.Currency.AmountSpent = 0;
        gameData.Currency.TotalAmountCollected = 0;
    }

    public void LoadData(IGameData data)
    {
        var gameData = (GameData)data;
        CurrentGems = gameData.Currency.CurrentAmountAvailable;
        GemsSpent = gameData.Currency.AmountSpent;
        TotalCollectedGems = gameData.Currency.TotalAmountCollected;
    }

    public void SaveData(ref IGameData data)
    {
        var gameData = (GameData)data;
        gameData.Currency.CurrentAmountAvailable = CurrentGems;
        gameData.Currency.AmountSpent = GemsSpent;
        gameData.Currency.TotalAmountCollected = TotalCollectedGems;
    }
}
