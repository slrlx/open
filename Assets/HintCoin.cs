using UnityEngine;

public class HintCoin : MonoBehaviour
{
    public string coinID;
    public AudioClip collectSFX;
    private AudioSource audioSource;

    private bool isCollected = false;

    void Start()
    {
        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            isCollected = true;
            gameObject.SetActive(false); // 이미 수집된 코인이면 꺼짐
        }
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    // ✅ UI 버튼에서 호출할 함수
    public void CollectFromUIButton()
    {
        if (isCollected) return;

        isCollected = true;

        if (collectSFX != null)
            audioSource.PlayOneShot(collectSFX);

        HintCoinManager.Instance.CollectCoin();

        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();

        HintCoinManager.Instance.UnregisterCoin(this.gameObject);

        // 소리 재생 시간 후 삭제
        Destroy(gameObject, collectSFX != null ? collectSFX.length : 0f);
    }
}