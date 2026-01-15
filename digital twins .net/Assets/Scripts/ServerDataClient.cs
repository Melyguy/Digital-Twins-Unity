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
    // URL of your .NET backend
    public string apiUrl = "https://localhost:7292/api/servers";

    // How often to update (seconds)
    public float updateInterval = 2f;

    // Expose last-fetched data so other components can read it
    public ServerStateList data;

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
                var parsed = JsonUtility.FromJson<ServerStateList>(json);

                // store parsed data for others to read
                this.data = parsed;

                foreach (var server in parsed.servers)
                {
                    // Find the GameObject with the same name as server.Id
                    GameObject go = GameObject.Find(server.id);
                    if (go != null)
                    {
                        Debug.Log("Found server GameObject: " + server.id);
                    }
                }
            }
        }
    }
}
