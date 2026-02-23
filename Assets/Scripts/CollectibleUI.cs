using UnityEngine;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = CollectibleManager.Instance.totalCollected.ToString();
    }
}
