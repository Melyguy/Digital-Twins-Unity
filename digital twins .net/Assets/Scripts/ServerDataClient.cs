using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

[Serializable]
public class ServerState
{
    public string id;
    public float temperature;
    public float cpuUsage;
    public bool online;
}

[Serializable]
public class ServerStateList
{
    public ServerState[] servers;
}

public class ServerDataClient : MonoBehaviour
{
    public string apiUrl = "https://localhost:7292/api/servers";
    public float updateInterval = 2f;
    public ServerStateList data;

    void Start()
    {
        StartCoroutine(UpdateServersLoop());
    }

    IEnumerator UpdateServersLoop()
    {
        while (true)
        {
            yield return FetchServers();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    IEnumerator FetchServers()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        yield return request.SendWebRequest();
        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching server data: " + request.error);
        }
        else
        {
            string json = "{\"servers\":" + request.downloadHandler.text + "}";
            data = JsonUtility.FromJson<ServerStateList>(json);
            Debug.Log("Fetched " + data.servers.Length + " servers.");
        }
    }
}
