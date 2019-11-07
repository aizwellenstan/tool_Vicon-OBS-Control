using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;

public class ViconControl : MonoBehaviour
{
    public UdpSender sender;
    public string RemoteIP
    {
        set
        {
            if (sender.remoteIp != value)
                sender.CreateRemoteEP(value, sender.remotePort);
        }
    }
    public string RemotePort
    {
        set
        {
            var newPort = int.Parse(value);
            if (sender.remotePort != newPort)
                sender.CreateRemoteEP(sender.remoteIp, newPort);
        }
    }

    public string SubjectName { set { subjectName = value; } }
    [SerializeField] string subjectName = "ViconTest";
    public string Notes { set { notes = value; } }
    [SerializeField] string notes = "";
    public string Description { set { description = value; } }
    [SerializeField] string description = "";
    public string DbPath { set { dbPath = value; } }
    [SerializeField] string dbPath = "D:\\ViconDB\\Reflap\\Project 2\\Gene\\Session 1\\";
    public string Delay { set { delay = int.Parse(value); } }
    [SerializeField] int delay = 0;

    [SerializeField] int packetId = 0;

    public void SendStartCapture()
    {
        SendText(CreateStartCaptureXML());
    }

    public void SendStopCapture()
    {
        SendText(CreateStopCaptureXML());
    }


    string CreateStartCaptureXML()
    {
        var doc = new XmlDocument();
        var docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
        doc.AppendChild(docNode);

        var rootNode = doc.CreateElement("CaptureStart");
        doc.AppendChild(rootNode);

        var childNode = doc.CreateElement("Name");
        childNode.SetAttribute("VALUE", subjectName);
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("Notes");
        childNode.SetAttribute("VALUE", notes);
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("Description");
        childNode.SetAttribute("VALUE", description);
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("DatabasePath");
        childNode.SetAttribute("VALUE", dbPath);
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("Delay");
        childNode.SetAttribute("VALUE", delay.ToString());
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("PacketID");
        childNode.SetAttribute("VALUE", packetId.ToString());
        rootNode.AppendChild(childNode);
        packetId++;

        return doc.OuterXml;
    }
    string CreateStopCaptureXML()
    {
        var doc = new XmlDocument();
        var docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
        doc.AppendChild(docNode);

        var rootNode = doc.CreateElement("CaptureStop");
        rootNode.SetAttribute("RESULT", "SUCCESS");
        doc.AppendChild(rootNode);

        var childNode = doc.CreateElement("Name");
        childNode.SetAttribute("VALUE", subjectName);
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("DatabasePath");
        childNode.SetAttribute("VALUE", dbPath);
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("Delay");
        childNode.SetAttribute("VALUE", delay.ToString());
        rootNode.AppendChild(childNode);

        childNode = doc.CreateElement("PacketID");
        childNode.SetAttribute("VALUE", packetId.ToString());
        rootNode.AppendChild(childNode);
        packetId++;

        return doc.OuterXml;
    }

    void SendText(string text)
    {
        sender.Send(text);
    }
}
