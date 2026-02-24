using UnityEngine;
using System;

public static class CollectibleEventSystem
{
    // Define a static event for when a collectible is collected
    public static event Action<Collectible.CollectibleType, int> OnCollectibleCollected;
    // Method to invoke the event
    // Fired AFTER totals are updated
    public static event Action
   OnCollectiblesUpdated;
    public static void RaiseCollectibleCollected(Collectible.CollectibleType type, int amount)
    {
        OnCollectibleCollected?.Invoke(type, amount);
    }
    public static void
RaiseCollectiblesUpdated()
    {
        OnCollectiblesUpdated?.Invoke();
    }
}

    

