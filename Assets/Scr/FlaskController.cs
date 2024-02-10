using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendPongOnButtonPress : MonoBehaviour
{
    public Button yourButton;
    public Image targetImage;

    private void Start()
    {
        yourButton.onClick.AddListener(SendPong);
    }
    void Update()
    {
        StartCoroutine(CheckColorChangeRoutine());
    }

    void SendPong()
    {
        StartCoroutine(SendPongCoroutine("http://localhost:5000/receive"));
        
    }

    IEnumerator SendPongCoroutine(string url)
    {
        byte[] postData = System.Text.Encoding.UTF8.GetBytes("Unity To Flask");
        UnityWebRequest www = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(postData),
            downloadHandler = new DownloadHandlerBuffer()
        };
        www.SetRequestHeader("Content-Type", "text/plain");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Pong sent successfully!");
        }
    }
    IEnumerator CheckColorChangeRoutine()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/check-color-change");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<ColorChangeResponse>(www.downloadHandler.text);
                if (response.changeColor)
                {
                    Debug.Log("Recieved flask");
                    targetImage.color = Color.green; // Change color to green
                }
            }
            else
            {
                //Debug.LogError("Failed to check color change: " + www.error);
            }
            yield return new WaitForSeconds(1); // Poll every second
        }
    }

    [System.Serializable]
    private class ColorChangeResponse
    {
        public bool changeColor;
    }
}
