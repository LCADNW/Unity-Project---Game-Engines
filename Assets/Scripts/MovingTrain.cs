using System.Collections;
using TMPro;
using UnityEngine;

public class MovingTrain : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private GameObject pointA;
    [SerializeField]
    private GameObject pointB;
    [SerializeField]
    private GameObject platform;

    [Header("Car Values")]
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float delay = 1f;

    private Vector3 targetPosition;
    private void Start()
    {
        platform.transform.position = pointA.transform.position;
        targetPosition = pointB.transform.position;

        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
            while (true)
            {
                while ((targetPosition -
               platform.transform.position).sqrMagnitude > 0.01f)
                {
                    platform.transform.position = Vector3.MoveTowards(
                    platform.transform.position,
                    targetPosition,
                    speed * Time.deltaTime
                    );
                    yield return null;
                }

            targetPosition = targetPosition ==
     pointA.transform.position
      ? pointB.transform.position
      : pointA.transform.position;
            yield return new WaitForSeconds(delay);
        }
    }
}



