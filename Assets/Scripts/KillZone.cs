using UnityEngine;
using UnityEngine.SceneManagement;
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ReturnToMenu();
        }
    }
}
