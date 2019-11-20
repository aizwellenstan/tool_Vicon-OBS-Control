using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Osc;

public class SoundOscControl : MonoBehaviour
{
    public UdpSender sender;
    public string RemoteIP
    {
        get { return sender.remoteIp; }
        set
        {
            if (sender.remoteIp != value)
                sender.CreateRemoteEP(value, sender.remotePort);
        }
    }
    public string RemotePort
    {
        get { return sender.remotePort.ToString(); }
        set
        {
            var newPort = int.Parse(value);
            if (sender.remotePort != newPort)
                sender.CreateRemoteEP(sender.remoteIp, newPort);
        }
    }
    public string CutName { get { return cutName; } set { cutName = value; } }
    [SerializeField] string cutName = "C1";
    public string Delay { get { return delay.ToString(); } set { delay = int.Parse(value); } }
    [SerializeField] int delay;

    public void SendStart()
    {
        var osc = new MessageEncoder("start");
        osc.Add(cutName);
        osc.Add(delay);
        sender.Send(osc.Encode());
    }
    public void SendStop()
    {
        var osc = new MessageEncoder("stop");
        osc.Add(cutName);
        sender.Send(osc.Encode());
    }
}
