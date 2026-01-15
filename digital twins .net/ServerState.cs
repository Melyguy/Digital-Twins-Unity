csharp Assets\Scripts\ServerDataClient.cs
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

    // Expose last-fetched data so other components can read it
    public ServerStateList data;

    // Development helper: allow bypassing TLS validation for localhost (do NOT use in production)
    public bool allowInsecureLocalhostTls = true;

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
        Debug.Log($"ServerDataClient.FetchServers: attempting GET {apiUrl}");
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            // Developer option: bypass TLS validation for localhost with self-signed cert
            if (allowInsecureLocalhostTls && apiUrl.StartsWith("https://localhost", StringComparison.OrdinalIgnoreCase))
            {
                request.certificateHandler = new AcceptAllCertificates();
            }

            yield return request.SendWebRequest();

            long responseCode = request.responseCode;
            string downloaded = request.downloadHandler != null ? request.downloadHandler.text : string.Empty;
            Debug.Log($"ServerDataClient.FetchServers: result={request.result}, responseCode={responseCode}, error='{request.error}', payloadLength={downloaded?.Length ?? 0}");

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error fetching servers: {request.error} (HTTP {responseCode})");
                // Print body when available for additional clues
                if (!string.IsNullOrEmpty(downloaded))
                    Debug.LogWarning("Server returned payload: " + downloaded);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(downloaded))
                {
                    Debug.LogWarning("ServerDataClient.FetchServers: empty response body");
                }

                // Wrap JSON in "servers" key so JsonUtility can parse arrays
                string jsonWrapped = "{\"servers\":" + downloaded + "}";
                Debug.Log("ServerDataClient.FetchServers: wrapped JSON: " + jsonWrapped);

                try
                {
                    var parsed = JsonUtility.FromJson<ServerStateList>(jsonWrapped);
                    if (parsed == null || parsed.servers == null)
                    {
                        Debug.LogError("Failed to parse server list (parsed == null or parsed.servers == null). Check JSON format.");
                    }
                    else
                    {
                        this.data = parsed;
                        Debug.Log($"ServerDataClient.FetchServers: parsed {parsed.servers.Length} server(s).");
                        foreach (var server in parsed.servers)
                        {
                            if (server == null) continue;
                            GameObject go = GameObject.Find(server.Id);
                            if (go != null)
                            {
                                Debug.Log("Found server GameObject: " + server.Id);
                            }
                            else
                            {
                                Debug.Log($"No GameObject found for server.Id='{server.Id}'");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Exception parsing JSON: " + ex);
                }
            }
        }
    }

    // Accept all certificates (ONLY for local development when using self-signed certs)
    class AcceptAllCertificates : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}