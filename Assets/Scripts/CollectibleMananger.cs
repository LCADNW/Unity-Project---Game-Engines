using UnityEngine;
using static Collectible;
using System.Collections.Generic;
public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance // ask about get/set
    {
        get;
        private set;
    }
    [Header("Collectible Counts")] //different types of resources
    [SerializeField] int trophiesTotal;
    [SerializeField] int gemsTotal;

    private Dictionary<CollectibleType, int> collectibles = new Dictionary<CollectibleType, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Add(CollectibleType type, int amount)
    {
        if (!collectibles.ContainsKey(type))
            collectibles[type] = 0;
        collectibles[type] += amount;
        Debug.Log($"{type}: {collectibles[type]}");
    }
    public int GetAmount(CollectibleType type)
    {
        if (collectibles.ContainsKey(type))
            return collectibles[type];
        return 0;
    }


    private void OnEnable()
    {
        // Subscribe to the collectible event
        CollectibleEventSystem.OnCollectibleCollected +=
       HandleCollectibleCollected;
    }
    private void OnDisable()
    {
        // Unsubscribe from the event
        CollectibleEventSystem.OnCollectibleCollected -= HandleCollectibleCollected;

    }

    private void HandleCollectibleCollected(CollectibleType
type, int amount)
    {
        if (!collectibles.ContainsKey(type))
            collectibles[type] = 0;
        collectibles[type] += amount;
        Debug.Log($"{type}: {collectibles[type]}");

        // Notify UI AFTER updating totals
        CollectibleEventSystem.RaiseCollectiblesUpdated();
    }

}



