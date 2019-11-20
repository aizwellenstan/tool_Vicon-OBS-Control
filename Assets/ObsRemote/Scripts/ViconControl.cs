﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;

public class ViconControl : MonoBehaviour
{
    public UdpSender sender;
    public UdpSender sender2;
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
    public string RemoteIP2
    {
        get { return sender2.remoteIp; }
        set
        {
            if (sender2.remoteIp != value)
                sender2.CreateRemoteEP(value, sender2.remotePort);
        }
    }
    public string RemotePort2
    {
        get { return sender2.remotePort.ToString(); }
        set
        {
            var newPort = int.Parse(value);
            if (sender2.remotePort != newPort)
                sender2.CreateRemoteEP(sender2.remoteIp, newPort);
        }
    }

    public string SubjectName { get { return subjectNamePrefix; } set { subjectNamePrefix = value; } }
    [SerializeField] string subjectNamePrefix = "ViconTest";
    string subjectName;
    public string Notes { get { return notes; } set { notes = value; } }
    [SerializeField] string notes = "";
    public string Description { get { return description; } set { description = value; } }
    [SerializeField] string description = "";
    public string DbPath { get { return dbPath; } set { dbPath = value; } }
    [SerializeField] string dbPath = "D:\\ViconDB\\Reflap\\Project 2\\Gene\\Session 1\\";
    public string Delay { get { return delay.ToString(); }  set { delay = int.Parse(value); } }
    [SerializeField] int delay = 0;
    public bool UseSecondIP { get { return useSecondIP; } set { useSecondIP = value; } }
    [SerializeField] bool useSecondIP;

    [SerializeField] int packetId = 0;

    public void SendStartCapture()
    {
        packetId = Random.Range(0, int.MaxValue);
        var now = System.DateTime.Now;
        subjectName = $"{subjectNamePrefix}_{now.Year}_{now.Month.ToString("00")}{now.Day.ToString("00")}_{now.Hour.ToString("00")}{now.Minute.ToString("00")}";
        SendText(CreateStartCaptureXML());
    }

    public void SendStopCapture()
    {
        packetId = Random.Range(0, int.MaxValue);
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

        return doc.OuterXml;
    }

    void SendText(string text)
    {
        sender.Send(text);
        if (useSecondIP)
            sender2.Send(text);
    }
}
