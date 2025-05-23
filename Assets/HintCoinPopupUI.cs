using UnityEngine;
using System.Collections;

public class HintCoinPopupUI : MonoBehaviour
{
    public GameObject popupUI; // 보여줄 UI 오브젝트
    public float showDuration = 0.5f;

    private bool isShowing = false;

    public void ShowPopup()
    {
        if (popupUI == null || isShowing) return;

        isShowing = true;
        popupUI.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(showDuration);
        popupUI.SetActive(false);
        isShowing = false;
    }

    public void HideNow()
    {
        StopAllCoroutines();
        popupUI.SetActive(false);
        isShowing = false;
    }
}
