using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStatusControl : MonoBehaviour
{
    public Button obsStart;
    public Button obsStop;

    public Button viconStart;
    public Button viconStop;

    public Button soundStart;
    public Button soundStop;

    public Button allStart;
    public Button allStop;
    public Button playBtn;

    public Button ready1;
    public Button ready2;

    List<Button> AllButtons
    {
        get
        {
            if (allButtons == null)
                allButtons = new List<Button>(new[] {
                    obsStart,obsStop,
                    viconStart,viconStop,
                    soundStart,soundStop,
                    soundStart,soundStop,
                    allStart,allStop,
                    ready1,ready2,playBtn,
                });
            return allButtons;
        }
    }
    List<Button> allButtons;
    List<Button> AllStartButtons
    {
        get
        {
            if (allStartButtons == null)
                allStartButtons = new List<Button>(new[] {
                    obsStart,
                    viconStart,
                    soundStart,
                    soundStart,
                    allStart,
                    ready1,ready2,playBtn,
                });
            return allStartButtons;
        }
    }
    List<Button> allStartButtons;

    private void Start()
    {
        OnStopButton();
    }
    public void OnStopButton()
    {
        AllButtons.ForEach(b => b.interactable = false);
        AllStartButtons.ForEach(b => b.interactable = true);
    }

    public void OnObsStart()
    {
        AllButtons.ForEach(b => b.interactable = false);
        obsStop.interactable = true;
    }
    public void OnViconStart()
    {
        AllButtons.ForEach(b => b.interactable = false);
        viconStop.interactable = true;
    }
    public void OnSoundStart()
    {
        AllButtons.ForEach(b => b.interactable = false);
        soundStop.interactable = true;
    }
    public void OnAllStart()
    {
        AllButtons.ForEach(b => b.interactable = false);
        allStop.interactable = true;
    }
}
