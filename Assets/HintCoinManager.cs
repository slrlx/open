using UnityEngine;

public class HintCoinManager : MonoBehaviour
{
    public static HintCoinManager Instance;

    public int totalCoins = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectCoin()
    {
        totalCoins++;
        Debug.Log("힌트 코인을 찾았습니다! 총 코인 수: " + totalCoins);
        // UI 업데이트, 사운드 재생 등 추가
    }
}