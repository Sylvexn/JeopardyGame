using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DiscordController : MonoBehaviour
{
    private string flaskAppUrl = "http://localhost:5000/mute";
    private string flaskAppPing = "http://localhost:5000/ping";
    private bool shouldMute;

    // Call this function to mute a user with the given userID
    public void MuteDiscordUser(string userID)
    {
        StartCoroutine(MuteUserCoroutine(userID));
    }

    private IEnumerator MuteUserCoroutine(string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", userID);
        using (UnityWebRequest www = UnityWebRequest.Post(flaskAppUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error muting user: " + www.error);
            }
            else
            {
                Debug.Log("Successfully requested user mute: " + www.downloadHandler.text);
            }
        }
    }
    public void PingTest()
    {
        StartCoroutine(PingCoroutine());
    }
    private IEnumerator PingCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(flaskAppPing))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ping test failed: " + www.error);
            }
            else
            {
                Debug.Log("Ping test success: " + www.downloadHandler.text);
            }
        }
    }
}
