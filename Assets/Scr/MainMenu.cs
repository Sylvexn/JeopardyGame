using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Animator myAnimator;
    public RectTransform playButton, loadButton, quitButton; // Use RectTransform instead of GameObject
    private bool hasScaled = false;

    void Update()
    {
        AnimationCheck();
    }

    private void AnimationCheck()
    {
        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") && !hasScaled)
        {
            Debug.Log("Animation Finished, starting scaling.");
            StartCoroutine(ScaleObjects());
            hasScaled = true;
        }
    }

    IEnumerator ScaleObjects()
    {
        Debug.Log("Scaling Objects...");
        float duration = 0.75f;
        float currentTime = 0f;
        while (currentTime <= duration)
        {
            float t = currentTime / duration; // Normalized time
            Vector3 targetScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            playButton.localScale = targetScale;
            loadButton.localScale = targetScale;
            quitButton.localScale = targetScale;
            currentTime += Time.deltaTime;
            yield return null;
        }
        // Ensure final scale is set to Vector3.one
        playButton.localScale = Vector3.one;
        loadButton.localScale = Vector3.one;
        quitButton.localScale = Vector3.one;
        Debug.Log("Scaling complete.");
    }
}
