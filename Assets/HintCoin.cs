using UnityEngine;
using UnityEngine.UI;

public class HintCoin : MonoBehaviour
{
    public string coinID;
    public AudioClip collectSFX;
    public Button buttonToDisable;
    private AudioSource audioSource;

    private bool isCollected = false;

    void Start()
    {
        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            isCollected = true;
            if (buttonToDisable != null)
                buttonToDisable.gameObject.SetActive(false);
        }
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    public void CollectCoinFromUIButton()
    {
        if (isCollected) 
        {
            Debug.Log("이미 수집된 코인입니다.");
            return;
        }

        isCollected = true;

        if (collectSFX != null)
        {
            Debug.Log("HintCoin 사운드 재생: " + collectSFX.name);
            audioSource.PlayOneShot(collectSFX);
        }
        else
        {
            Debug.LogWarning("HintCoin collectSFX가 할당되지 않았습니다!");
        }

        HintCoinManager.Instance.CollectCoin();

        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();

        if (buttonToDisable != null)
            buttonToDisable.gameObject.SetActive(false);
    }
}