using UnityEngine;

public class MovingCar : MonoBehaviour
{
    [Header("Objects")]
    public GameObject pointA;
    public GameObject pointB;
    public GameObject platform;

    [Header("Car Values")]
    [SerializeField]
    private float carSpeed = 2f;
    [SerializeField]
    private float carDelay = 1f;

    public void Update()
    {
        
    }
}
