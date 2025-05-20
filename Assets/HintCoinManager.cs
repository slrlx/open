using UnityEngine;
using System.Collections.Generic;

public class HintCoinManager : MonoBehaviour
{
    private List<GameObject> allCoins = new List<GameObject>();

    public static HintCoinManager Instance;

    public int totalCoins = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        totalCoins = 0;
        PlayerPrefs.DeleteKey("TotalHintCoins");
        PlayerPrefs.Save();
    }

    void Start()
    {
        // totalCoins = PlayerPrefs.GetInt("TotalHintCoins", 0);
    }

    public void RegisterCoin(GameObject coin)
    {
        allCoins.Add(coin);
    }

    public void UnregisterCoin(GameObject coin)
    {
        if (allCoins.Contains(coin))
            allCoins.Remove(coin);
    }

    public void HideAllCoins()
    {
        foreach (GameObject coin in allCoins)
        {
            if (coin != null)
                coin.SetActive(false);
        }
    }

    public void CollectCoin()
    {
        totalCoins++;
        PlayerPrefs.SetInt("TotalHintCoins", totalCoins);
        PlayerPrefs.Save();

        Debug.Log("힌트 코인을 찾았습니다! 총 코인 수: " + totalCoins);
    }
}