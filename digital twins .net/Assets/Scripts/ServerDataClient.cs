using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

[Serializable]
public class ServerState
{
    public string Id;
    public float Temperature;
    public float CpuUsage;
    public bool IsOnline;
}

[Serializable]
public class ServerStateList
{
    public ServerState[] servers;
}

public class ServerDataClient : MonoBehaviour
{
    // URL of your .NET backend
    public string apiUrl = "https://localhost:7292/api/servers";

    // How often to update (seconds)
    public float updateInterval = 2f;

    void Start()
    {
        // Start the polling coroutine
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
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching servers: " + request.error);
            }
            else
            {
                // Wrap JSON in "servers" key so JsonUtility can parse arrays
                string json = "{\"servers\":" + request.downloadHandler.text + "}";
                var data = JsonUtility.FromJson<ServerStateList>(json);

                foreach (var server in data.servers)
                {
                    // Find the GameObject with the same name as server.Id
                    GameObject go = GameObject.Find(server.Id);
                    if (go != null)
                    {
                        Debug.Log("Found server GameObject: " + server.Id);
                    }
                }
            }
        }
    }
}
