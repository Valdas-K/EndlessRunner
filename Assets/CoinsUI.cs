using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    
    void Update()
    {
        coinsUI.text = "Total Coins: " + gm.totalCoins.ToString();

    }
}
