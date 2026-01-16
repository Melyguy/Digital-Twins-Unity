using UnityEngine;
using TMPro;

public class Clickcheck : MonoBehaviour
{
    public GameObject Server;
    public GameObject UIFrame;
    public serverInfo info;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Temp;
    public TextMeshProUGUI Usage;
    public TextMeshProUGUI Online;

    public Camera Cam;

    public void Clicked()
    {
        if(UIFrame.activeSelf == false)
        {
            UIFrame.SetActive(true);
            Name.text = Server.name;
            Temp.text = "Temperature: " + info.Temperature.ToString("F1") + " °F";
            Usage.text = "CPU Usage: " + info.CpuUsage.ToString("F1") + " %";
            Online.text = "Status: " + (info.IsOnline ? "Online" : "Offline"); 
        }

    }

    public void Update()
    { 
        Vector3 MousePos = Input.mousePosition;
        Ray ray = Cam.ScreenPointToRay(MousePos);

        RaycastHit raycastHit;

        bool Hit = Physics.Raycast(ray, out raycastHit);

        if(Hit)
        {
            if(raycastHit.transform.gameObject == Server)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Clicked();
                }
            }
        }
    }

    public void CloseUI()
    {
        UIFrame.SetActive(false);
    }
}
