using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

//https://github.com/Palakis/obs-websocket-dotnet/blob/master/TestClient/MainWindow.cs
public class ObsControl : MonoBehaviour
{
    public ViconControl viconControl;

    public string RemoteIP { get { return remoteIp; } set { remoteIp = value; } }
    [SerializeField] string remoteIp = "localhost";
    public string RemotePort { get { return remotePort.ToString(); } set { remotePort = int.Parse(value); } }
    [SerializeField] int remotePort = 4444;
    string url { get { return $"ws://{remoteIp}:{remotePort}"; } }
    public string Password { get { return password; } set { password = value; } }
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

    public void OnStartRecordingButton()
    {
        if (obs.IsConnected)
            obs.StartRecording();
    }
    public void OnStopRecordingButton()
    {
        if (obs.IsConnected)
            obs.StopRecording();
        RenameRecordedFile();
    }
    public void RenameRecordedFile()
    {
        var recordingFolder = obs.GetRecordingFolder();
        Thread.Sleep(2000);
        var files = Directory.GetFiles(recordingFolder);
        var latestFile = files.OrderBy(f => File.GetCreationTime(f)).Last();
        var directoryName = Path.GetDirectoryName(latestFile);
        var fileName = Path.GetFileName(latestFile);
        fileName = viconControl.SubjectName + fileName;
        File.Move(latestFile, Path.Combine(directoryName, fileName));
    }
}
