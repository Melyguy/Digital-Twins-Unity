using UnityEngine;
using TMPro;    

public class ServersManager : MonoBehaviour
{
    public GameObject[] Servers;
    public TextMeshProUGUI ServersInRack;


    void Start()
    {
        ServersInRack.text = "Servers in Rack: " + Servers.Length.ToString();
    }
}
