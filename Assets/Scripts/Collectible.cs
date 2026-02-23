using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1;
    public enum CollectibleType
    {
        Coin,
        Gem
    }
    [SerializeField] CollectibleType type;
    [SerializeField]
    int amount = 1;
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0,
       Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleManager.Instance.Add(type, amount);
            Destroy(gameObject);
        }

    }

}
