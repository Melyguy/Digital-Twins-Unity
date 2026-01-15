using UnityEngine;

public class serverInfo : MonoBehaviour
{
    public GameObject Server;
    public string id;
    public float Temperature;
    public float CpuUsage;
    public bool IsOnline;

    public GameObject OnButton;
    public GameObject OffButton;

    public ServerDataClient serverDataClient;

    void Start()
    {
        serverDataClient = FindObjectOfType<ServerDataClient>();
    }
    void Update()
    {
               if (serverDataClient.data != null)
        {
            foreach (var server in serverDataClient.data.servers)
            {
                if (server.id == id)
                {
                    Temperature = server.temperature;
                    CpuUsage = server.cpuUsage;
                    IsOnline = server.online;
                }
            }
        }

        if(IsOnline)
        {
            OnButton.SetActive(true);
            OffButton.SetActive(false);
        }
        else
        {
            OnButton.SetActive(false);
            OffButton.SetActive(true);
        }
    }
}
