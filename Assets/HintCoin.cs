using UnityEngine;

public class HintCoin : MonoBehaviour
{
    public bool isCollected = false;

    // 토글 버튼 오브젝트 (에디터에서 할당)
    public GameObject toggleButton;

    // 유니크한 ID를 부여 (예: HintCoin_1, HintCoin_2 등)
    public string coinID;

    void Start()
    {
        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            isCollected = true;
            gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (isCollected) return;

        isCollected = true;
        HintCoinManager.Instance.CollectCoin();
        gameObject.SetActive(false);

        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();

        if (toggleButton != null)
        {
            toggleButton.SetActive(false);  // 토글 버튼 비활성화
        }

        // 추가로 이펙트, 사운드 호출 가능
    }
}