using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadControl : MonoBehaviour
{
    public ObsControl obsControl;
    public ViconControl viconControl;
    public SoundOscControl soundControl;

    public ObsSettings obsSettings;
    public ViconSettings viconSettings;
    public SoundSettings soundSettings;

    public ObsSettingsUI obsUis;
    public ViconSettingsUI viconUis;
    public SoundSettingsUI soundUis;

    string obsSettingPath { get { return Path.Combine(Application.persistentDataPath, "obsSettings.json"); } }
    string viconSettingPath { get { return Path.Combine(Application.persistentDataPath, "viconSettings.json"); } }
    string soundSettingPath { get { return Path.Combine(Application.persistentDataPath, "soundSettings.json"); } }

    [System.Serializable]
    public class ObsSettingsUI
    {
        public InputField remoteIP;
        public InputField remotePort;
        public InputField password;
    }
    [System.Serializable]
    public class ViconSettingsUI
    {
        public InputField remoteIP1;
        public InputField remotePort1;
        public InputField remoteIP2;
        public InputField remotePort2;

        public InputField subjectName;
        public InputField notes;
        public InputField description;
        public InputField dataBasePath;
        public InputField delay;

        public Toggle useSecontIP;
    }
    [System.Serializable]
    public class SoundSettingsUI
    {
        public InputField remoteIP;
        public InputField remotePort;
        public InputField cutName;
        public InputField delay;
    }

    [System.Serializable]
    public class ObsSettings
    {
        public string remoteIP;
        public int remotePort;
        public string password;
    }
    [System.Serializable]
    public class ViconSettings
    {
        public string remoteIP1;
        public int remotePort1;
        public string remoteIP2;
        public int remotePort2;

        public string subjectName;
        public string notes;
        public string description;
        public string dataBasePath;
        public int delay;

        public bool useSecontIP;
    }
    [System.Serializable]
    public class SoundSettings
    {
        public string remoteIP;
        public int remotePort;
        public string cutName;
        public int delay;
    }

    private void Start()
    {
        LoadSettings();
    }
    private void OnDestroy()
    {
        SaveSettings();
    }

    void LoadSettings()
    {
        if (File.Exists(obsSettingPath))
        {
            var json = File.ReadAllText(obsSettingPath);
            JsonUtility.FromJsonOverwrite(json, obsSettings);
            json = File.ReadAllText(viconSettingPath);
            JsonUtility.FromJsonOverwrite(json, viconSettings);
            json = File.ReadAllText(soundSettingPath);
            JsonUtility.FromJsonOverwrite(json, soundSettings);
        }
        else
        {
            SaveSettings();
        }
        ApplySettingsToUI();
    }
    void ApplySettingsToUI() {
        obsUis.remoteIP.text = obsSettings.remoteIP;
        obsUis.remotePort.text = obsSettings.remotePort.ToString();
        obsUis.password.text = obsSettings.password;


        viconUis.remoteIP1.text = viconSettings.remoteIP1;
        viconUis.remotePort1.text = viconSettings.remotePort1.ToString();
        viconUis.remoteIP2.text = viconSettings.remoteIP2;
        viconUis.remotePort2.text = viconSettings.remotePort2.ToString();


        viconUis.subjectName.text = viconSettings.subjectName;
        viconUis.notes.text = viconSettings.notes;
        viconUis.description.text = viconSettings.description;
        viconUis.dataBasePath.text = viconSettings.dataBasePath;
        viconUis.delay.text = viconSettings.delay.ToString();
        viconUis.useSecontIP.isOn = viconSettings.useSecontIP;


        soundUis.remoteIP.text = soundSettings.remoteIP;
        soundUis.remotePort.text = soundSettings.remotePort.ToString();
        soundUis.cutName.text = soundSettings.cutName;
        soundUis.delay.text = soundSettings.delay.ToString();
    }
    void SaveSettings()
    {
        obsSettings.remoteIP = obsControl.RemoteIP;
        obsSettings.remotePort = int.Parse(obsControl.RemotePort);
        obsSettings.password = obsControl.Password;

        viconSettings.remoteIP1 = viconControl.RemoteIP;
        viconSettings.remotePort1 = int.Parse(viconControl.RemotePort);
        viconSettings.remoteIP2 = viconControl.RemoteIP2;
        viconSettings.remotePort2 = int.Parse(viconControl.RemotePort2);

        viconSettings.subjectName = viconControl.SubjectName;
        viconSettings.notes = viconControl.Notes;
        viconSettings.description = viconControl.Description;
        viconSettings.dataBasePath = viconControl.DbPath;
        viconSettings.delay = int.Parse(viconControl.Delay);
        viconSettings.useSecontIP = viconControl.UseSecondIP;

        soundSettings.remoteIP = soundControl.RemoteIP;
        soundSettings.remotePort = int.Parse(soundControl.RemotePort);
        soundSettings.cutName = soundControl.CutName;
        soundSettings.delay = int.Parse(soundControl.Delay);

        var json = JsonUtility.ToJson(obsSettings);
        File.WriteAllText(obsSettingPath, json, System.Text.Encoding.UTF8);
        json = JsonUtility.ToJson(viconSettings);
        File.WriteAllText(viconSettingPath, json, System.Text.Encoding.UTF8);
        json = JsonUtility.ToJson(soundSettings);
        File.WriteAllText(soundSettingPath, json, System.Text.Encoding.UTF8);
    }
}
