using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HintCoinButton : MonoBehaviour
{
    public string coinID = "HintCoin_1";        // 유니크한 키
    public AudioClip collectSFX;                // 효과음
    public Button buttonToDisable;              // 클릭 후 꺼질 버튼 (자기 자신)
    public HintCoinPopupUI popupUIController;

    private AudioSource audioSource;

    void Start()
    {
        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            if (buttonToDisable != null)
                buttonToDisable.gameObject.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    public void CollectCoinFromUIButton()
    {
        if (PlayerPrefs.GetInt(coinID, 0) == 1) return; // 중복 수집 방지

        HintCoinManager.Instance.CollectCoin();

        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();

        if (popupUIController != null)
        {
            popupUIController.ShowPopup();
        }

        StartCoroutine(PlaySoundThenDisable());
    }

    public void ResetHintCoin()
    {
        PlayerPrefs.DeleteKey(coinID);
        PlayerPrefs.Save();

        if (buttonToDisable != null)
            buttonToDisable.gameObject.SetActive(true);
    }

    private IEnumerator PlaySoundThenDisable()
    {
        if (collectSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSFX);
            yield return new WaitForSeconds(collectSFX.length);
        }

        if (buttonToDisable != null)
            buttonToDisable.gameObject.SetActive(false);
    }
}