using UnityEngine;
using TMPro;
using static Collectible;


public class UICollectiblesDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text trophiesText;
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
        trophiesText.text = $"Trophies Collected: {trophies}/3";
    }

}
