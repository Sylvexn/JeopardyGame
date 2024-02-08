using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DiscordController : MonoBehaviour
{
    
    //private string sylUID = "138887188468203520";
    // The URL to your Flask app
    private string flaskAppUrl = "http://localhost:5000/mute";
    private bool shouldMute;

    // Call this function to mute a user with the given userID
    public void MuteDiscordUser(string userID)
    {
        StartCoroutine(MuteUserCoroutine(userID));
    }

    private IEnumerator MuteUserCoroutine(string userID)
    {
        // Create a form and add the user ID field
        WWWForm form = new WWWForm();
        form.AddField("userId", userID);
        //form.AddField("Should Mute", shouldMute);

        // Send a POST request to the Flask app with the form
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
}
