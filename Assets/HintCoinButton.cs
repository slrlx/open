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

        HintCoinManager.Instance.CollectCoin();

        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();

        if (collectSFX != null)
        {
            Debug.Log("HintCoinButton 사운드 재생: " + collectSFX.name);
            audioSource.PlayOneShot(collectSFX);
        }
        else
        {
            Debug.LogWarning("HintCoinButton collectSFX가 할당되지 않았습니다!");
        }

        if (buttonToDisable != null)
            buttonToDisable.gameObject.SetActive(false);
    }

    public void ResetHintCoin()
    {
        PlayerPrefs.DeleteKey(coinID);
        PlayerPrefs.Save();

        if (buttonToDisable != null)
            buttonToDisable.gameObject.SetActive(true);
    }
}