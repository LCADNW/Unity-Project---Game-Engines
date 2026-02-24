using UnityEngine;
using UnityEngine.Audio;
using static Collectible;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance
    {
        get;
        private set;
    }
    [Header("Pickup Sounds")]
    [SerializeField]
    private AudioClip
   trophyPickupSound;
    // [SerializeField]
    //  private AudioClip
    //    gemPickupSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {

        CollectibleEventSystem.OnCollectibleCollected
        += PlayPickupSound;
    }
    private void OnDisable()
    {

        CollectibleEventSystem.OnCollectibleCollected -= PlayPickupSound;
    }
    private void PlayPickupSound(CollectibleType
type, int amount)
    {
        switch (type)
        {
            case CollectibleType.Trophy:

                audioSource.PlayOneShot(trophyPickupSound);
                break;
            case CollectibleType.Gem:

                audioSource.PlayOneShot(trophyPickupSound);
                break;


        }
    }
}
