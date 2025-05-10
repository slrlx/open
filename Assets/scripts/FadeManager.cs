using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;  // fadeimage 오브젝트 연결할 것
    public float fadeDuration = 1.0f;

    void Start()
    {
        // 시작 시 화면을 투명하게 해둠
        SetAlpha(0f);
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(Fade(0f, 1f, onComplete));
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(Fade(1f, 0f, onComplete));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, System.Action onComplete)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
        onComplete?.Invoke();
    }

    private void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, a);
    }
}