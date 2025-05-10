using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public RoomManager roomManager;

    public void FadeToRoom(int roomIndex)
    {
        StartCoroutine(FadeTransition(roomIndex));
    }

    private IEnumerator FadeTransition(int index)
    {
        yield return StartCoroutine(Fade(1)); // 검게
        roomManager.MoveToRoom(index);        // 방 이동
        yield return StartCoroutine(Fade(0)); // 다시 투명하게
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            fadeImage.color = new Color(0, 0, 0, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}