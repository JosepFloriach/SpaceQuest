using jovetools.gameserialization;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GamePersistanceController: SingletonMonoBehaviour<GamePersistanceController>
{
    [SerializeField] private string fileName;

    private static bool init = false;
    
    protected override void Start()
    {
        base.Start();
        if (!init)
        {
            RegisterSerializables();
            init = true;
        }
    }

    private void OnApplicationQuit()
    {
        PersistanceManager<GameData>.Instance.SaveGame();
    }

    private void RegisterSerializables()
    {
        var jsonHandler = new JsonHandler<GameData>(Application.persistentDataPath, fileName);
        PersistanceManager<GameData>.Instance.DeregisterAllSerializables();
        PersistanceManager<GameData>.Instance.Init(jsonHandler);
        foreach (var obj in FindObjectsOfType<MonoBehaviour>().OfType<ISerializable>())
        {
            PersistanceManager<GameData>.Instance.RegisterSerializableObject(obj);
        }
        PersistanceManager<GameData>.Instance.LoadGame();
    }
}