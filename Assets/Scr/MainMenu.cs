using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Animator myAnimator;
    public RectTransform playButton, loadButton, quitButton; // Assuming these are assigned in the inspector
    private bool isScaled = false;

    void Update()
    {
        //AnimationCheck();
    }

    private void AnimationCheck()
    {
        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log($"Animation State: {stateInfo.fullPathHash}, Normalized Time: {stateInfo.normalizedTime}");
        if (stateInfo.IsName("Idle") && stateInfo.normalizedTime >= 1.0f && !isScaled)
        {
            Debug.Log("Starting Scaling");
            StartScaling();
            isScaled = true;
        }
    }

    public void StartScaling()
    {
        StartCoroutine(ScaleRectTransform(playButton, 0.75f));
        StartCoroutine(ScaleRectTransform(loadButton, 0.75f));
        StartCoroutine(ScaleRectTransform(quitButton, 0.75f));
    }

    IEnumerator ScaleRectTransform(RectTransform rectTransform, float duration)
    {
        float currentTime = 0;
        Vector3 startScale = new Vector3(0, 0, 0); // Starting scale
        Vector3 endScale = new Vector3(1, 1, 1); // Target scale

        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration; // Normalize time to [0, 1]
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, t); // Smoothly interpolate scale
            Debug.Log($"Scaling {rectTransform.name}: {rectTransform.localScale}"); // Add logging
            yield return null;
        }

        rectTransform.localScale = endScale; // Ensure it's set to the final scale after loop
        Debug.Log($"Scaling Complete for {rectTransform.name}"); // Confirm completion
    }
}
