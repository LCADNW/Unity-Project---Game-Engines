using UnityEngine;
using static Collectible;
public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance // ask about get/set
    {
        get;
        private set;
    }
    [Header("Collectible Counts")] //different types of resources
    [SerializeField] int coinsTotal;
    [SerializeField] int gemsTotal;
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
        switch (type)
        {
            case CollectibleType.Coin:
                coinsTotal += amount;
                break;
            case CollectibleType.Gem:
                gemsTotal += amount;
                break;
        }
        Debug.Log($"Coins: {coinsTotal}, Gems: {gemsTotal}");
    }
}



