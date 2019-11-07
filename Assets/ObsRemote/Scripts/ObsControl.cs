using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

//https://github.com/Palakis/obs-websocket-dotnet/blob/master/TestClient/MainWindow.cs
public class ObsControl : MonoBehaviour
{
    public string RemoteIP { set { remoteIp = value; } }
    [SerializeField] string remoteIp = "localhost";
    public string RemotePort { set { remotePort = int.Parse(value); } }
    [SerializeField] int remotePort = 4444;
    string url { get { return $"ws://{remoteIp}:{remotePort}"; } }
    public string Password { set { password = value; } }
    [SerializeField] string password;

    [Header("UI Settings")]
    public RectTransform connectedUI;
    public Text connectBtnText;

    OBSWebsocket obs;

    // Start is called before the first frame update
    void Start()
    {
        obs = new OBSWebsocket();

        obs.Connected += (sender, args) =>
        {
            connectBtnText.text = "Dissconnect";
            connectedUI.gameObject.SetActive(true);
        };
        obs.Disconnected += (sender, args) =>
        {
            connectBtnText.text = "Connect To OBS";
            connectedUI.gameObject.SetActive(false);
        };
    }
    private void OnDestroy()
    {
        obs.Disconnect();
    }

    public void OnConnectButton()
    {
        if (!obs.IsConnected)
        {
            try
            {
                obs.Connect(url, password);
            }
            catch (AuthFailureException)
            {
                Debug.Log("Authentication failed.");
            }
            catch (ErrorResponseException ex)
            {
                Debug.Log("Connect failed" + ex.Message);
            }
        }
        else
            obs.Disconnect();
    }

    public void OnStartRecordingButton() {
        obs.StartRecording();
    }
    public void OnStopRecordingButton()
    {
        obs.StopRecording();
    }
}
