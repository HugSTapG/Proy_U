using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinText;
    public int GetCoins()
    {
        return coinCount;
    }
    public void AddCoins()
    {
        coinCount++;
        coinText.text = ":" + coinCount.ToString();
    }
    public void RemoveCoin()
    {
        if(coinCount == 0)
        {
            coinText.text = ":" + coinCount.ToString();
        }
        else
        {
            coinCount--;
            coinText.text = ":" + coinCount.ToString();
        }
    }
}

