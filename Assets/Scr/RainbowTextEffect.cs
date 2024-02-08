using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace
using UnityEngine.EventSystems; // Required for the event system interfaces

public class RainbowTextEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textMesh; // Assign this in the inspector
    private bool isHovering = false;
    private Coroutine rainbowCoroutine;

    private void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (rainbowCoroutine != null)
        {
            StopCoroutine(rainbowCoroutine);
        }
        rainbowCoroutine = StartCoroutine(RainbowColorEffect());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (rainbowCoroutine != null)
        {
            StopCoroutine(rainbowCoroutine);
            textMesh.color = Color.black; // Revert to black when not hovering
        }
    }

    private IEnumerator RainbowColorEffect()
    {
        float hue = 0;
        while (isHovering)
        {
            textMesh.color = Color.HSVToRGB(hue, 1, 1);
            hue += Time.deltaTime; // Adjust the speed of color change by modifying this value
            if (hue >= 1) hue = 0;
            yield return null;
        }
    }
}
