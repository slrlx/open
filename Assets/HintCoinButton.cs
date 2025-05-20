using UnityEngine;
using UnityEngine.UI;

public class HintCoinButton : MonoBehaviour
{
    public string coinID = "HintCoin_1";        // 유니크한 키
    public AudioClip collectSFX;                // 효과음
    public Button buttonToDisable;              // 클릭 후 꺼질 버튼 (자기 자신)

    private AudioSource audioSource;

    void Start()
    {
        // 이미 수집했는지 확인
        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            if (buttonToDisable != null)
                buttonToDisable.gameObject.SetActive(false); // 이미 수집된 버튼은 숨김
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void CollectCoinFromUIButton()
    {
        if (PlayerPrefs.GetInt(coinID, 0) == 1) return; // 중복 수집 방지

        // 코인 증가
        HintCoinManager.Instance.CollectCoin();

        // 저장
        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();

        // 사운드 재생
        if (collectSFX != null)
            audioSource.PlayOneShot(collectSFX);

        // 버튼 비활성화
        if (buttonToDisable != null)
            buttonToDisable.gameObject.SetActive(false);
    }

    // ✅ 이걸 추가하세요
    public void ResetHintCoin()
    {
        PlayerPrefs.DeleteKey(coinID);
        PlayerPrefs.Save();

        if (buttonToDisable != null)
            buttonToDisable.gameObject.SetActive(true);
    }
}