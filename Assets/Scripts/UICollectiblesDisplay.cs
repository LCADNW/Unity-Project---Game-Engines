using UnityEngine;
using TMPro;
using static Collectible;


public class UICollectiblesDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text trophiesText;
    [SerializeField] private TMP_Text gemsText;
    private void OnEnable()
    { CollectibleEventSystem.OnCollectiblesUpdated += UpdateUI; }
    private void OnDisable()
    { CollectibleEventSystem.OnCollectiblesUpdated -= UpdateUI; }

    private void Start()
    { UpdateUI(); } // Initial refresh
                 
 private void UpdateUI()
    {
        if (CollectibleManager.Instance == null)
            return;
        int trophies =
       CollectibleManager.Instance.GetAmount(CollectibleType.Trophy)
       ;
        int gems =
       CollectibleManager.Instance.GetAmount(CollectibleType.Gem);
        trophiesText.text = $"Trophies Collected: {trophies}/3";
        gemsText.text = $"Gems: {gems}";
    }

}
