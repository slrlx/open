using UnityEngine;

public class HintCoinResetter : MonoBehaviour
{
    public HintCoinButton[] allHintButtons;  // UI 버튼 기반 힌트코인들
    public HintCoin[] allHintCoins;          // 씬에 존재하는 힌트코인 오브젝트들

    void Start()
    {
        // UI 버튼들 초기화
        foreach (HintCoinButton button in allHintButtons)
        {
            if (button == null) continue;

            PlayerPrefs.DeleteKey(button.coinID);

            if (button.buttonToDisable != null)
                button.buttonToDisable.gameObject.SetActive(true);
        }

        // 씬에 있는 HintCoin 오브젝트 초기화
        foreach (HintCoin coin in allHintCoins)
        {
            if (coin == null) continue;

            PlayerPrefs.DeleteKey(coin.coinID);
            coin.gameObject.SetActive(true);
        }

        PlayerPrefs.Save();
        Debug.Log("모든 힌트 코인 상태 초기화됨!");
    }
}