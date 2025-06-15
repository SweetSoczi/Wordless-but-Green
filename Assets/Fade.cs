using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator FadeOutIn(System.Action onMidFade)
    {
        yield return StartCoroutine(Fade(0f, 1f));

        onMidFade?.Invoke();

        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, to);
    }
}
