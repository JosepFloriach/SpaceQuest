using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class BonusGemsController : MonoBehaviour
{
    [SerializeField] List<GameObject> gems;

    private Player player;
    private LevelProgressController levelProgressController;
    private LevelSetup levelSetup;

    // This dictionary holds if the gems that have been collected in this run. A run takes
    // since the player starts the current level until it's closed. Either because he/she
    // aborted the mission, or because he/she won the level.
    private Dictionary<GameObject, bool> collectedGems = new();

    // This dictionary holds the same objects than collectedGems. But instead of tracking
    // gems collected on this run, it's tracking the gems collected in a previous run
    // completed successfuly, and they were saved in the progress file.
    private Dictionary<GameObject, bool> previouslyCollectedGems = new();

    /// <summary>
    /// Returns the gems collected in this try.
    /// </summary>
    public List<bool> CollectedGems
    {
        get
        {
            return collectedGems.Values.ToList();
        }
    }

    /// <summary>
    /// Returns the number of gems that were collected for first time in this try. That means
    /// those never collected on a successfuly completed run.
    /// </summary>
    public int CollectedGemsForFirstTime
    {
        get;
        private set;
    }

    private void Awake()
    {
        levelProgressController = FindObjectOfType<LevelProgressController>();
        levelSetup = FindObjectOfType<LevelSetup>();
        player = FindObjectOfType<Player>();
        ReferenceValidator.NotNull(levelSetup, player, gems);
    }

    private void Start()
    {
        if (levelProgressController != null)
        {
            for (int idx = 0; idx < gems.Count; ++idx)
            {
                bool collected = levelProgressController.GetLevelCompletion(levelSetup.LevelIndex).GemsCollected[idx];
                previouslyCollectedGems.Add(gems[idx], collected);
                collectedGems.Add(gems[idx], false);
            }
        }
    }

    private void OnEnable()
    {
        BasePickup.OnPickup += OnPickup;
        player.PlayerKilled += OnPlayerKilled;
    }

    private void OnDisable()
    {
        BasePickup.OnPickup -= OnPickup;
        player.PlayerKilled -= OnPlayerKilled;
    }

    private void OnPickup(object sender, PickupEventArgs args)
    {
       GameObject pickedUpGem = args.pickUp.gameObject;
       if (collectedGems.ContainsKey(pickedUpGem))
       {
            if (previouslyCollectedGems.ContainsKey(pickedUpGem) &&
                !previouslyCollectedGems[pickedUpGem])
            {
                CollectedGemsForFirstTime++;
            }
            collectedGems[pickedUpGem] = true;
       }
    }

    public void OnPlayerKilled()
    {
        collectedGems.Clear();
        foreach(var gem in gems)
        {
            collectedGems.Add(gem, false);
        }
        CollectedGemsForFirstTime = 0;
    }
}
