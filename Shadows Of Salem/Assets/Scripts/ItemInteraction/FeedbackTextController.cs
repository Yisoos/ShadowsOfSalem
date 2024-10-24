using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackTextController : MonoBehaviour
{
    public TMP_Text feedbackText;
    public float displayDuration = 2f;  // Time to display the text in seconds
    public float fadeDuration = 1f;  // Time it takes to fully fade out
    // Start is called before the first frame update
    void Start()
    {
        feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, 0);
    }
    public void PopUpText(string displayText)
    {
        // Set the text
        feedbackText.text = displayText;
        transform.SetAsLastSibling();
        // Set the color with full opacity (alpha = 1)
        feedbackText.color = new Color(feedbackText.color.r, feedbackText.color.g, feedbackText.color.b, 1f);

        // Start the coroutine to handle the fade-out after the display duration
        StartCoroutine(FadeOutText());
    }

    // Coroutine to fade out the text after a delay
    private IEnumerator FadeOutText()
    {
        // Wait for the specified display duration
        yield return new WaitForSeconds(displayDuration);

        // Start fading the alpha of the text color
        
        float elapsedTime = 0f;

        // Store the current color
        Color originalColor = feedbackText.color;

        // Gradually fade out the alpha over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            feedbackText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;  // Wait for the next frame
        }

        // Ensure the text is fully transparent at the end
        feedbackText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
