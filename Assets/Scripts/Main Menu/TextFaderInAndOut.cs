using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
 
public class TextFaderInAndOut : MonoBehaviour
{
    public float timeToFade = 2.0f;
    bool isFadingIn = true;
    TextMeshProUGUI text;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        while (isFadingIn)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / timeToFade));
            if (text.color.a >= 1.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
                isFadingIn = false;
            }
            yield return null;
        }

        while (!isFadingIn)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / (timeToFade * 1.5f)));
            if (text.color.a <= 0.25f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0.25f);
                isFadingIn = true;
            }
            yield return null;
        }
    }
}